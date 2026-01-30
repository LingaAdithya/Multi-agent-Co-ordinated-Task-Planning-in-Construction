using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class AgentController : MonoBehaviour
{
    [Header("Target")]
    public Transform targetTransform;

    [Header("Vacuum Bot Motion")]
    public float moveSpeed = 2f;
    public float stopDistance = 0.25f;

    [Header("Block Detection")]
    public float sensorRadius = 0.25f;
    public float sensorLength = 0.9f;
    public LayerMask avoidMask;              // ONLY Obstacle + Human + Agent
    public float detourSideStep = 1.0f;      // how far to sidestep
    public float detourForwardStep = 0.6f;   // how far forward while sidestepping
    public float detourReach = 0.3f;         // when we consider detour reached
    public float replanCooldown = 0.25f;     // prevents spam detours

    [Header("Role Flags")]
    public bool isTransportAgent = false;
    public bool isAssemblyAgent = false;

    [Header("Coordination")]
    private ZoneManager zoneManager;
    private bool waitingForZone = false;
    private string currentZone = "";

    [Header("Human Safety")]
    private bool humanBlocked = false;

    private Rigidbody rb;
    private bool deliveredOnce = false;

    // Detour logic
    private bool usingDetour = false;
    private Vector3 detourPoint;
    private float nextReplanTime = 0f;
    private int preferRight = 1; // 1 right, -1 left (flip if stuck)

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationY |
                         RigidbodyConstraints.FreezeRotationZ;
    }

    void Start()
    {
        zoneManager = FindObjectOfType<ZoneManager>();
    }

    void FixedUpdate()
    {
        if (targetTransform == null) { StopMotion(); return; }
        if (humanBlocked || waitingForZone) { StopMotion(); return; }

        if (isAssemblyAgent && TaskManager.Instance != null && !TaskManager.Instance.materialDelivered)
        { StopMotion(); return; }

        Vector3 pos = rb.position; pos.y = 0f;

        // Decide current goal: detour or real target
        Vector3 goal = usingDetour ? detourPoint : targetTransform.position;
        goal.y = 0f;

        // Reached goal?
        float distToGoal = Vector3.Distance(pos, goal);

        if (usingDetour && distToGoal <= detourReach)
        {
            usingDetour = false; // back to real target
        }

        // If reached final target
        if (!usingDetour)
        {
            Vector3 finalTarget = targetTransform.position; finalTarget.y = 0f;
            float distFinal = Vector3.Distance(pos, finalTarget);

            if (distFinal <= stopDistance)
            {
                StopMotion();

                if (isTransportAgent && !deliveredOnce && TaskManager.Instance != null)
                {
                    TaskManager.Instance.MarkMaterialDelivered();
                    deliveredOnce = true;
                    isTransportAgent = false;
                }
                return;
            }
        }

        // Move direction
        Vector3 dir = (goal - pos);
        dir.y = 0f;

        if (dir.sqrMagnitude < 0.0001f) { StopMotion(); return; }
        dir.Normalize();

        // If blocked ahead and not currently detouring, plan a detour
        if (!usingDetour && Time.time >= nextReplanTime && IsBlocked(dir))
        {
            if (TryPlanDetour(pos, dir))
            {
                usingDetour = true;
                nextReplanTime = Time.time + replanCooldown;
            }
            else
            {
                // If both sides blocked, just stop briefly and flip preference
                StopMotion();
                preferRight *= -1;
                nextReplanTime = Time.time + replanCooldown;
                return;
            }
        }

        // Face + move (vacuum behavior)
        FaceDirection(dir);
        rb.linearVelocity = dir * moveSpeed;
    }

    // SphereCast forward: ignores triggers (zones)
    bool IsBlocked(Vector3 dir)
    {
        // Start slightly above ground AND slightly forward so we don't hit our own collider
        Vector3 origin = transform.position + Vector3.up * 0.25f + dir * 0.3f;

        return Physics.SphereCast(
            origin,
            sensorRadius,
            dir,
            out RaycastHit hit,
            sensorLength,
            avoidMask,
            QueryTriggerInteraction.Ignore
        );
    }

    bool TryPlanDetour(Vector3 pos, Vector3 forwardDir)
    {
        Vector3 right = Vector3.Cross(Vector3.up, forwardDir).normalized; // right-hand perpendicular
        Vector3 left = -right;

        // Candidate detours (a little forward + side)
        Vector3 candRight = pos + forwardDir * detourForwardStep + right * detourSideStep;
        Vector3 candLeft  = pos + forwardDir * detourForwardStep + left  * detourSideStep;

        // Check if path toward candidate is clear
        bool rightClear = !IsBlocked((candRight - pos).normalized);
        bool leftClear  = !IsBlocked((candLeft - pos).normalized);

        if (rightClear && leftClear)
        {
            detourPoint = (preferRight == 1) ? candRight : candLeft;
            return true;
        }
        if (rightClear)
        {
            detourPoint = candRight;
            preferRight = 1;
            return true;
        }
        if (leftClear)
        {
            detourPoint = candLeft;
            preferRight = -1;
            return true;
        }

        return false;
    }

    void FaceDirection(Vector3 dir)
    {
        Quaternion targetRot = Quaternion.LookRotation(dir);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, 10f * Time.fixedDeltaTime));
    }

    void StopMotion()
    {
        rb.linearVelocity = Vector3.zero;
    }

    // ---------- Zone coordination ----------
    public void TryEnterZone(string zoneName)
    {
        if (zoneManager == null) return;

        if (zoneManager.CanEnter(zoneName))
        {
            zoneManager.EnterZone(zoneName, gameObject);
            currentZone = zoneName;
            waitingForZone = false;
        }
        else waitingForZone = true;
    }

    public void ExitZone(string zoneName)
    {
        if (zoneManager == null) return;

        if (currentZone == zoneName)
        {
            zoneManager.ExitZone(zoneName, gameObject);
            currentZone = "";
            waitingForZone = false;
        }
    }

    public void SetHumanBlocked(bool blocked) => humanBlocked = blocked;

    // Debug (optional): draw sensor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 dir = transform.forward;
        Vector3 origin = transform.position + Vector3.up * 0.25f + dir * 0.3f;
        Gizmos.DrawWireSphere(origin, sensorRadius);
        Gizmos.DrawLine(origin, origin + dir * sensorLength);

        if (usingDetour)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(detourPoint + Vector3.up * 0.1f, 0.2f);
        }
    }
}

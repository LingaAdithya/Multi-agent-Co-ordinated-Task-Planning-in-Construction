using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HumanController : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true; // manual control
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(h, 0f, v).normalized * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + move);
    }
}

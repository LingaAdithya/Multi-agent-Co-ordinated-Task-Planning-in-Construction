using UnityEngine;

public class HumanBlockZone : MonoBehaviour
{
    private int humansInside = 0; // handles multiple colliders / multiple humans safely

    private bool IsHuman(Collider other)
    {
        // Use tag if you have it, otherwise name fallback
        return other.CompareTag("Human") || other.name.Contains("Human");
    }

    private void SetAllAgentsBlocked(bool blocked)
    {
        var agents = FindObjectsOfType<AgentController>();
        foreach (var a in agents)
            a.SetHumanBlocked(blocked);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsHuman(other)) return;

        humansInside++;
        if (humansInside == 1)
        {
            SetAllAgentsBlocked(true);
            Debug.Log("🚧 Human entered corridor. Robots paused.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsHuman(other)) return;

        humansInside = Mathf.Max(0, humansInside - 1);
        if (humansInside == 0)
        {
            SetAllAgentsBlocked(false);
            Debug.Log("✅ Corridor clear. Robots resumed.");
        }
    }   
}

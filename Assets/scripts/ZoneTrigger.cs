using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    public string zoneName;

    private void OnTriggerEnter(Collider other)
    {
        AgentController agent = other.GetComponent<AgentController>();
        if (agent != null)
        {
            agent.TryEnterZone(zoneName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AgentController agent = other.GetComponent<AgentController>();
        if (agent != null)
        {
            agent.ExitZone(zoneName);
        }
    }
}

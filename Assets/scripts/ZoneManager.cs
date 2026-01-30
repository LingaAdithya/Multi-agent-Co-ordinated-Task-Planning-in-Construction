using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    private Dictionary<string, GameObject> zoneOwner = new();

    public bool CanEnter(string zoneName)
    {
        return !zoneOwner.ContainsKey(zoneName);
    }

    public void EnterZone(string zoneName, GameObject agent)
    {
        if (!zoneOwner.ContainsKey(zoneName))
        {
            zoneOwner[zoneName] = agent;
        }
    }

    public void ExitZone(string zoneName, GameObject agent)
    {
        if (zoneOwner.ContainsKey(zoneName) && zoneOwner[zoneName] == agent)
        {
            zoneOwner.Remove(zoneName);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zone : MonoBehaviour
{
    [Serializable]
    public class ZoneData
    {
        public string name; // Name of the zone
        public GameObject zone; // Reference to the zone object
    }

    public List<ZoneData> safeZones = new List<ZoneData>();
    public List<ZoneData> unsafeZones = new List<ZoneData>();

    public GameObject GetSafeZone(string zoneName)
    {
        foreach (ZoneData data in safeZones)
        {
            if (data.name == zoneName)
                return data.zone;
        }
        return null;
    }

    public GameObject GetUnsafeZone(string zoneName)
    {
        foreach (ZoneData data in unsafeZones)
        {
            if (data.name == zoneName)
                return data.zone;
        }
        return null;
    }
}


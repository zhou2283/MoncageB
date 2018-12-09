
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DisableOtherZones : MonoBehaviour
{
    private static List<GameObject> zoneList = new List<GameObject>();

    [MenuItem("Tools/DisableOtherZones %h")]
    public static void change()
    {
        zoneList.Clear();
        for (int i = 0; i < 5; i++)
        {
            zoneList.Add(GameObject.Find("ZoneGroup").transform.Find("Zone" + i.ToString()).gameObject);
        }

        foreach (GameObject child in zoneList)
        {
            child.SetActive(false);
        }
        GameObject[] currentZone = Selection.gameObjects;
        foreach (GameObject child in currentZone)
        {
            child.SetActive(true);
        }
    }
}

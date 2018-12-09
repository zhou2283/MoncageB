using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using WebSocketSharp;

public class SetZoneLayersAndTags : MonoBehaviour
{
	
	[MenuItem("Tools/SetZoneLayers")]
	public static void change()
	{
		for (int i = 0; i < 5; i++)
		{
			Transform zone = GameObject.Find("ZoneGroup").transform.Find("Zone" + i.ToString()).gameObject.transform;
			foreach (Transform child in zone)
			{
				if (child.GetComponent<SceneBase>() == null)
				{
					child.gameObject.AddComponent<SceneBase>();
				}
			}
			foreach (Transform child in zone.GetComponentsInChildren<Transform>())
			{
				child.gameObject.layer = LayerMask.NameToLayer("Zone" + i.ToString());
				if (child.name.Contains("_CNT_"))
				{
					child.tag = "CNT";
				}
				else if (child.name.Contains("_ANM_"))
				{
					child.tag = "ANM";
				}
			}
		}
	}
}

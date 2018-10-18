using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlaneMask : MonoBehaviour {

    [SerializeField]
    int maskIndex = 1;

    List<Renderer> renderListArray = new List<Renderer>();

    ZoneGroupManager zoneGroupManager;


	// Use this for initialization
	void Start () {


        zoneGroupManager = GameObject.Find("ZoneGroup").GetComponent<ZoneGroupManager>();
        //print(zoneGroupManager.name);
        Renderer[] renderers = zoneGroupManager.zoneArray[maskIndex].GetComponentsInChildren<Renderer>(true);
        //print(renderers.Length);
        foreach(Renderer child in renderers)
        {
            renderListArray.Add(child);
        }
        Vector3 pos = transform.position;
        Vector3 normal = -transform.forward;
        for (int i = 0; i < renderListArray.Count; i++)
        {
            renderListArray[i].material.SetVector("_pV", new Vector4(pos.x,pos.y,pos.z,0));
            renderListArray[i].material.SetVector("_pN", new Vector4(normal.x, normal.y, normal.z, 0));
        }
    }
	
	// Update is called once per frame
	void Update () {

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneGroupManager : MonoBehaviour {

    //all children transform
    public Transform[] maskArray = new Transform[5];//index 0 is empty, for convience to recognize.
    public Transform[] zoneArray = new Transform[5];//index 0 is empty, for convience to recognize.

	// Use this for initialization
	void Start () {
        //get all children transform
        for (int i = 1; i <= 4; i++)
        {
            maskArray[i] = transform.Find("Mask" + i.ToString());
            zoneArray[i] = transform.Find("Zone" + i.ToString());
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

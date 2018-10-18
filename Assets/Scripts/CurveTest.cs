using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveTest : MonoBehaviour
{
	public BezierCurve curveScript;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = curveScript.GetPointAt(Mathf.PingPong(Time.time,1f));
	}
}

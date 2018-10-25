using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestFunction : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Keypad0))
		{
			GameObject.Find("Zone0").transform.GetComponentInChildren<SceneBase>().MoveToNextState();
		}

		if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			GameObject.Find("Zone1").transform.GetComponentInChildren<SceneBase>().MoveToNextState();
		}
		
		if (Input.GetKeyDown(KeyCode.Keypad2))
		{
			GameObject.Find("Zone2").transform.GetComponentInChildren<SceneBase>().MoveToNextState();
		}
		
		if (Input.GetKeyDown(KeyCode.Keypad3))
		{
			GameObject.Find("Zone3").transform.GetComponentInChildren<SceneBase>().MoveToNextState();
		}
		
		if (Input.GetKeyDown(KeyCode.Keypad4))
		{
			GameObject.Find("Zone4").transform.GetComponentInChildren<SceneBase>().MoveToNextState();
		}
	}
}

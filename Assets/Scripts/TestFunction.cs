using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using  UnityEngine.SceneManagement;

public class TestFunction : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.P))
		{
			GameObject.Find("SceneE1").GetComponent<Animator>().SetInteger("Status",1);
			GameObject.Find("_CNT_PlankInHandle").GetComponent<Animator>().SetInteger("Status",1);
		}
		
		if (Input.GetKeyDown(KeyCode.O))
		{
			GameObject.Find("SceneE1").GetComponent<Animator>().SetInteger("Status",0);
			GameObject.Find("_CNT_PlankInHandle").GetComponent<Animator>().SetInteger("Status",0);
		}
		
		
		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		
		if (Input.GetKeyDown(KeyCode.Keypad0))
		{
			GameObject.Find("Zone0").transform.GetComponentInChildren<SceneBase>(false).MoveToNextState();
		}

		if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			GameObject.Find("Zone1").transform.GetComponentInChildren<SceneBase>(false).MoveToNextState();
		}
		
		if (Input.GetKeyDown(KeyCode.Keypad2))
		{
			GameObject.Find("Zone2").transform.GetComponentInChildren<SceneBase>(false).MoveToNextState();
		}
		
		if (Input.GetKeyDown(KeyCode.Keypad3))
		{
			GameObject.Find("Zone3").transform.GetComponentInChildren<SceneBase>(false).MoveToNextState();
		}
		
		if (Input.GetKeyDown(KeyCode.Keypad4))
		{
			GameObject.Find("Zone4").transform.GetComponentInChildren<SceneBase>(false).MoveToNextState();
		}

		
		if (Input.GetKeyDown(KeyCode.Alpha0))
		{
			GameObject.Find("ZoneGroup").transform.Find("Mask0").gameObject.SetActive(true);
		}
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			GameObject.Find("ZoneGroup").transform.Find("Mask1").gameObject.SetActive(true);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			GameObject.Find("ZoneGroup").transform.Find("Mask2").gameObject.SetActive(true);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			GameObject.Find("ZoneGroup").transform.Find("Mask3").gameObject.SetActive(true);
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			GameObject.Find("ZoneGroup").transform.Find("Mask4").gameObject.SetActive(true);
		}
	}
}

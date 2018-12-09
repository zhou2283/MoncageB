﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MP1_WireAndWire_3_4 : MarkPositionBase
{
	[Header("Extra Part")]
	public GameObject light;
	public GameObject panel;
	
	[Header("Affected ANM")]
	public GameObject _ANM_Fan;

	public GameObject _ANM_SmallBox;

	[Header("Affected MRK")] 
	public GameObject mp2;

	private void Start()
	{
		
		GameObject[] _ANM_List = GameObject.FindGameObjectsWithTag("ANM");
		foreach (GameObject child in _ANM_List)
		{
			if (child.name == "_ANM_Fan")
			{
				_ANM_Fan = child;
			}
			else if (child.name == "_ANM_SmallBox")
			{
				_ANM_SmallBox = child;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

    public override float DoEvent()//return the wait time
    {
	    if (phase == 0)
	    {
		    print(transform.name + " IN");
		    
		    light.GetComponent<MeshRenderer>().material.DOVector(new Vector4(0, 1, 0, 0), "_Emission", 0.5f);
		    panel.GetComponent<MeshRenderer>().material.DOVector(new Vector4(0, 1, 0, 0), "_Emission", 0.5f);
		    _ANM_Fan.GetComponent<Animator>().SetInteger("Status",1);
		    _ANM_SmallBox.GetComponent<Animator>().SetInteger("Status",1);
		    mp2.gameObject.SetActive(true);

		    phase = 1;
		    gameObject.SetActive(false);
		    
		    
		    return 0.0f;
	    }
	    else if(phase == 1)
	    {
		    return 0.0f;
	    }
	    else
	    {
		    return 0.0f;
	    }

    }


}
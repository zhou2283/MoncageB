using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MP3_CenterPivotAndWellLifter_3_4 : MarkPositionBase
{
	[Header("Extra Part")]

	[Header("Affected ANM")]
	public GameObject _ANM_CenterPillar;

	public GameObject _ANM_WellLifter;

	[Header("Affected MRK")] 
	public GameObject mp4;

	[Header("Affected Scene")] 
	public GameObject sceneE1;
	
	
	private void Start()
	{
		
		GameObject[] _ANM_List = GameObject.FindGameObjectsWithTag("ANM");
		foreach (GameObject child in _ANM_List)
		{
			if (child.name == "_ANM_CenterPillar")
			{
				_ANM_CenterPillar = child;
			}
			else if (child.name == "_ANM_WellLifter")
			{
				_ANM_WellLifter = child;
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


		    
		    _ANM_CenterPillar.GetComponent<Animator>().SetInteger("Status",1);
		    _ANM_WellLifter.GetComponent<Animator>().SetInteger("Status",1);
		    phase = 1;
		    sceneE1.SetActive(true);
		    mp4.SetActive(true);
		    
		    gameObject.SetActive(false);
		    return 2.0f;
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

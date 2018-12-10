using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MP7_RopeAndLift_1_4 : MarkPositionBase
{
	[Header("Extra Part")] 
	
	[Header("Affected ANM")]
	public GameObject _ANM_RopeAndWeightGroup;

	public GameObject _ANM_LabBuilding;

	[Header("Affected MRK")] 
	public GameObject mp5;
	public GameObject mp6;

	[Header("Affected Scene")] 
	public GameObject sceneA1;
	public GameObject sceneA2;
	
	
	private void Start()
	{
		
		GameObject[] _ANM_List = GameObject.FindGameObjectsWithTag("ANM");
		foreach (GameObject child in _ANM_List)
		{
			if (child.name == "_ANM_RopeAndWeightGroup")
			{
				_ANM_RopeAndWeightGroup = child;
			}
			else if (child.name == "_ANM_LabBuilding")
			{
				_ANM_LabBuilding = child;
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

		    _ANM_RopeAndWeightGroup.GetComponent<Animator>().SetInteger("Status",1);
		    _ANM_LabBuilding.GetComponent<Animator>().SetInteger("Status",1);
		    phase = 1;
		    sceneA1.SetActive(false);
		    sceneA2.SetActive(true);
		    
		    
		    return 5.0f;
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
	
	public IEnumerator DelayToAnimateDoor(float delay) {
		
		yield return new WaitForSeconds(delay);
		gameObject.SetActive(false);
 
	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MP4_DoorAndPlank_0_3 : MarkPositionBase
{
	[Header("Extra Part")]

	[Header("Affected ANM")]
	public GameObject _ANM_PlankInHandle;

	public GameObject _ANM_PlankGroup;
	
	public GameObject _ANM_DoorToIsland;

	[Header("Affected MRK")] 
	public GameObject mp5;
	public GameObject mp6;

	[Header("Affected Scene")] 
	public GameObject sceneD1;
	
	
	private void Start()
	{
		
		GameObject[] _ANM_List = GameObject.FindGameObjectsWithTag("ANM");
		foreach (GameObject child in _ANM_List)
		{
			if (child.name == "_ANM_PlankInHandle")
			{
				_ANM_PlankInHandle = child;
			}
			else if (child.name == "_ANM_PlankGroup")
			{
				_ANM_PlankGroup = child;
			}
			else if (child.name == "_ANM_DoorToIsland")
			{
				_ANM_DoorToIsland = child;
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


		    
		    _ANM_PlankInHandle.GetComponent<Animator>().SetInteger("Status",1);
		    _ANM_PlankGroup.GetComponent<Animator>().SetInteger("Status",1);
		   
		    mp5.gameObject.SetActive(true);
		    mp6.gameObject.SetActive(true);
		    StartCoroutine(DelayToAnimateDoor(1f));
		    
		    GameObject.Find("CameraControl").GetComponent<CameraControl>().ChangeCamera(new Vector3(19f,152f,0f), 1f, 0.5f);
		    phase = 1;
		    sceneD1.SetActive(true);
		    
		    
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
	
	public IEnumerator DelayToAnimateDoor(float delay) {
		
		yield return new WaitForSeconds(delay);
		_ANM_DoorToIsland.GetComponent<Animator>().SetInteger("Status",1);
		gameObject.SetActive(false);
 
	}


}

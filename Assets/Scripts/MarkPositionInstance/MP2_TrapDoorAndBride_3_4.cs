using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MP2_TrapDoorAndBride_3_4 : MarkPositionBase
{
	[Header("Extra Part")]

	[Header("Affected ANM")]
	public GameObject _ANM_TrapDoor;

	public GameObject _ANM_Bridge;

	public GameObject _ANM_TrunkToFactory;

	[Header("Affected MRK")] 
	public GameObject mp3;

	[Header("Affected Scene")] 
	public GameObject sceneC1;
	
	
	private void Start()
	{
		
		GameObject[] _ANM_List = GameObject.FindGameObjectsWithTag("ANM");
		foreach (GameObject child in _ANM_List)
		{
			if (child.name == "_ANM_TrapDoor")
			{
				_ANM_TrapDoor = child;
			}
			else if (child.name == "_ANM_Bridge")
			{
				_ANM_Bridge = child;
			}
			else if (child.name == "_ANM_TrunkToFactory")
			{
				_ANM_TrunkToFactory = child;
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


		    
		    _ANM_TrapDoor.GetComponent<Animator>().SetInteger("Status",1);
		    _ANM_Bridge.GetComponent<Animator>().SetInteger("Status",1);
		    _ANM_TrunkToFactory.GetComponent<Animator>().SetInteger("Status",1);
			GameObject.Find("CameraControl").GetComponent<CameraControl>().ChangeCamera(new Vector3(9f,100f,0f), 5f);
		    GameObject.Find("SceneB1").GetComponent<SceneBase>().MoveToState(1,4f,1f);
		    phase = 1;
		    sceneC1.SetActive(true);
		    mp3.SetActive(true);
		    
		    gameObject.SetActive(false);
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


}

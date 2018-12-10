using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MP5_WireAndWireAndWire_0_1_4 : MarkPositionBase
{
	[Header("Extra Part")] 
	
	[Header("Affected ANM")]
	public GameObject _ANM_TransitMachineGroup;

	[Header("Affected MRK")] public GameObject mp7;

	[Header("Affected Scene")] 
	public GameObject sceneD1;
	
	
	private void Start()
	{
		
		GameObject[] _ANM_List = GameObject.FindGameObjectsWithTag("ANM");
		foreach (GameObject child in _ANM_List)
		{
			if (child.name == "_ANM_TransitMachineGroup")
			{
				_ANM_TransitMachineGroup = child;
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

		    StartCoroutine(DelayToAnimateDoor(1f));
		    _ANM_TransitMachineGroup.GetComponent<Animator>().SetInteger("Status",1);
		    GameObject.Find("SceneC1").GetComponent<SceneBase>().MoveToState(1,4f,0f);
		    GameObject.Find("CameraControl").GetComponent<CameraControl>().ChangeCamera(new Vector3(7.6f,-1.2f,0f), 4f, 0f);
		    mp7.SetActive(true);
		    phase = 1;
		    sceneD1.SetActive(true);
		    
		    
		    return 8.0f;
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

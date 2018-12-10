using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MP6_TireAndTire_0_2 : MarkPositionBase
{
	[Header("Extra Part")] 
	
	[Header("Affected ANM")]
	public GameObject _ANM_Tire;

	public GameObject _ANM_TireInWell;

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
			if (child.name == "_ANM_Tire")
			{
				_ANM_Tire = child;
			}
			else if (child.name == "_ANM_TireInWell")
			{
				_ANM_TireInWell = child;
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
		    _ANM_Tire.GetComponent<Animator>().SetInteger("Status",1);
		    _ANM_TireInWell.GetComponent<Animator>().SetInteger("Status",1);
		    GameObject.Find("SceneD1").GetComponent<SceneBase>().MoveToState(1,4f,0f);
		    GameObject.Find("CameraControl").GetComponent<CameraControl>().ChangeCamera(new Vector3(14f,-100f,0f), 3f, 0.5f);
		    phase = 1;
		    sceneD1.SetActive(true);
		    
		    
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

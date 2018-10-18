using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MarkPosition02 : MarkPositionBase
{
	// Update is called once per frame
	void Update () {
		
	}

    public override float DoEvent()//return the wait time
    {
	    if (phase == 0)
	    {
		    GameObject.Find("trapDoor").GetComponent<AnimatePartBase>().EnableIsConnectActive();
		    exitIsActive = true;
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

    public override void DoExitEvent()
    {
	    if (phase == 0)
	    {
		    if (exitIsActive && transform.gameObject.activeSelf)
		    {
			    exitIsActive = false;
			    GameObject.Find("trapDoor").GetComponent<AnimatePartBase>().DisableIsConnectActive();
		    }      
	    }
	    else if(phase == 1)
	    {
		    
	    }
	    else
	    {
		    
	    }
  
    }
}

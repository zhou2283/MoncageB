using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MarkPosition03 : MarkPositionBase
{
	// Update is called once per frame
	void Update () {
		
	}

    public override float DoEvent()//return the wait time
    {
	    if (phase == 0)
	    {
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
 
	    }
	    else if(phase == 1)
	    {
		    
	    }
	    else
	    {
		    
	    }
  
    }
}

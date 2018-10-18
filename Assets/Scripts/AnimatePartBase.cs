using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class AnimatePartBase : MonoBehaviour {

    public int phase = 0;
	public bool isConnectActive = false;//only used for connect event
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual int DoEvent()
	{
		return GameConst.INTERACTION_STATE_INVALID;
	}
	
	public virtual void EnableIsConnectActive()
	{
		isConnectActive = true;
	}

	public virtual void DisableIsConnectActive()
	{
		isConnectActive = false;
	}

	
    public void SetPhase(int _phase)//used for reset
    {
        phase = _phase;
        switch (phase)
        {
            case 0:
                break;
            default:
                break;
        }
    }
	
	

    
}

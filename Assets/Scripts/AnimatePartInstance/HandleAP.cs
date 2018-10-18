using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandleAP : AnimatePartBase
{
    public GameObject light;


    public override int DoEvent()
    {
        if (phase == 0)
        {
            if (isConnectActive)
            {
                phase = 1;
                light.GetComponent<MeshRenderer>().material.DOVector(new Vector4(1, 0, 0, 0), "_Emission", 0.2f);
                transform.parent.DORotate(new Vector3(-30, 60, 0), 0.2f);
                GameObject.Find("M-01-TestMarkPosition-3/4").GetComponent<MarkPositionBase>().phase = 1;
                GameObject.Find("UnderGear").GetComponent<SelfRotation>().enabled = true;
                return GameConst.INTERACTION_STATE_VALID;
            }
            else
            {
                return GameConst.INTERACTION_STATE_INVALID;
            }
        }
        else if (phase == 1)
        {
            return GameConst.INTERACTION_STATE_INVALID;
        }
        else
        {
            return GameConst.INTERACTION_STATE_INVALID;
        }
        
    }

}

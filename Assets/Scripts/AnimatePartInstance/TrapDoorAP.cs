using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrapDoorAP : AnimatePartBase {
    
    public override int DoEvent()
    {
        if (phase == 0)
        {
            if (isConnectActive)
            {
                transform.DOLocalMove(new Vector3(0.0664f,-0.1232f,-0.0603f), 0.5f);
                GameObject.Find("BridgeInDesert").transform.DOLocalMove(new Vector3(-0.3f,-9.64f,-7.2f), 0.5f);
                phase = 1;
                return GameConst.INTERACTION_STATE_VALID;
            }
            else
            {
                return GameConst.INTERACTION_STATE_INVALID;
            }
        }
        else if (phase == 1)
        {
            if (isConnectActive)
            {
                transform.DOLocalMove(new Vector3(0.1f,-0.1232f,-0.005f), 0.5f);
                GameObject.Find("BridgeInDesert").transform.DOLocalMove(new Vector3(1.19f,-9.64f,-4.6f), 0.5f);
                phase = 0;
                return GameConst.INTERACTION_STATE_VALID;
            }
            else
            {
                return GameConst.INTERACTION_STATE_INVALID;
            }
        }
        else
        {
            return GameConst.INTERACTION_STATE_INVALID;
        }
        
    }

}
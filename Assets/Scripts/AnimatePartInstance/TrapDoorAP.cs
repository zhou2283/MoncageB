using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrapDoorAP : AnimatePartBase
{
    public MarkPositionBase[] markPosArray;
    
    public override int DoEvent()
    {
        if (phase == 0)
        {
            if (isConnectActive)
            {
                transform.DOLocalMove(new Vector3(0.0664f,-0.1232f,-0.0603f), 0.5f);
                GameObject.Find("BridgeInDesert").transform.DOLocalMove(new Vector3(-0.3f,-9.64f,-7.2f), 0.5f);
                phase = 1;
                
                GameObject.Find("CameraControl").GetComponent<CameraControl>().ChangeCamera(new Vector3(17f,98f,0), 2f);
                GameObject.Find("DesertTest").GetComponent<SceneBase>().ChangeMoveState(1,2f,2f);
                
                foreach (var child in markPosArray)
                {
                    child.transform.gameObject.SetActive(false);
                }
                
                return GameConst.INTERACTION_STATE_VALID;
            }
            else
            {
                transform.DOShakePosition(0.2f, 0.005f, 50, 90f);
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
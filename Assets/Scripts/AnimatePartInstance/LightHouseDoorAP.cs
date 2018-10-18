using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightHouseDoorAP : AnimatePartBase
{


    private void Start()
    {
    }

    public override int DoEvent()
    {
        if (phase == 0)
        {
            phase = 1;
            transform.DOScale(0, 0.5f);
            GameObject.Find("Zone2").transform.Find("LightHouseOutdoor").gameObject.SetActive(true);
            GameObject.Find("ZoneGroup").transform.Find("Mask2").gameObject.SetActive(true);
            GameObject.Find("DoorWhiteEmission").transform.Find("DoorOpenEffect").gameObject.SetActive(true);
            return GameConst.INTERACTION_STATE_VALID;
        }
        else if(phase == 1)
        {
            return GameConst.INTERACTION_STATE_INVALID;
        }
        else
        {
            return GameConst.INTERACTION_STATE_INVALID;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WrenchAP : AnimatePartBase
{

    private ToolsControl toolsControlScript;

    private void Start()
    {
        toolsControlScript = GameObject.Find("ToolsControl").GetComponent<ToolsControl>();
    }

    public override int DoEvent()
    {
        if (phase == 0)
        {
            if (toolsControlScript.NeedItemIsInList(gameObject.name))
            {
                toolsControlScript.FindItem(gameObject);
                return GameConst.INTERACTION_STATE_FIND;
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

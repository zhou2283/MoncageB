using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MarkPositionBase : MonoBehaviour
{
    public float snapDist = 2f;
    [Header("Sub Mark Position Part")]
    public bool hasSubMarkPosition = false;
    public Vector2 subMarkPositionRotation = Vector2.zero;
    [Range(1,4)]
    public int connectedLeftFaceIndex = 1;
    [HideInInspector]
    public int connectedRightFaceIndex = 2;
    [HideInInspector]
    public bool exitIsActive = false;
    [HideInInspector]
    public int phase = 0;//it is for different animations for different phases

    protected ZoneGroupManager zoneGroupManager;
    protected Transform maskLeft;
    protected Transform maskRight;

    private void Start()
    {
        zoneGroupManager = GameObject.Find("ZoneGroup").GetComponent<ZoneGroupManager>();
        
        if (connectedLeftFaceIndex == 4)
        {
            connectedRightFaceIndex = 1;
        }
        else
        {
            connectedRightFaceIndex = connectedLeftFaceIndex + 1;
        }

        maskLeft = zoneGroupManager.maskArray[connectedLeftFaceIndex];
        maskRight = zoneGroupManager.maskArray[connectedRightFaceIndex];
        
    }

    public Vector2 GetRotation()//returns only x and y of the eulars
    {
        return new Vector2(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y);
    }

    public void DoMaskTransitionIn()
    {
        maskLeft.DOScaleX(24.8f, GameConst.CAMERA_SNAP_DURATION);
        maskRight.DOScaleX(24.8f, GameConst.CAMERA_SNAP_DURATION);
        print(maskLeft.name);
    }
    public void DoMaskTransitionOut()
    {
        maskLeft.DOScaleX(24.5f, 0.2f);
        maskRight.DOScaleX(24.5f, 0.2f);
    }


    public virtual float DoEvent()//return the wait time
    {
        //active exit function
        exitIsActive = true;
        //change phase
        return 0f;
    }

    public virtual void DoExitEvent()
    {
        if (exitIsActive)
        {
            //revert part
        }
    }

    public virtual void DoEffect()
    {

    }

    public virtual void DoAnimation()
    {

    }

    public virtual void RenewMarkPositionGroup()
    {

    }

    public virtual void RenewInteractiveItems()
    {

    }

    public virtual void SetPhase(int _phase)//used for reset
    {
        phase = _phase;
        //extra things to do?
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityScript.Lang;

public class MarkPositionBase : MonoBehaviour
{
    public float snapDist = 2f;
    [Header("Sub Mark Position Part")]
    public bool hasSubMarkPosition = false;
    public Vector2 subMarkPositionRotation = Vector2.zero;
    [Header("Connect Face Index")]
    [Range(0,4)]
    public int connectedFaceIndexA = 1;
    [Range(0,4)]
    public int connectedFaceIndexB = 2;
    public bool connectThreeFaces = false;
    [Range(0,4)]
    public int connectedFaceIndexC = 0;

    [HideInInspector] public List<int> sortedFaceIndexList = new List<int>();

    [HideInInspector]
    public bool exitIsActive = false;
    [HideInInspector]
    public int phase = 0;//it is for different animations for different phases

    protected ZoneGroupManager zoneGroupManager;
    protected Transform maskA;
    protected Transform maskB;
    protected Transform maskC;

    private void Start()
    {
        zoneGroupManager = GameObject.Find("ZoneGroup").GetComponent<ZoneGroupManager>();
        if (SortIndex())
        {
            if (!connectThreeFaces)
            {
                maskA = zoneGroupManager.maskArray[sortedFaceIndexList[0]];
                maskB = zoneGroupManager.maskArray[sortedFaceIndexList[1]];
                maskC = null;
            }
            else
            {
                maskA = zoneGroupManager.maskArray[sortedFaceIndexList[0]];
                maskB = zoneGroupManager.maskArray[sortedFaceIndexList[1]];
                maskC = zoneGroupManager.maskArray[sortedFaceIndexList[2]];
            }
        }
        
        


        
    }

    public Vector2 GetRotation()//returns only x and y of the eulars
    {
        return new Vector2(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y);
    }

    public void DoMaskTransitionIn()
    {
        if (!connectThreeFaces)
        {
            if (sortedFaceIndexList[0] == 0)
            {
                if (sortedFaceIndexList[1] == 2 || sortedFaceIndexList[1] == 4)
                {
                    maskA.DOScaleZ(24.8f, GameConst.CAMERA_SNAP_DURATION);
                }
                else
                {
                    maskA.DOScaleX(24.8f, GameConst.CAMERA_SNAP_DURATION);
                }
                maskB.DOScaleY(24.8f, GameConst.CAMERA_SNAP_DURATION);
            }
            else
            {
                maskA.DOScaleX(24.8f, GameConst.CAMERA_SNAP_DURATION);
                maskB.DOScaleX(24.8f, GameConst.CAMERA_SNAP_DURATION);
            }

        }
        else
        {
            maskA.DOScaleX(24.8f, GameConst.CAMERA_SNAP_DURATION);
            maskA.DOScaleY(24.8f, GameConst.CAMERA_SNAP_DURATION);
            maskB.DOScaleX(24.8f, GameConst.CAMERA_SNAP_DURATION);
            maskB.DOScaleY(24.8f, GameConst.CAMERA_SNAP_DURATION);
            maskC.DOScaleX(24.8f, GameConst.CAMERA_SNAP_DURATION);
            maskC.DOScaleY(24.8f, GameConst.CAMERA_SNAP_DURATION);
        }
    }
    public void DoMaskTransitionOut()
    {
        if (!connectThreeFaces)
        {
            if (sortedFaceIndexList[0] == 0)
            {
                if (sortedFaceIndexList[1] == 2 || sortedFaceIndexList[1] == 4)
                {
                    maskA.DOScaleZ(24.5f, GameConst.CAMERA_SNAP_DURATION);
                }
                else
                {
                    maskA.DOScaleX(24.5f, GameConst.CAMERA_SNAP_DURATION);
                }
                maskB.DOScaleY(24.5f, GameConst.CAMERA_SNAP_DURATION);
            }
            else
            {
                maskA.DOScaleX(24.5f, GameConst.CAMERA_SNAP_DURATION);
                maskB.DOScaleX(24.5f, GameConst.CAMERA_SNAP_DURATION);
            }

        }
        else
        {
            maskA.DOScaleX(24.5f, GameConst.CAMERA_SNAP_DURATION);
            maskA.DOScaleY(24.5f, GameConst.CAMERA_SNAP_DURATION);
            maskB.DOScaleX(24.5f, GameConst.CAMERA_SNAP_DURATION);
            maskB.DOScaleY(24.5f, GameConst.CAMERA_SNAP_DURATION);
            maskC.DOScaleX(24.5f, GameConst.CAMERA_SNAP_DURATION);
            maskC.DOScaleY(24.5f, GameConst.CAMERA_SNAP_DURATION);
        }
    }

    bool SortIndex()
    {
        if (!connectThreeFaces)
        {
            if (connectedFaceIndexA == connectedFaceIndexB)
            {
                Debug.Log("Error: " + transform.name + "has the same connected faces");
                return false;
            }
            else
            {
                sortedFaceIndexList.Add(connectedFaceIndexA);
                sortedFaceIndexList.Add(connectedFaceIndexB);
                sortedFaceIndexList.Sort();

                if (sortedFaceIndexList[0] == 1 && sortedFaceIndexList[1] == 3||
                    sortedFaceIndexList[0] == 2 && sortedFaceIndexList[1] == 4)
                {
                    Debug.Log("Error: " + transform.name + "has the opposite connected faces");
                    return false;
                }
            }
        }
        else
        {
            if (connectedFaceIndexA == connectedFaceIndexB || connectedFaceIndexB == connectedFaceIndexC ||
                connectedFaceIndexC == connectedFaceIndexA)
            {
                Debug.Log("Error: " + transform.name + "has the same connected faces");
                return false;
            }
            else
            {
                sortedFaceIndexList.Add(connectedFaceIndexA);
                sortedFaceIndexList.Add(connectedFaceIndexB);
                sortedFaceIndexList.Add(connectedFaceIndexC);
                sortedFaceIndexList.Sort();
                if (sortedFaceIndexList[0] != 0)
                {
                    Debug.Log("Error: " + transform.name + "has the opposite connected faces");
                    return false;
                }
                else
                {
                    if (sortedFaceIndexList[1] == 1 && sortedFaceIndexList[2] == 3||
                        sortedFaceIndexList[1] == 2 && sortedFaceIndexList[2] == 4)
                    {
                        Debug.Log("Error: " + transform.name + "has the opposite connected faces");
                        return false;
                    }
                }
            }
        }

        return true;
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

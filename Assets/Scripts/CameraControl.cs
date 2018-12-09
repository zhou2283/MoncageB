using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraControl : MonoBehaviour {

    //all children transform
    Transform cameraPivotV;
    Transform cameraPivotH;
    Transform cameraGroup;

    //all connected script
    MarkPositionGroupManager markPositionGroupManager;
    ZoneGroupManager zoneGroupManager;

    //snap part
    Tweener snapTweenerV;
    Tweener snapTweenerH;
    MarkPositionBase targetMarkPositionScript;
    

    //max rotate speed
    [SerializeField] 
    float speedV = 4;
    [SerializeField] 
    float speedH = 4;
    

    //mouse delta move
    //also used for multiply
    float fMouseX = 0;//speedH multiply
    float fMouseY = 0;//speedV multiply
    //current rotate speed multiply
    float rotateSpeedMultiply = 0f;
    [SerializeField]
    float minRotateSpeedMultiplyForSnap = 0.02f;

    //boarder buffer var
    float maxUpAngle = 85f;
    float maxUpAngleWithBuffer = 90f;
    float maxDownAngle = -9f;
    float maxDownAngleWithBuffer = -14f;
    
    //all bools
    bool isBounceBack = false;
    bool isSnapping = false;

    // Use this for initialization
    void Start () {
        SaveManager.Instance.i++;
        //get all children transform
        cameraPivotH = transform.GetChild(0);
        cameraPivotV = cameraPivotH.GetChild(0);
        cameraGroup = cameraPivotV.GetChild(0);

        //get all connected script
        markPositionGroupManager = GameObject.Find("MarkPositionGroup").GetComponent<MarkPositionGroupManager>();
        zoneGroupManager = GameObject.Find("ZoneGroup").GetComponent<ZoneGroupManager>();
    }
	
	// Update is called once per frame
	void Update () {
        //move part
	    if (Input.GetKey(KeyCode.Space))
	    {
	        speedV = 0.5f;
	        speedH = 0.5f;
	    }
	    else
	    {
	        speedV = 4f;
	        speedH = 4f;
	    }
        if (Input.GetMouseButton(1) && GameControlGlobal.Instance.INTERACTION_IS_ACTIVE)
        {
            fMouseX = Input.GetAxis("Mouse X");
            fMouseY = Input.GetAxis("Mouse Y");

            if (targetMarkPositionScript != null)
            {
                DoTargetExitEvent();
                targetMarkPositionScript = null;
            }

            isSnapping = false;
            snapTweenerV.Kill();
            snapTweenerH.Kill();
        }
        else
        {
            fMouseX = Mathf.Lerp(fMouseX, 0, Time.deltaTime * 5f);
            fMouseY = Mathf.Lerp(fMouseY, 0, Time.deltaTime * 5f);


            rotateSpeedMultiply = Mathf.Sqrt(fMouseX * fMouseX + fMouseY + fMouseY);
            if (rotateSpeedMultiply < minRotateSpeedMultiplyForSnap && !isSnapping)
            {
                if (CheckAndSnapCamera())
                {
                    isSnapping = true;
                }                
            }
        }


        //buffer area part
        if (NormalizeEuler(cameraPivotV.localRotation.eulerAngles.x) > maxUpAngle)
        {
            
            if (Input.GetMouseButton(1))
            {
                isBounceBack = false;
                //speedV *= Mathf.Pow((maxUpAngleWithBuffer - NormalizeEuler(cameraPivotV.localRotation.eulerAngles.x)) / (maxUpAngleWithBuffer - maxUpAngle), 4);
                fMouseY *= Mathf.Pow((maxUpAngleWithBuffer - NormalizeEuler(cameraPivotV.localRotation.eulerAngles.x)) / (maxUpAngleWithBuffer - maxUpAngle), 4);
            }
            else
            {
                fMouseY = 0;
                cameraPivotV.Rotate(Vector3.right, -(NormalizeEuler(cameraPivotV.localRotation.eulerAngles.x) - maxUpAngle) / 20f, Space.Self);                
            }
        }
        else if (NormalizeEuler(cameraPivotV.localRotation.eulerAngles.x) < maxDownAngle)
        {
            if (Input.GetMouseButton(1))
            {
                isBounceBack = false;
                //speedV *= Mathf.Pow((maxDownAngleWithBuffer - NormalizeEuler(cameraPivotV.localRotation.eulerAngles.x)) / (maxDownAngleWithBuffer - maxDownAngle), 4);
                fMouseY *= Mathf.Pow((maxDownAngleWithBuffer - NormalizeEuler(cameraPivotV.localRotation.eulerAngles.x)) / (maxDownAngleWithBuffer - maxDownAngle), 4);
            }
            else
            {
                fMouseY = 0;
                cameraPivotV.Rotate(Vector3.right, -(NormalizeEuler(cameraPivotV.localRotation.eulerAngles.x) - maxDownAngle) / 20f, Space.Self);
            }
        }
        else
        {
            isBounceBack = false;
        }

        //final rotate part
        if (!isBounceBack)
        {
            cameraPivotV.Rotate(Vector3.right, -fMouseY * speedV, Space.Self);
        }
        cameraPivotH.Rotate(Vector3.up, fMouseX * speedH, Space.Self);

    }

    public float NormalizeEuler(float value)
    {
        float angle = value - 180;
        if (angle > 0)
            return angle - 180;
        return angle + 180;
    }

    //check and snap camera
    public bool CheckAndSnapCamera()
    {
        bool canSnap = false;
        targetMarkPositionScript = markPositionGroupManager.CheckForCameraSnapTarget(cameraPivotV.rotation.eulerAngles.x, cameraPivotH.rotation.eulerAngles.y);
        if (targetMarkPositionScript != null)
        {
            targetMarkPositionScript.DoMaskTransitionIn();
            
            snapTweenerV = cameraPivotV.DOLocalRotate(new Vector3(targetMarkPositionScript.GetRotation().x, 0, 0), GameConst.CAMERA_SNAP_DURATION);
            snapTweenerH = cameraPivotH.DOLocalRotate(new Vector3(0, targetMarkPositionScript.GetRotation().y, 0), GameConst.CAMERA_SNAP_DURATION);
            snapTweenerV.OnComplete(DoTargetEvent);
            canSnap = true;
        }
        return canSnap;
    }

    public void ChangeCamera(Vector3 cameraRotation, float duration)
    {
        cameraPivotV.DOLocalRotate(new Vector3(cameraRotation.x, 0, 0), duration).SetEase(Ease.InOutCubic);
        cameraPivotH.DOLocalRotate(new Vector3(0, cameraRotation.y, 0), duration).SetEase(Ease.InOutCubic);
    }

    void DoTargetEvent()
    {
        float delay = 0f;
        delay = targetMarkPositionScript.DoEvent();
        DisactiveRotate(delay);
    }

    void DoTargetExitEvent()
    {
        targetMarkPositionScript.DoExitEvent();
        targetMarkPositionScript.DoMaskTransitionOut();
    }

    void DisactiveRotate(float duration)
    {
        GameControlGlobal.Instance.INTERACTION_IS_ACTIVE = false;
        StartCoroutine(DelayToActiveRotate(duration));
    }
    IEnumerator DelayToActiveRotate(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameControlGlobal.Instance.INTERACTION_IS_ACTIVE = true;
    }
}

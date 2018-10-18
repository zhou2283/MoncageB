using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCameraControl : MonoBehaviour {

    Transform cameraPivotV;
    Transform cameraPivotH;

    public float speedV = 2;
    public float speedH = 2;
    float fMouseX = 0;
    float fMouseY = 0;
    bool rotateIsActive = true;

    bool isBounceBack = false;

    bool isZoom = false;

    //boarder buffer var
    float maxUpAngle = 85f;
    float maxUpAngleWithBuffer = 90f;
    float maxDownAngle = -9f;
    float maxDownAngleWithBuffer = -14f;

    // Use this for initialization
    void Start () {
        cameraPivotV = transform.parent;
        cameraPivotH = cameraPivotV.parent;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(1) && rotateIsActive)
        {
            fMouseX = Input.GetAxis("Mouse X");
            fMouseY = Input.GetAxis("Mouse Y");
            speedV = 2f;
            speedH = 2f;
        }
        else
        {
            speedV = Mathf.Lerp(speedV, 0, Time.deltaTime * 5f);
            speedH = Mathf.Lerp(speedH, 0, Time.deltaTime * 5f);
        }

        if (Input.GetAxis("Mouse ScrollWheel")>0 && !isZoom)
        {
            transform.DOLocalMove(new Vector3(0, 0, -85), 0.5f);
            transform.GetComponent<Camera>().DOFieldOfView(20f, 0.5f);
            transform.DOLocalMove(new Vector3(0, 0, -85), 0.5f);
            isZoom = true;
            //rotateIsActive = false;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && isZoom)
        {
            transform.DOLocalMove(new Vector3(0,0,-85), 0.5f);
            transform.GetComponent<Camera>().DOFieldOfView(30f, 0.5f);
            isZoom = false;
            rotateIsActive = true;
        }

        if (isZoom && Input.GetMouseButton(2))
        {
            transform.Translate(new Vector3(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"), 0), Space.Self);
        }



        if (NormalizeEuler(cameraPivotV.localRotation.eulerAngles.x) > maxUpAngle)
        {
            
            if (Input.GetMouseButton(1))
            {
                isBounceBack = false;
                speedV *= Mathf.Pow((maxUpAngleWithBuffer - NormalizeEuler(cameraPivotV.localRotation.eulerAngles.x)) / (maxUpAngleWithBuffer - maxUpAngle), 4);
            }
            else
            {
                fMouseY = 0;
                cameraPivotV.Rotate(Vector3.right, -(NormalizeEuler(cameraPivotV.localRotation.eulerAngles.x) - maxUpAngle) / 50f, Space.Self);                
            }
        }
        else if (NormalizeEuler(cameraPivotV.localRotation.eulerAngles.x) < maxDownAngle)
        {
            if (Input.GetMouseButton(1))
            {
                isBounceBack = false;
                speedV *= Mathf.Pow((maxDownAngleWithBuffer - NormalizeEuler(cameraPivotV.localRotation.eulerAngles.x)) / (maxDownAngleWithBuffer - maxDownAngle), 4);
            }
            else
            {
                fMouseY = 0;
                cameraPivotV.Rotate(Vector3.right, -(NormalizeEuler(cameraPivotV.localRotation.eulerAngles.x) - maxDownAngle) / 50f, Space.Self);
            }
        }
        else
        {
            isBounceBack = false;
        }

        if (!isBounceBack)
        {
            cameraPivotV.Rotate(Vector3.right, -fMouseY * speedV, Space.Self);
        }
        cameraPivotH.Rotate(Vector3.up, fMouseX * speedH, Space.Self);

        
        


        print(cameraPivotV.localRotation.eulerAngles.x);
    }

    public float NormalizeEuler(float value)
    {
        float angle = value - 180;
        if (angle > 0)
            return angle - 180;
        return angle + 180;
    }
}

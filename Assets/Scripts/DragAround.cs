using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAround : MonoBehaviour
{

    public Transform obj;
    public float speed = 2;

    private bool _mouseDown = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _mouseDown = true;
        else if (Input.GetMouseButtonUp(0))
            _mouseDown = false;


        if (_mouseDown)
        {
            float fMouseX = Input.GetAxis("Mouse X");
            float fMouseY = Input.GetAxis("Mouse Y");
            transform.Rotate(Vector3.up, -fMouseX * speed, Space.World);
            transform.Rotate(Vector3.right, fMouseY * speed, Space.World);
        }
    }
}

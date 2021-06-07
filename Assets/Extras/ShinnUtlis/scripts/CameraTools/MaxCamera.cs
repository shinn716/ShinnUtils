//
// UDPServer - Unity TCP Socket
//
// Copyright (C) 2021 John Tsai
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxCamera : MonoBehaviour
{
    #region DECLARE
    public Vector3 targetOffset;
    public Vector2 limitRotation = new Vector2(-80, 80);    // x->min, y->max
    public float distance = 5.0f;
    public float mouseRotationSpeed = 200;
    public float panSpeed = 0.3f;
    public float zoomDampening = 6.0f;

    private Transform target = null;
    private Vector2 deg = Vector2.zero;
    private float desiredDistance = 0;
    private float fov = 0;
    private float fovOrigin = 0;

    private Quaternion currentRotation = Quaternion.identity;
    private Quaternion desiredRotation = Quaternion.identity;
    private Quaternion rotation = Quaternion.identity;
    private Vector3 position = Vector3.zero;
    private Vector3 vec3Origin = Vector3.zero;
    private Quaternion quaternionOrigin = Quaternion.identity;
    private bool boolResetFlag = false;
    #endregion

    #region MAIN
    private void Start()
    {
        Init();

        distance = Vector3.Distance(transform.position, target.position);
        vec3Origin = Camera.main.transform.position;
        quaternionOrigin = Camera.main.transform.rotation;
        fovOrigin = Camera.main.fieldOfView;
        fov = fovOrigin;
    }

    private void Update()
    {
        if (boolResetFlag)
        {
            if (Vector3.Distance(transform.position, vec3Origin) < .001f)
            {
                transform.localPosition = vec3Origin;
                transform.localRotation = quaternionOrigin;
                boolResetFlag = false;
                Init();
            }

            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fov, Time.unscaledDeltaTime * zoomDampening);
            transform.position = Vector3.Lerp(transform.localPosition, vec3Origin, Time.unscaledDeltaTime * zoomDampening);
            transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.unscaledDeltaTime * zoomDampening);
        }
        else
        {
            // touch
            if (Input.touchCount > 0)
            {
                // rotation
                if (Input.touchCount == 1)
                {
                    Touch touch0 = Input.GetTouch(0);
                    if (touch0.phase == TouchPhase.Moved)
                        Rotation(-touch0.deltaPosition.x * .002f, -touch0.deltaPosition.y * .002f);
                }
                // zoom in/out  
                else if (Input.touchCount == 2)
                {
                    Touch touch0 = Input.GetTouch(0);
                    Touch touch1 = Input.GetTouch(1);
                    if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
                    {
                        Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
                        Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

                        float prevMagnitude = (touch0PrevPos - touch1PrevPos).magnitude;
                        float currentMagitude = (touch0.position - touch1.position).magnitude;
                        float difference = currentMagitude - prevMagnitude;

                        Zoom(difference * .01f);
                    }
                }
                // pan
                else if (Input.touchCount == 3)
                {
                    Touch touch0 = Input.GetTouch(0);
                    if (touch0.phase == TouchPhase.Moved)
                        Pan(touch0.deltaPosition.x * .025f, -touch0.deltaPosition.y * .025f);
                }

                position = target.position - (rotation * Vector3.forward * desiredDistance + targetOffset);
                transform.position = position;
            }
            // mouse
            else
            {
                // rotation
                if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
                    Rotation(Input.GetAxis("Mouse X") * .1f, Input.GetAxis("Mouse Y") * .1f);
                // pan
                else if (Input.GetMouseButton(2))
                    Pan(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
                // zoom in/out
                Zoom(Input.GetAxis("Mouse ScrollWheel") * .5f);

                position = target.position - (rotation * Vector3.forward * desiredDistance + targetOffset);
                transform.position = position;
            }
        }
    }
    #endregion

    #region PRIVATE
    private void Init()
    {
        if (!target)
        {
            GameObject go = new GameObject("Cam Target");
            go.transform.position = Camera.main.transform.position + (transform.forward * distance);
            target = go.transform;
        }

        target.position = Camera.main.transform.position + (transform.forward * distance);
        target.rotation = quaternionOrigin;

        desiredDistance = distance;
        position = vec3Origin;
        rotation = quaternionOrigin;
        currentRotation = quaternionOrigin;
        desiredRotation = quaternionOrigin;

        deg.x = Vector3.Angle(Vector3.right, transform.right);
        deg.y = Vector3.Angle(Vector3.up, transform.up);
    }

    private void Rotation(float posx, float posy)
    {
        deg.x += posx * mouseRotationSpeed;
        deg.y -= posy * mouseRotationSpeed;

        deg.y = ClampAngle(deg.y, limitRotation.x, limitRotation.y);
        desiredRotation = Quaternion.Euler(deg.y, deg.x, 0);
        currentRotation = transform.rotation;

        rotation = Quaternion.Slerp(currentRotation, desiredRotation, Time.unscaledDeltaTime * zoomDampening);
        transform.rotation = rotation;
    }

    private void Pan(float right, float up)
    {
        target.rotation = transform.rotation;
        target.Translate(Vector3.right * right * panSpeed);
        target.Translate(transform.up * up * panSpeed, Space.World);
    }

    private void Zoom(float increment)
    {
        fov += increment * 15;
        fov = Mathf.Clamp(fov, 5, 80);
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fov, Time.unscaledDeltaTime * zoomDampening);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
    #endregion

    #region PUBLIC
    public void ResetCam()
    {
        boolResetFlag = true;
        fov = fovOrigin;
    }
    #endregion
}

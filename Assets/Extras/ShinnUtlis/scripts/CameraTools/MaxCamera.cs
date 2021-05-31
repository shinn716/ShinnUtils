// Author : Shinn
// Date : 20210531
// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxCamera : MonoBehaviour
{
    #region DECLARE
    public enum Type
    {
        Desktop,
        Mobile
    }

    public Type type = Type.Desktop;

    public static MaxCamera instance;

    public Vector3 targetOffset;
    public float distance = 5.0f;
    public float mouseRotationSpeed = 200;
    public Vector2 limitRotation = new Vector2(-80, 80);    // x->min, y->max


    public float panSpeed = 0.3f;
    public float zoomDampening = 5.0f;

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
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Init();

        distance = Vector3.Distance(transform.position, target.position);
        vec3Origin = Camera.main.transform.position;
        quaternionOrigin = Camera.main.transform.rotation;
        fovOrigin = Camera.main.fieldOfView;
        fov = fovOrigin;
    }

    private void LateUpdate()
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

            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fov, Time.deltaTime * zoomDampening);
            transform.localPosition = Vector3.Lerp(transform.localPosition, vec3Origin, Time.deltaTime * zoomDampening);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * zoomDampening);
        }
        else
        {
            if (type.Equals(Type.Desktop))
            {
                // rotation
                if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
                {
                    deg.x += Input.GetAxis("Mouse X") * mouseRotationSpeed * 0.02f;
                    deg.y -= Input.GetAxis("Mouse Y") * mouseRotationSpeed * 0.02f;

                    //Clamp the vertical axis for the orbit
                    deg.y = ClampAngle(deg.y, limitRotation.x, limitRotation.y);
                    desiredRotation = Quaternion.Euler(deg.y, deg.x, 0);
                    currentRotation = transform.rotation;

                    rotation = Quaternion.Slerp(currentRotation, desiredRotation, Time.deltaTime * zoomDampening);
                    transform.rotation = rotation;
                }
                // pan
                else if (Input.GetMouseButton(2))
                {
                    target.rotation = transform.rotation;
                    target.Translate(Vector3.right * -Input.GetAxis("Mouse X") * panSpeed);
                    target.Translate(transform.up * -Input.GetAxis("Mouse Y") * panSpeed, Space.World);
                }
                // zoom in/out
                else if (Mathf.Abs(Input.mouseScrollDelta.y) > 0)
                    Zoom(Input.GetAxis("Mouse ScrollWheel") * .5f);

                // calculate position based on the new currentDistance 
                position = target.position - (rotation * Vector3.forward * desiredDistance + targetOffset);
                transform.position = position;
            }
            else if (type.Equals(Type.Mobile))
            {
                if (Input.touchCount > 0)
                {
                    // rotation
                    if (Input.touchCount == 1)
                    {
                        Touch touch = Input.GetTouch(0);
                        if (Input.GetTouch(0).phase == TouchPhase.Moved)
                        {

                            deg.x += touch.deltaPosition.x * mouseRotationSpeed * 0.002f;
                            deg.y -= touch.deltaPosition.y * mouseRotationSpeed * 0.002f;

                            ////////OrbitAngle

                            //Clamp the vertical axis for the orbit
                            deg.y = ClampAngle(deg.y, limitRotation.x, limitRotation.y);
                            // set camera rotation 
                            desiredRotation = Quaternion.Euler(deg.y, deg.x, 0);
                            currentRotation = transform.rotation;

                            rotation = Quaternion.Slerp(currentRotation, desiredRotation, Time.deltaTime * zoomDampening);
                            transform.rotation = rotation;
                        }
                    }
                    // zoom in/out  
                    else if (Input.touchCount == 2)
                    {
                        Touch touch0 = Input.GetTouch(0);
                        Touch touch1 = Input.GetTouch(1);

                        Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
                        Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

                        float prevMagnitude = (touch0PrevPos - touch1PrevPos).magnitude;
                        float currentMagitude = (touch0.position - touch1.position).magnitude;
                        float difference = currentMagitude - prevMagnitude;

                        Zoom(difference * .01f);
                    }
                    // pan
                    else if (Input.touchCount == 3)
                    {
                        Touch touch0 = Input.GetTouch(0);

                        //grab the rotation of the camera so we can move in a psuedo local XY space
                        target.rotation = transform.rotation;
                        target.Translate(Vector3.right * touch0.deltaPosition.x * panSpeed * .05f);
                        target.Translate(transform.up * -touch0.deltaPosition.y * panSpeed * .05f, Space.World);
                    }

                    // calculate position based on the new currentDistance 
                    position = target.position - (rotation * Vector3.forward * desiredDistance + targetOffset);
                    transform.position = position;
                }
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

    private void Zoom(float increment)
    {
        fov -= increment * 15;
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fov, Time.deltaTime * zoomDampening);
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

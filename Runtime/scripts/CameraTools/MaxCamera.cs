// Author : Shinn
// Date : 20210531
// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxCamera : MonoBehaviour
{
    #region DECLARE
    public bool useTouch = false;
    public Vector2 limitRotation = new Vector2(-80, 80);    // x->min, y->max
    public float distance = 5.0f;
    public float rollCameraSpeed = .1f;
    public float zoomDampening = 50f;
    public float zoomTouchOffset = .5f;
    public float panSpeed = 0.3f;

    private Transform target = null;
    private Vector2 deg = Vector2.zero;
    private float desiredDistance = 0;

    private Quaternion currentRotation = Quaternion.identity;
    private Quaternion desiredRotation = Quaternion.identity;
    private Quaternion rotation = Quaternion.identity;
    private Vector3 position = Vector3.zero;
    private Vector3 vec3Origin = Vector3.zero;
    private Quaternion quaternionOrigin = Quaternion.identity;
    private bool isMouseDragging = false;

    private bool init = false;
    private GameObject povit = null;
    private Rigidbody rb = null;
    #endregion

    #region MAIN
    private void Start()
    {
        povit = new GameObject();
        povit.name = $"Camera_Povit";
        transform.SetParent(povit.transform);
        povit.transform.localPosition = transform.localPosition;
        povit.transform.localRotation = transform.localRotation;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        if (GetComponent<Rigidbody>() == null)
            rb = gameObject.AddComponent<Rigidbody>();
        else
            rb = gameObject.GetComponent<Rigidbody>();
        rb.mass = 10;
        rb.drag = 10;
        rb.useGravity = false;
        rb.isKinematic = false;

        Init();

        distance = Vector3.Distance(transform.position, target.position);
        vec3Origin = povit.transform.localPosition;
        quaternionOrigin = povit.transform.localRotation;

        Rotation(0, 0);
    }

    private void Update()
    {
        // touch
        if (useTouch)
        {
            // rotation
            if (Input.touchCount == 1)
            {
                Touch touch0 = Input.GetTouch(0);
                if (touch0.phase == TouchPhase.Moved)
                    Rotation(touch0.deltaPosition.x * .002f, touch0.deltaPosition.y * .002f);
            }
            //zoom in/out  
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

                    // Zoom in/Zoom out
                    if (difference > 5f)
                        rb.velocity = zoomTouchOffset * Time.deltaTime * zoomDampening * transform.forward;
                    else if (difference < -5f)
                        rb.velocity = zoomTouchOffset * Time.deltaTime * zoomDampening * -transform.forward;
                }
            }
            //pan
            else if (Input.touchCount == 3)
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);
                Touch touch2 = Input.GetTouch(2);
                if (touch0.phase == TouchPhase.Moved && touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
                    Pan(touch0.deltaPosition.x * .025f, touch0.deltaPosition.y * .025f);
            }

            position = target.position - (rotation * Vector3.forward * desiredDistance);
            povit.transform.localPosition = position;
        }
        // mouse
        else
        {
            if (Input.GetMouseButtonDown(0))
                isMouseDragging = true;

            if (Input.GetMouseButtonUp(0))
                isMouseDragging = false;

            // rotation
            if (isMouseDragging)
                Rotation(Input.GetAxis("Mouse X") * rollCameraSpeed, Input.GetAxis("Mouse Y") * rollCameraSpeed);

            // pan
            if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
                Pan(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            // Zoom in/Zoom out
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                rb.velocity = Time.deltaTime * zoomDampening * -transform.forward;
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                rb.velocity = Time.deltaTime * zoomDampening * transform.forward;

            position = target.position - (rotation * Vector3.forward * desiredDistance);
            povit.transform.localPosition = position;
        }

    }
    #endregion

    #region PRIVATE
    private void Init()
    {
        if (!target)
        {
            GameObject go = new GameObject("Cam Target");
            go.transform.position = transform.position + (transform.forward * distance);
            target = go.transform;
        }

        target.position = transform.position + (povit.transform.forward * distance);
        target.rotation = quaternionOrigin;

        desiredDistance = distance;
        position = vec3Origin;
        rotation = quaternionOrigin;
        currentRotation = quaternionOrigin;
        desiredRotation = quaternionOrigin;

        deg.x = Vector3.Angle(Vector3.right, transform.right);
        deg.y = Vector3.Angle(Vector3.up, transform.up);

        init = true;
    }

    private void Rotation(float posx, float posy)
    {
        deg.x += posx * 200;
        deg.y -= posy * 200;

        deg.y = ClampAngle(deg.y, limitRotation.x, limitRotation.y);
        desiredRotation = Quaternion.Euler(deg.y, deg.x, 0);
        currentRotation = povit.transform.rotation;

        rotation = Quaternion.Slerp(currentRotation, desiredRotation, Time.unscaledDeltaTime * zoomDampening);
        povit.transform.rotation = rotation;
    }

    private void Pan(float right, float up)
    {
        target.rotation = transform.rotation;
        target.Translate(Vector3.right * right * panSpeed);
        target.Translate(transform.up * up * panSpeed, Space.World);
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
    [ContextMenu("ResetCam")]
    public void ResetCam()
    {
        if (!init)
            return;

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        povit.transform.localPosition = vec3Origin;
        povit.transform.localRotation = quaternionOrigin;
        rb.velocity = Vector3.zero;

        Init();
    }
    #endregion
}
// Author   Shinn
// Date     20220318

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxCamera : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 0.5f;

    private GameObject Povit = null;
    private Transform mainCamera = null;
    private Vector3 prevMousePos = Vector3.zero;
    private Vector3 origin = Vector3.zero;

    private void Awake()
    {
        mainCamera = Camera.main.transform;
    }

    private void Start()
    {
        origin = transform.position;
        Povit = new GameObject();
        Povit.name = "Povit";
        Povit.transform.SetPositionAndRotation(new Vector3(mainCamera.position.x, mainCamera.position.y, 0), mainCamera.rotation);

        mainCamera.SetParent(Povit.transform);
        mainCamera.localPosition = new Vector3(0, 0, origin.z);
        mainCamera.localRotation = Quaternion.identity;
    }

    private void Update()
    {
        // touch
        if (Input.touchCount > 0)
        {
            // rotation
            if (Input.touchCount == 1)
            {
                Touch touch0 = Input.GetTouch(0);
                if (touch0.phase == TouchPhase.Moved)
                {
                    Vector2 deltaPos = touch0.deltaPosition * sensitivity;

                    Vector3 rot = Povit.transform.localEulerAngles;
                    while (rot.x > 180f)
                        rot.x -= 360f;
                    while (rot.x < -180f)
                        rot.x += 360f;

                    rot.x = Mathf.Clamp(rot.x - deltaPos.y, -89.8f, 89.8f);
                    rot.y += deltaPos.x;
                    rot.z = 0f;

                    Povit.transform.localEulerAngles = rot;
                }
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
                    if (difference > 5f)
                        mainCamera.Translate(new Vector3(0, 0, 1));
                    else if (difference < -5f)
                        mainCamera.Translate(-new Vector3(0, 0, 1));
                }
            }
            //pan
            else if (Input.touchCount == 3)
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);
                Touch touch2 = Input.GetTouch(2);
                if (touch0.phase == TouchPhase.Moved && touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
                    Pan(touch0.deltaPosition.x * .025f, -touch0.deltaPosition.y * .025f);
            }
        }
        // mouse
        else
        {
            if (Input.GetMouseButtonDown(1))
                prevMousePos = Input.mousePosition;
            else if (Input.GetMouseButton(1))
            {
                Vector3 mousePos = Input.mousePosition;
                Vector2 deltaPos = (mousePos - prevMousePos) * sensitivity;

                Vector3 rot = Povit.transform.localEulerAngles;
                while (rot.x > 180f)
                    rot.x -= 360f;
                while (rot.x < -180f)
                    rot.x += 360f;

                rot.x = Mathf.Clamp(rot.x - deltaPos.y, -89.8f, 89.8f);
                rot.y += deltaPos.x;
                rot.z = 0f;

                Povit.transform.localEulerAngles = rot;
                prevMousePos = mousePos;
            }
            else if (Input.GetMouseButton(2))
                Pan(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
            else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                mainCamera.Translate(new Vector3(0, 0, 1));
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                mainCamera.Translate(-new Vector3(0, 0, 1));
        }
    }

    private void Pan(float right, float up)
    {
        mainCamera.Translate(sensitivity * 2 * right * Vector3.left);
        mainCamera.Translate(sensitivity * 2 * up * Vector3.up, Space.World);
    }
}
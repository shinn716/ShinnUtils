// MaxCamera
// Author: johnTC
// Last update: 20220506

using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MaxCamera : MonoBehaviour
{
    #region DECLARE
    [SerializeField] Transform povit;
	[Header("Rotation"), SerializeField] private float rotCameraSpeed = .1f;

    [Header("Roll"), SerializeField] private float zoomDampening = .1f;

    [Header("Pan"), SerializeField] private float panSpeed = .4f;

    [Space, SerializeField] private bool disableRoll = false;
    [SerializeField] private bool disablePan = false;
    [SerializeField] private bool inverseMouseClick = false;

    private Vector3 prevMousePos;
    private Vector3 orgPos;
    private Quaternion orgRot;
    private Vector3 orgPovitPos;
    private Quaternion orgPovitRot;

    private int mouseClickValue = 0;
    private readonly float offset = 2.5f;
    #endregion

    #region START
    private void Start()
	{
        transform.SetParent(povit);

        orgPos = transform.localPosition;
        orgRot = transform.localRotation;
        orgPovitPos = povit.transform.localPosition;
        orgPovitRot = povit.transform.localRotation;

        InverseMouseClick(inverseMouseClick);

#if UNITY_EDITOR
        rotCameraSpeed *= offset;
        zoomDampening *= offset;
        panSpeed *= offset;
#endif
    }

	private void Update()
    {
        if (povit == null)
            return;

        // touch
        if (Input.touchCount > 0)
        {
            // rotation
            if (Input.touchCount == 1)
            {
                Touch touch0 = Input.GetTouch(0);
                if (touch0.phase == TouchPhase.Moved)
                {
                    Vector2 deltaPos = touch0.deltaPosition * rotCameraSpeed;
                    Rotation(deltaPos);
                }
            }

            // zoom in/out 
            else if (Input.touchCount == 2 && !disableRoll)
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
                        transform.Translate(Vector3.forward * zoomDampening);
                    else if (difference < -5f)
                        transform.Translate(Vector3.back * zoomDampening);
                }
            }

            // pan
            else if (Input.touchCount == 3 && !disablePan)
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);
                Touch touch2 = Input.GetTouch(2);
                if (touch0.phase == TouchPhase.Moved && touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
                    Pan(touch0.deltaPosition.x * .025f, touch0.deltaPosition.y * .025f);
            }
        }
        // mouse
        else
        {
            // rotation
            if (Input.GetMouseButtonDown(mouseClickValue))
                prevMousePos = Input.mousePosition;
            else if (Input.GetMouseButton(mouseClickValue))
            {
                Vector3 mousePos = Input.mousePosition;
                Vector2 deltaPos = (mousePos - prevMousePos) * rotCameraSpeed;
                Rotation(deltaPos);
                prevMousePos = mousePos;
            }

            // pan
            else if (Input.GetMouseButton(2) && !disablePan)
                Pan(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));

            // zoom in/out
            if (!disableRoll)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                    transform.Translate(Vector3.forward * zoomDampening);

                else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                    transform.Translate(Vector3.back * zoomDampening);
            }
        }
    }
    #endregion

    #region PUBLIC
    public void SetReset()
    {
        prevMousePos = Vector3.zero;
        transform.localPosition = orgPos;
        transform.localRotation = orgRot;
        povit.localPosition = orgPovitPos;
        povit.localRotation = orgPovitRot;
    }

    public void InverseMouseClick(bool boolvalue)
    {
        mouseClickValue = boolvalue ? 1 : 0;
    }
    #endregion

    private void Rotation(Vector2 deltaPos)
    {
        Vector3 rot = povit.localEulerAngles;
        while (rot.x > 180f)
            rot.x -= 360f;
        while (rot.x < -180f)
            rot.x += 360f;

        rot.x = Mathf.Clamp(rot.x - deltaPos.y, -30f, 60f);
        rot.y += deltaPos.x;
        rot.z = 0f;

        povit.localEulerAngles = rot;
    }

    private void Pan(float right, float up)
    {
        transform.Translate(panSpeed * right * Vector3.left);
        transform.Translate(panSpeed * up * Vector3.up, Space.World);
    }

}
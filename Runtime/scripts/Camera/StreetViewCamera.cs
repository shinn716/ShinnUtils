// StreetViewCamera
// Author: johnTC
// Last update: 20220506

using UnityEngine;

[RequireComponent(typeof(Camera))]
public class StreetViewCamera : MonoBehaviour
{
    #region DECLARE
    [Header("Rotation"), SerializeField] private float rotCameraSpeed = 2f;

    [Header("Roll"), SerializeField] private float zoomDampening = .1f;
    [SerializeField] private float zoomTouchOffset = 1;

    [Header("Pan"),SerializeField] private float panSpeed = .4f;

    [Space, SerializeField] private bool disableRoll = false;
    [SerializeField] private bool disablePan = false;
    [SerializeField] private bool inverseMouseClick = false;

    public Transform Povit { get => povit.transform; }
    public bool IsMouseDragging { get; set; } = false;

    private int mouseClickValue = 0;
    private GameObject povit;
    private Vector3 orgPos = Vector3.zero;
    private Quaternion orgRot = Quaternion.identity;
    private readonly float offset = 2.5f;
    #endregion

    #region MAIN
    private void Start()
    {
        povit = new GameObject();
        povit.name = $"StreetViewCamera_Povit";
        transform.SetParent(povit.transform);
        povit.transform.localPosition = transform.localPosition;
        povit.transform.localRotation = transform.localRotation;

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        orgPos = povit.transform.localPosition;
        orgRot = povit.transform.localRotation;

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
                    Rotation(touch0.deltaPosition.x * rotCameraSpeed * .05f, touch0.deltaPosition.y * rotCameraSpeed * .05f);
            }
            //zoom in/out  
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

                    // Zoom in/Zoom out
                    if (difference > 5f)
                        transform.Translate(Vector3.forward * zoomDampening);
                    else if (difference < -5f)
                        transform.Translate(Vector3.back * zoomDampening);
                }
            }
            //pan
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
            // Rotation
            if (Input.GetMouseButtonDown(mouseClickValue))
                IsMouseDragging = true;

            if (Input.GetMouseButtonUp(mouseClickValue))
                IsMouseDragging = false;

            // rotation
            if (IsMouseDragging)
                Rotation(-Input.GetAxis("Mouse X") * rotCameraSpeed, -Input.GetAxis("Mouse Y") * rotCameraSpeed);

            // Zoom in/Zoom out
            if (!disableRoll)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                    transform.Translate(Vector3.forward * zoomDampening);
                else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                    transform.Translate(Vector3.back * zoomDampening);
            }

            // Pan
            if (Input.GetMouseButton(2) && !disablePan)
                Pan(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
        }
    }

    private void OnDisable()
    {
        IsMouseDragging = false;
    }
    #endregion

    #region PUBLIC
    [ContextMenu("Reset")]
    public void Reset()
    {
        povit.transform.localPosition = orgPos;
        povit.transform.localRotation = orgRot;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void InverseMouseClick(bool boolvalue)
    {
        mouseClickValue = boolvalue ? 1 : 0;
    }
    #endregion

    #region PRIVATE
    private void Rotation(float posx, float posy)
    {
        povit.transform.Rotate(new Vector3(posy, -posx, 0));
        povit.transform.rotation = Quaternion.Euler(povit.transform.eulerAngles.x, povit.transform.eulerAngles.y, 0);
    }

    private void Pan(float right, float up)
    {
        povit.transform.Translate(panSpeed * right * Vector3.right);
        povit.transform.Translate(panSpeed * up * Vector3.up, Space.World);
    }
    #endregion
}
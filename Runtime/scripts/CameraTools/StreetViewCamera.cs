using UnityEngine;

[RequireComponent(typeof(Camera))]
public class StreetViewCamera : MonoBehaviour
{
    #region DECLARE
    [SerializeField] private float zoomDampening = 100;
    [SerializeField] private float zoomTouchOffset = .5f;

    [SerializeField] private float rotCameraSpeed = 2.5f;
    [SerializeField] private float panSpeed = .1f;

    public Transform Povit { get => povit.transform; }

    private GameObject povit;
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

        rb = GetComponent<Rigidbody>() == null ? gameObject.AddComponent<Rigidbody>() : GetComponent<Rigidbody>();
        rb.mass = 10;
        rb.drag = 5;
        rb.useGravity = false;
        rb.isKinematic = false;
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
                    povit.transform.Rotate(new Vector3(touch0.deltaPosition.x * .002f, touch0.deltaPosition.y * .002f, 0));
                    povit.transform.rotation = Quaternion.Euler(povit.transform.rotation.eulerAngles.x, povit.transform.rotation.eulerAngles.y, 0);
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
        }
        else
        {
            // Rotation
            if (Input.GetMouseButton(1) || Input.GetMouseButton(0))
            {
                povit.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * rotCameraSpeed, Input.GetAxis("Mouse X") * rotCameraSpeed, 0));
                povit.transform.rotation = Quaternion.Euler(povit.transform.rotation.eulerAngles.x, povit.transform.rotation.eulerAngles.y, 0);
            }

            // Zoom in/Zoom out
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                rb.velocity = Time.deltaTime * zoomDampening * -transform.forward;
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                rb.velocity = Time.deltaTime * zoomDampening * transform.forward;

            // Pan
            if (Input.GetMouseButton(2))
                Pan(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
        }
    }

    private void Pan(float right, float up)
    {
        povit.transform.Translate(panSpeed * right * Vector3.right);
        povit.transform.Translate(panSpeed * up * Vector3.up, Space.World);
    }
    #endregion
}
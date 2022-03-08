using UnityEngine;

public class StreetViewCamera : MonoBehaviour
{
    #region DECLARE
    [SerializeField] private float rollCameraSpeed = 3;
    [SerializeField] private float rotCameraSpeed = 5;
    [SerializeField] private float panCameraSensitivity = .02f;
    #endregion

    #region PRIVATE
    private Vector3 lastPosition = Vector3.zero;
    private Camera mainCamera;
    #endregion

    #region MAIN
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (mainCamera == null)
            return;

        // Rotation
        if (Input.GetMouseButton(1))
        {
            mainCamera.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * rotCameraSpeed, Input.GetAxis("Mouse X") * rotCameraSpeed, 0));
            mainCamera.transform.rotation = Quaternion.Euler(mainCamera.transform.rotation.eulerAngles.x, mainCamera.transform.rotation.eulerAngles.y, 0);
        }

        // Zoom in/Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, mainCamera.transform.position -= Vector3.forward * rollCameraSpeed, Time.deltaTime * 5);
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, mainCamera.transform.position += Vector3.forward * rollCameraSpeed, Time.deltaTime * 5);

        // Pan
        if (Input.GetMouseButtonDown(2))
            lastPosition = Input.mousePosition;
        else if (Input.GetMouseButton(2))
        {
            Vector3 delta = Input.mousePosition - lastPosition;
            mainCamera.transform.Translate(-delta.x * panCameraSensitivity, -delta.y * panCameraSensitivity, 0);
            lastPosition = Input.mousePosition;
        }
    }
    #endregion
}
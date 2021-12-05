using UnityEngine;

public class StreetViewCamera : MonoBehaviour
{
    public float RollCameraSpeed = 3;
    public float RotCameraSpeed = 5;
    public float PanCameraSensitivity = .02f;
    
    private Vector3 lastPosition;

    private void Update()
    {
        // Rotation
        if (Input.GetMouseButton(1))
        {
            Camera.main.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * RotCameraSpeed, Input.GetAxis("Mouse X") * RotCameraSpeed, 0));
            Camera.main.transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles.x, Camera.main.transform.rotation.eulerAngles.y, 0);
        }

        // Zoom in/Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Camera.main.transform.position -= Vector3.forward * RollCameraSpeed, Time.deltaTime * 5);
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Camera.main.transform.position += Vector3.forward * RollCameraSpeed, Time.deltaTime * 5);

        // Pan
        if (Input.GetMouseButtonDown(2))
            lastPosition = Input.mousePosition;

        else if (Input.GetMouseButton(2))
        {
            Vector3 delta = Input.mousePosition - lastPosition;
            Camera.main.transform.Translate(-delta.x * PanCameraSensitivity, -delta.y * PanCameraSensitivity, 0);
            lastPosition = Input.mousePosition;
        }
    }
}

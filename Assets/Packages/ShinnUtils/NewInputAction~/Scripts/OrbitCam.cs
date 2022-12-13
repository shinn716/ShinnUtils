using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Shinn
{
    [RequireComponent(typeof(Camera))]
    public class OrbitCam : MonoBehaviour
    {
        [SerializeField] private Transform povit;
        [Header("Rotation"), SerializeField] private float rotCameraSpeed = .2f;
        [Header("Roll"), SerializeField] private float zoomDampening = .2f;
        [Header("Pan"), SerializeField] private float panSpeed = .2f;
        [Space, SerializeField] private bool disableRoll = false;
        [SerializeField] private bool disablePan = false;

        private ShiCamControlAcrions cameraControlActions;
        private Vector3 orgPos;
        private Quaternion orgRot;
        private Vector3 orgPovitPos;
        private Quaternion orgPovitRot;

        private TouchState touchState0;
        private TouchState touchState1;
        private TouchState touchState2;
        private void Awake()
        {
            transform.SetParent(povit);
            orgPos = transform.localPosition;
            orgRot = transform.localRotation;
            orgPovitPos = povit.transform.localPosition;
            orgPovitRot = povit.transform.localRotation;

            cameraControlActions = new ShiCamControlAcrions();
        }
        void OnEnable()
        {
            cameraControlActions.ShiCamera.Enable();
            cameraControlActions.ShiCamera.Mouse_Rotate.performed += RotateCamera;
            cameraControlActions.ShiCamera.Mouse_Pan.performed += PanCamera;
            cameraControlActions.ShiCamera.Mouse_Zoom.performed += ZoomCamera;

            cameraControlActions.ShiCamera.Touch_0.performed += OnTouch0;
            cameraControlActions.ShiCamera.Touch_1.performed += OnTouch1;
            cameraControlActions.ShiCamera.Touch_2.performed += OnTouch2;
        }
        private void OnDisable()
        {
            cameraControlActions.ShiCamera.Disable();
            cameraControlActions.ShiCamera.Mouse_Rotate.performed -= RotateCamera;
            cameraControlActions.ShiCamera.Mouse_Pan.performed -= PanCamera;
            cameraControlActions.ShiCamera.Mouse_Zoom.performed -= ZoomCamera;

            cameraControlActions.ShiCamera.Touch_0.performed -= OnTouch0;
            cameraControlActions.ShiCamera.Touch_1.performed -= OnTouch1;
            cameraControlActions.ShiCamera.Touch_2.performed -= OnTouch2;
        }
        private void Update()
        {
            TouchEvent();
        }

        private void TouchEvent()
        {
            if (Mathf.Abs(touchState0.delta.magnitude) > 0 && Mathf.Abs(touchState1.delta.magnitude) > 0 && Mathf.Abs(touchState2.delta.magnitude) > 0)
            {
                Vector2 value = (touchState0.delta + touchState1.delta + touchState2.delta) / 3;
                Pan(value.x * panSpeed, value.y * panSpeed);
            }
            else if (Mathf.Abs(touchState0.delta.magnitude) > 0 && Mathf.Abs(touchState1.delta.magnitude) > 0)
            {
                float value = (touchState0.delta.y + touchState1.delta.y) / 2f / 100f;
                Zoom(value);
            }
            else if (Mathf.Abs(touchState0.delta.magnitude) > 0)
            {
                Vector2 value = touchState0.delta;
                Rotation(value * rotCameraSpeed);
            }
        }

        private void OnTouch0(InputAction.CallbackContext obj)
        {
            touchState0 = obj.ReadValue<TouchState>();
        }
        private void OnTouch1(InputAction.CallbackContext obj)
        {
            touchState1 = obj.ReadValue<TouchState>();
        }
        private void OnTouch2(InputAction.CallbackContext obj)
        {
            touchState2 = obj.ReadValue<TouchState>();
        }

        private void ZoomCamera(InputAction.CallbackContext obj)
        {
            if (disableRoll)
                return;
            float value = obj.ReadValue<Vector2>().y / 100f;
            Zoom(value);
        }
        private void PanCamera(InputAction.CallbackContext obj)
        {
            if (disablePan)
                return;
            if (!Mouse.current.middleButton.isPressed)
                return;
            Vector2 value = obj.ReadValue<Vector2>();
            Pan(value.x * panSpeed, value.y * panSpeed);
        }
        private void RotateCamera(InputAction.CallbackContext obj)
        {
            if (!Mouse.current.leftButton.isPressed)
                return;
            Vector2 value = obj.ReadValue<Vector2>();
            Rotation(value * rotCameraSpeed);
        }
        private void RotateCameraTouch(InputAction.CallbackContext obj)
        {
            Vector2 value = obj.ReadValue<Vector2>();
            Rotation(value * rotCameraSpeed);
        }
        private void ZoomCameraTouch(InputAction.CallbackContext obj)
        {
            if (disableRoll)
                return;
            float value = obj.ReadValue<Vector2>().y / 100f;
            Zoom(value);
        }
        private void PanCameraTouch(InputAction.CallbackContext obj)
        {
            if (disablePan)
                return;
            Vector2 value = obj.ReadValue<Vector2>();
            Pan(value.x * panSpeed, value.y * panSpeed);
        }


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
        private void Zoom(float input)
        {
            Vector3 value = input > 0 ? Vector3.forward * zoomDampening : Vector3.back * zoomDampening;
            transform.Translate(value);
        }


        [ContextMenu("SetReset")]
        public void SetReset()
        {
            transform.localPosition = orgPos;
            transform.localRotation = orgRot;
            povit.localPosition = orgPovitPos;
            povit.localRotation = orgPovitRot;
        }
    }
}
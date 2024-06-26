//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Packages/NewInputAction/InputSettings/ShiCamControlAcrions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @ShiCamControlAcrions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ShiCamControlAcrions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ShiCamControlAcrions"",
    ""maps"": [
        {
            ""name"": ""ShiCamera"",
            ""id"": ""dd146f25-1a0a-4ef8-a128-a3d4d1127e0b"",
            ""actions"": [
                {
                    ""name"": ""Mouse_Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""7ab94a02-fee5-4821-8159-1619ec331d96"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Mouse_Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""9b57733c-cb7b-4931-885d-e1d0c070a9a9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Mouse_Pan"",
                    ""type"": ""Value"",
                    ""id"": ""64d00ea1-9424-4809-ab39-1e35860e6870"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Touch_0"",
                    ""type"": ""Value"",
                    ""id"": ""94360f8e-6821-48a0-af83-6f0870a381f3"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Touch_1"",
                    ""type"": ""Value"",
                    ""id"": ""5fcdcdcc-cb1d-4731-a857-0a2536cd7e47"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Touch_2"",
                    ""type"": ""Value"",
                    ""id"": ""d5df6dc6-7c37-46d5-894a-87cab4eea099"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""31670c6d-fe4f-43fb-acd9-e42082bde228"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse_Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2d32ca4-67d5-47ec-999e-5f8790246038"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse_Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""00d0bdbf-b2f9-4284-9708-06793fb07b9f"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse_Pan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc2703fa-6892-4ebe-9915-1d34d1d9606d"",
                    ""path"": ""<Touchscreen>/touch0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Touch_0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a65b8c42-7393-4728-ab1d-b1d4d38b99b8"",
                    ""path"": ""<Touchscreen>/touch1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Touch_1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""acf42f82-94c4-4a24-858d-8fc1de84b1b6"",
                    ""path"": ""<Touchscreen>/touch2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Touch_2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // ShiCamera
        m_ShiCamera = asset.FindActionMap("ShiCamera", throwIfNotFound: true);
        m_ShiCamera_Mouse_Rotate = m_ShiCamera.FindAction("Mouse_Rotate", throwIfNotFound: true);
        m_ShiCamera_Mouse_Zoom = m_ShiCamera.FindAction("Mouse_Zoom", throwIfNotFound: true);
        m_ShiCamera_Mouse_Pan = m_ShiCamera.FindAction("Mouse_Pan", throwIfNotFound: true);
        m_ShiCamera_Touch_0 = m_ShiCamera.FindAction("Touch_0", throwIfNotFound: true);
        m_ShiCamera_Touch_1 = m_ShiCamera.FindAction("Touch_1", throwIfNotFound: true);
        m_ShiCamera_Touch_2 = m_ShiCamera.FindAction("Touch_2", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // ShiCamera
    private readonly InputActionMap m_ShiCamera;
    private IShiCameraActions m_ShiCameraActionsCallbackInterface;
    private readonly InputAction m_ShiCamera_Mouse_Rotate;
    private readonly InputAction m_ShiCamera_Mouse_Zoom;
    private readonly InputAction m_ShiCamera_Mouse_Pan;
    private readonly InputAction m_ShiCamera_Touch_0;
    private readonly InputAction m_ShiCamera_Touch_1;
    private readonly InputAction m_ShiCamera_Touch_2;
    public struct ShiCameraActions
    {
        private @ShiCamControlAcrions m_Wrapper;
        public ShiCameraActions(@ShiCamControlAcrions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Mouse_Rotate => m_Wrapper.m_ShiCamera_Mouse_Rotate;
        public InputAction @Mouse_Zoom => m_Wrapper.m_ShiCamera_Mouse_Zoom;
        public InputAction @Mouse_Pan => m_Wrapper.m_ShiCamera_Mouse_Pan;
        public InputAction @Touch_0 => m_Wrapper.m_ShiCamera_Touch_0;
        public InputAction @Touch_1 => m_Wrapper.m_ShiCamera_Touch_1;
        public InputAction @Touch_2 => m_Wrapper.m_ShiCamera_Touch_2;
        public InputActionMap Get() { return m_Wrapper.m_ShiCamera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ShiCameraActions set) { return set.Get(); }
        public void SetCallbacks(IShiCameraActions instance)
        {
            if (m_Wrapper.m_ShiCameraActionsCallbackInterface != null)
            {
                @Mouse_Rotate.started -= m_Wrapper.m_ShiCameraActionsCallbackInterface.OnMouse_Rotate;
                @Mouse_Rotate.performed -= m_Wrapper.m_ShiCameraActionsCallbackInterface.OnMouse_Rotate;
                @Mouse_Rotate.canceled -= m_Wrapper.m_ShiCameraActionsCallbackInterface.OnMouse_Rotate;
                @Mouse_Zoom.started -= m_Wrapper.m_ShiCameraActionsCallbackInterface.OnMouse_Zoom;
                @Mouse_Zoom.performed -= m_Wrapper.m_ShiCameraActionsCallbackInterface.OnMouse_Zoom;
                @Mouse_Zoom.canceled -= m_Wrapper.m_ShiCameraActionsCallbackInterface.OnMouse_Zoom;
                @Mouse_Pan.started -= m_Wrapper.m_ShiCameraActionsCallbackInterface.OnMouse_Pan;
                @Mouse_Pan.performed -= m_Wrapper.m_ShiCameraActionsCallbackInterface.OnMouse_Pan;
                @Mouse_Pan.canceled -= m_Wrapper.m_ShiCameraActionsCallbackInterface.OnMouse_Pan;
                @Touch_0.started -= m_Wrapper.m_ShiCameraActionsCallbackInterface.OnTouch_0;
                @Touch_0.performed -= m_Wrapper.m_ShiCameraActionsCallbackInterface.OnTouch_0;
                @Touch_0.canceled -= m_Wrapper.m_ShiCameraActionsCallbackInterface.OnTouch_0;
                @Touch_1.started -= m_Wrapper.m_ShiCameraActionsCallbackInterface.OnTouch_1;
                @Touch_1.performed -= m_Wrapper.m_ShiCameraActionsCallbackInterface.OnTouch_1;
                @Touch_1.canceled -= m_Wrapper.m_ShiCameraActionsCallbackInterface.OnTouch_1;
                @Touch_2.started -= m_Wrapper.m_ShiCameraActionsCallbackInterface.OnTouch_2;
                @Touch_2.performed -= m_Wrapper.m_ShiCameraActionsCallbackInterface.OnTouch_2;
                @Touch_2.canceled -= m_Wrapper.m_ShiCameraActionsCallbackInterface.OnTouch_2;
            }
            m_Wrapper.m_ShiCameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Mouse_Rotate.started += instance.OnMouse_Rotate;
                @Mouse_Rotate.performed += instance.OnMouse_Rotate;
                @Mouse_Rotate.canceled += instance.OnMouse_Rotate;
                @Mouse_Zoom.started += instance.OnMouse_Zoom;
                @Mouse_Zoom.performed += instance.OnMouse_Zoom;
                @Mouse_Zoom.canceled += instance.OnMouse_Zoom;
                @Mouse_Pan.started += instance.OnMouse_Pan;
                @Mouse_Pan.performed += instance.OnMouse_Pan;
                @Mouse_Pan.canceled += instance.OnMouse_Pan;
                @Touch_0.started += instance.OnTouch_0;
                @Touch_0.performed += instance.OnTouch_0;
                @Touch_0.canceled += instance.OnTouch_0;
                @Touch_1.started += instance.OnTouch_1;
                @Touch_1.performed += instance.OnTouch_1;
                @Touch_1.canceled += instance.OnTouch_1;
                @Touch_2.started += instance.OnTouch_2;
                @Touch_2.performed += instance.OnTouch_2;
                @Touch_2.canceled += instance.OnTouch_2;
            }
        }
    }
    public ShiCameraActions @ShiCamera => new ShiCameraActions(this);
    public interface IShiCameraActions
    {
        void OnMouse_Rotate(InputAction.CallbackContext context);
        void OnMouse_Zoom(InputAction.CallbackContext context);
        void OnMouse_Pan(InputAction.CallbackContext context);
        void OnTouch_0(InputAction.CallbackContext context);
        void OnTouch_1(InputAction.CallbackContext context);
        void OnTouch_2(InputAction.CallbackContext context);
    }
}

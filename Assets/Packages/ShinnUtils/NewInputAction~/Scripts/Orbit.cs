using System;
using Cysharp.Threading.Tasks;
using Delta.Input;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class Orbit : MonoBehaviour
{
    #region DECLARE
    [SerializeField] Transform pivot;
    [Header("Rotation"), SerializeField] private float rotCameraSpeed = .5f;

    [Header("Roll"), SerializeField] private float zoomDampening = .5f;

    [Header("Pan"), SerializeField] private float panSpeed = .1f;

    [Space, SerializeField] private bool disableRoll = false;
    [SerializeField] private bool disablePan = false;
    [SerializeField] float damping = 40;

    public bool Enable { get; set; } = true;
    public Camera GetCamera { get; private set; } = null;
    public event Action OnDraggingEvent = null;

    private Vector3 orgPos = Vector3.zero;
    private Quaternion orgRot = Quaternion.identity;
    private Vector3 orgPivotPos = Vector3.zero;
    private Quaternion orgPivotRot = Quaternion.identity;

    private InputAction dragAction = null;
    private InputAction scrollAction = null;
    private InputAction panAction = null;
    #endregion

    #region START

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        GetCamera = GetComponent<Camera>();
    }

    private void Start()
    {
        transform.SetParent(pivot);
        orgPos = transform.localPosition;
        orgRot = transform.localRotation;
        orgPivotPos = pivot.transform.localPosition;
        orgPivotRot = pivot.transform.localRotation;
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    async private void OnEnable()
    {
        await UniTask.WaitUntil(() => InputManager.Instance.PlayerInput != null);
        dragAction = InputManager.Instance.PlayerInput.actions["Drag"];
        scrollAction = InputManager.Instance.PlayerInput.actions["Scroll"];
        panAction = InputManager.Instance.PlayerInput.actions["Pan"];

        dragAction.performed += Drag;
        scrollAction.performed += OnScroll;
        panAction.performed += Pan;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    private void OnDisable()
    {
        scrollAction.performed -= OnScroll;
        dragAction.performed -= Drag;
        panAction.performed -= Pan;
    }


    private void OnScroll(InputAction.CallbackContext ctx)
    {
        if (!Enable)
            return;

        if (!disableRoll)
        {
            var scroll = ctx.ReadValue<Vector2>();
            if (scroll.y > 0f)
                transform.Translate(Vector3.forward * zoomDampening);

            else if (scroll.y < 0f)
                transform.Translate(Vector3.back * zoomDampening);
        }
    }

    private void Pan(InputAction.CallbackContext ctx)
    {
        if (!Enable)
            return;

        if (!disablePan)
        {
            var pan = ctx.ReadValue<Vector2>();
            Pan(pan.x, pan.y);
        }
    }

    private void Drag(InputAction.CallbackContext ctx)
    {
        if (pivot == null)
            return;

        if (!Enable)
            return;

        var drag = ctx.ReadValue<Vector2>();
        Rotation(drag);
        OnDraggingEvent?.Invoke();
    }
    private float ClampAngle(float angle, float min, float max)
    {
        while (angle > 180f)
            angle -= 360f;
        while (angle < -180f)
            angle += 360f;
        return Mathf.Clamp(angle, min, max);
    }

    private void Rotation(Vector2 deltaPos)
    {
        Vector3 rot = pivot.localEulerAngles;
        rot.x = ClampAngle(rot.x - deltaPos.y, -20f, 60f);
        rot.y += deltaPos.x * rotCameraSpeed;
        rot.z = 0;
        pivot.localRotation = Quaternion.Slerp(pivot.localRotation, Quaternion.Euler(rot), Time.deltaTime * damping);
    }

    private void Pan(float right, float forward)
    {
        transform.parent.Translate(panSpeed * right * Vector3.left, Space.Self);
        transform.parent.Translate(panSpeed * forward * Vector3.back, Space.Self);
        transform.parent.transform.position = Vector3.Scale(transform.parent.transform.position, new Vector3(1, 0, 1));
    }

    #endregion

    #region PUBLIC
    public void SetReset()
    {
        transform.SetLocalPositionAndRotation(orgPos, orgRot);
        pivot.SetLocalPositionAndRotation(orgPivotPos, orgPivotRot);
    }
    #endregion
}
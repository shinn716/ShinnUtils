using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class Orbit : MonoBehaviour
{
    public static Orbit instance;

    #region DECLARE
    [SerializeField] Transform povit;
    [Header("Rotation"), SerializeField] private float rotCameraSpeed = .5f;

    [Header("Roll"), SerializeField] private float zoomDampening = .5f;

    [Header("Pan"), SerializeField] private float panSpeed = .1f;

    [Space, SerializeField] private bool disableRoll = false;
    [SerializeField] private bool disablePan = false;
    [SerializeField] bool autoStart = true;
    [SerializeField] bool useKeyboard = false;
    [SerializeField] bool enableKeyboard = true;

    public bool Enable { get; set; } = true;

    private Vector3 orgPos = Vector3.zero;
    private Quaternion orgRot = Quaternion.identity;
    private Vector3 orgPovitPos = Vector3.zero;
    private Quaternion orgPovitRot = Quaternion.identity;

    InputAction dragAction;
    InputAction scrollAction;
    InputAction panAction;

    #endregion

    #region START

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        transform.SetParent(povit);

        dragAction = TouchManager.instance.playerInput.actions["Drag"];
        scrollAction = TouchManager.instance.playerInput.actions["Scroll"];
        panAction = TouchManager.instance.playerInput.actions["Pan"];

        dragAction.performed += Drag;
        scrollAction.performed += Scroll;
        panAction.performed += Pan;

        orgPos = transform.localPosition;
        orgRot = transform.localRotation;
        orgPovitPos = povit.transform.localPosition;
        orgPovitRot = povit.transform.localRotation;

        Enable = autoStart;
    }

    private void Update()
    {
        if (!enableKeyboard)
            return;

        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.up * .01f);
        else if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.down * .01f);
        else if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * .01f);
        else if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * .01f);
    }

    private void OnDestroy()
    {
        scrollAction.performed -= Scroll;
        dragAction.performed -= Drag;
        panAction.performed -= Pan;
    }

    private void Scroll(InputAction.CallbackContext ctx)
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
        if (povit == null)
            return;

        if (!Enable)
            return;

        var drag = ctx.ReadValue<Vector2>();
        Rotation(drag);
    }

    #endregion

    #region PUBLIC
    public void SetReset()
    {
        transform.SetLocalPositionAndRotation(orgPos, orgRot);
        povit.SetLocalPositionAndRotation(orgPovitPos, orgPovitRot);
    }
    #endregion

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
        Vector3 rot = povit.localEulerAngles;
        rot.x = ClampAngle(rot.x - deltaPos.y, -20f, 60f);
        rot.y += deltaPos.x * rotCameraSpeed;
        povit.localRotation = Quaternion.Slerp(povit.localRotation, Quaternion.Euler(rot), Time.deltaTime * 50);
    }

    private void Pan(float right, float forward)
    {
        transform.parent.Translate(panSpeed * right * Vector3.left, Space.Self);
        transform.parent.Translate(panSpeed * forward * Vector3.back, Space.Self);
        // transform.parent.transform.position = Vector3.Scale(transform.parent.transform.position, new Vector3(1, 0, 1));
    }

    [ContextMenu("DebugEnable")]
    private void DebugEnable()
    {
        Enable = true;
    }

    [ContextMenu("DebugDisable")]
    private void DebugDisable()
    {
        Enable = false;
    }
}
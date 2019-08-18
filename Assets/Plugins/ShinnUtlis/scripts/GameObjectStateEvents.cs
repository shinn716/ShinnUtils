using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameObjectStateEvents : MonoBehaviour
{

    public enum State
    {
        Enable,
        Disable
    }

    public State mystate;

    [Header("Events")]
    public Vector3 position = Vector3.zero;
    public Vector3 rotation = Vector3.zero;
    public Vector3 scale = Vector3.one;
    public Color color = Color.white;

    public Vector3 originPos;
    private Quaternion originRot;
    private Vector3 originScl;
    private Color originColor;

    [Space]
    public UnityEvent events;

    private void Awake()
    {
        originPos = transform.localPosition;
        originRot = transform.localRotation;
        originScl = transform.localScale;
    }

    private void Start()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        Image img = GetComponent<Image>();

        if (sp != null)
            originColor = sp.color;
        else if (img != null)
            originColor = img.color;
    }

    private void OnDisable()
    {
        if (mystate == State.Disable)
            events.Invoke();
    }

    private void OnEnable()
    {
        if (mystate == State.Enable)
            events.Invoke();
    }

    [ContextMenu("SetPosition")]
    public void SetLocalPosition()
    {
        RectTransform rect = GetComponent<RectTransform>();
        if (rect == null)
            transform.localPosition = position;
        else
            rect.anchoredPosition = position;
    }

    [ContextMenu("SetScale")]
    public void SetScale()
    {
        transform.localScale = scale;
    }

    [ContextMenu("SetColor")]
    public void SetColor()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        Image img = GetComponent<Image>();

        if (sp != null)
            sp.color = color;

        else if (img != null)
            img.color = color;
    }

    [ContextMenu("SetRotation")]
    public void SetLocalRotation()
    {
        transform.localEulerAngles = rotation;
    }

    [ContextMenu("SetReset")]
    public void SetReset()
    {
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
    }

    [ContextMenu("SetDefaultTransform")]
    public void SetDefaultTransform()
    {
        transform.localPosition = originPos;
        transform.localRotation = originRot;
        transform.localScale = originScl;
    }

    [ContextMenu("ResetLocationPosition")]
    public void ResetLocationPosition()
    {
        transform.localPosition = originPos;
    }

    [ContextMenu("ResetSpriteImageColor")]
    public void ResetSpriteImageColor()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        Image img = GetComponent<Image>();

        if (sp != null)
            sp.color = originColor;
        else if (img != null)
            img.color = originColor;
    }
}

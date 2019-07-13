using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameObjectStateEvents : MonoBehaviour
{

    public enum state
    {
        Enable,
        Disable
    }

    public state mystate;

    [Header("Events")]
    public Vector3 position = Vector3.zero;
    public Vector3 rotation = Vector3.zero;
    public Vector3 scale = Vector3.one;
    public Color color = Color.white;

    [Space]
    public UnityEvent events;

    private void OnDisable()
    {
        if (mystate == state.Disable)
            events.Invoke();
    }

    private void OnEnable()
    {
        if (mystate == state.Enable)
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
}

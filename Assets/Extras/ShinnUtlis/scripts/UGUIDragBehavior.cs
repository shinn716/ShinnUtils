using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UGUIDragBehavior : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    Vector2 startPoint;

    [SerializeField]
    Vector2 endPoint;

    public bool drag;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        startPoint = eventData.pressPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        drag = true;
        endPoint = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        drag = false;
    }
}

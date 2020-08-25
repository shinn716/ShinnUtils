using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject DraggedInstance;
    
    Vector3 _startPosition;
    Vector3 _offsetToMouse;
    float _zDistanceToCamera;

    [Header("DropIn box")]
    public string boxName1 = "box1";
    public string boxName2 = "box2";
    public GameObject box1;
    public GameObject box2;
    public bool InBox1 = false;
    public bool InBox2 = false;

    #region Interface Implementations

    public void OnBeginDrag(PointerEventData eventData)
    {
        DraggedInstance = gameObject;
        _startPosition = transform.position;
        _zDistanceToCamera = Mathf.Abs(_startPosition.z - Camera.main.transform.position.z);

        _offsetToMouse = _startPosition - Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
        );
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * .7f * .5f;

        if (Input.touchCount > 1)
            return;

        transform.position = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
            ) + _offsetToMouse;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * .5f;

        DraggedInstance = null;
        _offsetToMouse = Vector3.zero;

        if (box1 != null || box2 != null)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.name == boxName1)
                {
                    transform.position = new Vector3(box1.transform.position.x, box1.transform.position.y, transform.position.z);
                    InBox1 = true;
                    InBox2 = false;
                }
                else if (hit.collider.gameObject.name == boxName2)
                {
                    transform.position = new Vector3(box2.transform.position.x, box2.transform.position.y, transform.position.z);
                    InBox1 = false;
                    InBox2 = true;
                }
            }
        }

    }
    

    #endregion
}
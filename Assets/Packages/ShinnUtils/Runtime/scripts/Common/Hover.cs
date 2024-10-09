using UnityEngine;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Texture2D cursorCheck = null;
    private bool isCursorSet = false; // 追蹤當前游標是否為自定義游標

    private void LoadCursorIfNeeded()
    {
        if (cursorCheck == null)
        {
            cursorCheck = Resources.Load<Texture2D>("CursorHand");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isCursorSet)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            isCursorSet = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LoadCursorIfNeeded();
        if (!isCursorSet)
        {
            Cursor.SetCursor(cursorCheck, new Vector2(10, 0), CursorMode.Auto);
            isCursorSet = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isCursorSet)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            isCursorSet = false;
        }
    }
}

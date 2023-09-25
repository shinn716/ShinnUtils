using UnityEngine;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Texture2D cursorCheck;

    private void Start()
    {
        cursorCheck = Resources.Load<Texture2D>("CursorHand");
        // Main.instance.FinishLoadCursor += GetCursorAssets;
        // if (cursorCheck == null)
        //     cursorCheck = Main.instance.CursorHand;
    }

    public void OnPointerClick(PointerEventData eventData) => Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

    public void OnPointerEnter(PointerEventData eventData) => Cursor.SetCursor(cursorCheck, new Vector2(10, 0), CursorMode.Auto);

    public void OnPointerExit(PointerEventData eventData) => Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
}

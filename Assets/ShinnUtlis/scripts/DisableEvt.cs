using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DisableEvt : MonoBehaviour {

    public UnityEvent events;

    public Vector3 orgpos;
    public Color orgcolor = Color.white;

    private void OnDisable()
    {
        events.Invoke();
    }

    
    public void PosSetDefault()
    {
        RectTransform rect = GetComponent<RectTransform>();
        if (rect == null)
            transform.localPosition = orgpos;
        else
            rect.localPosition = orgpos;
    }

    public void ColorSetDefault()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        sp.color = orgcolor;
    }
}

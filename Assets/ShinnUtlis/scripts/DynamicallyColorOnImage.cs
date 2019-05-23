using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicallyColorOnImage : MonoBehaviour 
{
    [Range(0, 5)]
    public float speed = 1;
    public float changeTime = 2;
    public Color[] targetColors;


    private Image img;
    private Color lerpColor = Color.black;
    private int m_index = 0;
    private bool m_run = false;

    private void Start()
    {
        img = GetComponent<Image>();
    }

    private void OnEnable()
    {
        InvokeRepeating("Run", 0, changeTime);
    }

    private void Render()
    {
        if (m_run)
        {
            if (targetColors[m_index-1].Equals(lerpColor))
            {
                m_run = false;
            }
            else 
            {
                lerpColor = Color.Lerp(lerpColor, targetColors[m_index-1], Time.fixedDeltaTime * speed);
                img.color = lerpColor;
            }

        }

    }


    [ContextMenu("Run")]
    private void Run()
    {
        m_run = true;
        m_index = m_index % targetColors.Length;
        m_index++;
        if (IsInvoking("Render"))
            CancelInvoke("Render");
        InvokeRepeating("Render", 0, 0.1f);
    }

    private void OnDisable()
    {
        CancelInvoke("Run");
    }


}

using UnityEngine;
using UnityEngine.UI;

public class DynamicallyColorOnImage : MonoBehaviour 
{
    [Range(0, 10)]
    public float speed = 3;

    [Range(0, 5)]
    public float changeTime = 2;

    [Range(0, 1)]
    public float freshTime = .01f;
    
    public bool Random = false;

    [Space]
    public Color[] targetColors;
    
    private Image img;
    private Color m_lerpColor = Color.black;
    private int m_index = 0;

    // inital on stat
    private void Start()
    {
        img = GetComponent<Image>();
    }

    // Repeat and start function on enable.
    private void OnEnable()
    {
        Run();
    }

    #region public fuction
    public void Run()
    {
        InvokeRepeating("Change", 0, changeTime);
    }

    public void Stop()
    {
        CancelInvoke("Run");
    }
    #endregion

    #region private function

    // Change color function. 
    private void Render()
    {
        m_lerpColor = Color.Lerp(m_lerpColor, targetColors[m_index - 1], Time.fixedDeltaTime * speed);
        img.color = m_lerpColor;
    }

    // When run, change color on image.
    private void Change()
    {
        // m_index 在 targetColors.Length 內累加
        if (Random)
        {
            m_index = m_index % targetColors.Length;
            m_index++;
        }
        else
        {
            m_index = UnityEngine.Random.Range(0, targetColors.Length);
        }

        // 每一次執行前, 先確認 Render 是關閉的.
        if (IsInvoking("Render"))
            CancelInvoke("Render");

        // 0 -> first start time, 0.1f -> fresh time  
        InvokeRepeating("Render", 0, freshTime);
    }

    // Disable gameobject cancel invoke.
    private void OnDisable()
    {
        Stop();
    }
    #endregion

}

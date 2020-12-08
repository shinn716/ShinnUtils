using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullingGroupCamera : MonoBehaviour
{
    [SerializeField] float fov = 2;
    [SerializeField] Transform[] targets = null;

    private CullingGroup m_group = null;
    private BoundingSphere[] m_bounds;

    void Start()
    {
        m_group = new CullingGroup();

        //　設置相機進行剔除
        m_group.targetCamera = Camera.main;

        // 設置用於測量距離和距離水平的中心座標
        // 1：1米2：5米3：10米，4：30米，5：100米或更長：看不見的處理
        m_group.SetDistanceReferencePoint(Camera.main.transform);
        m_group.SetBoundingDistances(new float[] { 1, 5, 10, 30, 100 });

        // 設置列表以執行可見性確定
        m_bounds = new BoundingSphere[targets.Length];
        for (int i = 0; i < m_bounds.Length; i++)
        {
            m_bounds[i].radius = 1.5f;
        }

        // 註冊對列表的引用
        m_group.SetBoundingSpheres(m_bounds);
        m_group.SetBoundingSphereCount(targets.Length);

        // 註冊當對象可見性發生變化時的回調
        m_group.onStateChanged = OnChange;
    }

    void Update()
    {
        // 更新已註冊對象的座標
        for (int i = 0; i < m_bounds.Length; i++)
        {
            m_bounds[i].position = targets[i].position;
        }
    }

    void OnDestroy()
    {
        // 清理工作
        m_group.onStateChanged -= OnChange;
        m_group.Dispose();
        m_group = null;
    }

    void OnChange(CullingGroupEvent ev)
    {
        // 僅激活不在視圖中的對象
        targets[ev.index].gameObject.SetActive(ev.isVisible);

        // 如果範圍是2m或更多，則取消激活
        if (ev.currentDistance > fov)
        {
            targets[ev.index].gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, fov * 5.5f);
    }

}
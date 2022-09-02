using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullingGroupCamera : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] float fov = 2;

    private CullingGroup m_group = null;
    private Renderer[] targets = null;
    private BoundingSphere[] m_bounds = null;

    #region Main
    private void Start()
    {
        m_group = new CullingGroup();

        //　設置相機進行剔除
        m_group.targetCamera = Camera.main;

        // 設置用於測量距離和距離水平的中心座標
        // 1：1米2：5米3：10米，4：30米，5：100米或更長：看不見的處理
        m_group.SetDistanceReferencePoint(Camera.main.transform);
        m_group.SetBoundingDistances(new float[] { 1, 5, 10, 30, 100 });
        
        // Search layer of all gameobjects in hierarchy.
        var array = Shinn.Utils.Bitmask2Array(layer.value);
        targets = FindGameObjectsWithLayer(array);

        // 設置列表以執行可見性確定
        m_bounds = new BoundingSphere[targets.Length];
        for (int i = 0; i < m_bounds.Length; i++)
            m_bounds[i].radius = 1.5f;

        // 註冊對列表的引用
        m_group.SetBoundingSpheres(m_bounds);
        m_group.SetBoundingSphereCount(targets.Length);

        // 註冊當對象可見性發生變化時的回調
        m_group.onStateChanged = OnChange;
    }

    private void Update()
    {
        for (int i = 0; i < m_bounds.Length; i++)
            m_bounds[i].position = targets[i].transform.position;
    }

    private void OnDestroy()
    {
        m_group.onStateChanged -= OnChange;
        m_group.Dispose();
        m_group = null;
    }

    private void OnDrawGizmos()
    {
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.matrix = rotationMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.forward * 5.5f, 5.5f * fov * new Vector3(1, .25f, 1));
    }
    #endregion

    #region PRIAVTE
    private void OnChange(CullingGroupEvent ev)
    {
        targets[ev.index].enabled = (ev.isVisible);
        
        if (ev.currentDistance > fov)
            targets[ev.index].enabled = (false);
    }

    private Renderer[] FindGameObjectsWithLayer(int[] layer)
    {
        var goArray = FindObjectsOfType<Renderer>();
        List<Renderer> goList = new List<Renderer>();

        for (int i = 0; i < layer.Length; i++)
            for (int j=0; j< goArray.Length; j++)
                if (goArray[j].gameObject.layer.Equals(layer[i]))
                    goList.Add(goArray[j]);
        return goList.ToArray();
    }
    #endregion
}
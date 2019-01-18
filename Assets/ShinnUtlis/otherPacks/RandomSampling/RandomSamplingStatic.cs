using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

[ExecuteInEditMode]
//[RequireComponent(typeof(ParticleSystem)), ExecuteInEditMode]
public class RandomSamplingStatic : MonoBehaviour {

    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;

    GameObject[] point;
    [HideInInspector]
    public Transform[] pointloc;

    bool start = false;
    public Transform[] GetPos { get { return pointloc; }  }

    [Range(0, 1000)]
    public int SamplingCount = 100;

    [Range(0, 1)]
    public float gizmosStroke = .1f;

    ParticleSystem ps;

    [Button]
    public void GenerateParticleSystem()
    {
        GameObject go = new GameObject();
        go.transform.parent = transform;
        go.AddComponent<ParticleSystem>();
        go.name = "RandomSampleGroup";
        go.transform.localPosition = Vector3.zero;

        ps = go.GetComponent<ParticleSystem>();
        StartCoroutine(SetDefaultParticleSys(ps, 1));
    }

    [Button]
    public void Sampling() {
        InitializeIfNeeded();
        m_System.maxParticles = SamplingCount;
        
        var sh = m_System.shape;
        sh.enabled = true;
        sh.shapeType = ParticleSystemShapeType.MeshRenderer;
        sh.mesh = transform.parent.gameObject.GetComponent<Mesh>();
        StartCoroutine(GetParticlesInit());
    }

    [Button]
    public void Clear() {
        
        int childs = transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
        }

        point = new GameObject[0];
        start = false;
        
    }

    private IEnumerator SetDefaultParticleSys(ParticleSystem ps, float time)
    {
        yield return new WaitForSeconds(time);

        ParticleSystem.MainModule _main;
        _main = ps.main;

        _main.loop = false;
        _main.startLifetime = Mathf.Infinity;
        _main.startSize = .1f;
        _main.startSpeed = 0;
        _main.startColor = Color.red;
        _main.maxParticles = 100;

        ParticleSystem.EmissionModule _emissiom;
        _emissiom = ps.emission;

        _emissiom.enabled = true;
        _emissiom.rateOverTime = Mathf.Infinity;

        ParticleSystem.ShapeModule _shape;
        _shape = ps.shape;

        _shape.shapeType = ParticleSystemShapeType.MeshRenderer;
        _shape.meshShapeType = ParticleSystemMeshShapeType.Triangle;
        _shape.meshRenderer = GetComponent<MeshRenderer>();

        ParticleSystemRenderer _render;
        _render = ps.GetComponent<ParticleSystemRenderer>();

        _render.material = new Material(Shader.Find("Sprites/Default"));
        //_render.material.mainTexture = 
    }

    private IEnumerator GetParticlesInit() {
        yield return new WaitForSeconds(.25f);
        int numParticlesAlive = m_System.GetParticles(m_Particles);

        point = new GameObject[numParticlesAlive];
        pointloc = new Transform[numParticlesAlive];

        for (int i = 0; i < point.Length; i++)
        {
            point[i] = new GameObject();
            point[i].name = "node" + (i + 1);
            point[i].transform.localPosition = m_Particles[i].position;
            point[i].transform.localEulerAngles = Vector3.zero;
            point[i].transform.localScale = Vector3.one;
            point[i].transform.parent = transform;

            pointloc[i] = point[i].transform;
        }
        start = true;
    }

    private void InitializeIfNeeded()
    {
        if (m_System == null)
            m_System = GetComponent<ParticleSystem>();

        if (m_Particles == null || m_Particles.Length < m_System.main.maxParticles)
            m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles];
    }

    private void OnDrawGizmos()
    {
        if (start)
        {
            for (int i = 0; i < point.Length; i++)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(point[i].transform.position, gizmosStroke);
            }
        }
    }


}

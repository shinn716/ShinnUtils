using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

[ExecuteInEditMode]
public class RandomSamplingStatic : MonoBehaviour
{

    private ParticleSystem m_System;
    private ParticleSystem.Particle[] m_Particles;
    private GameObject[] point;
    private bool start = false;

    [HideInInspector]
    public Transform[] pointloc;
    public Transform[] GetPos { get { return pointloc; } }

    [Range(1, 1000)]
    public int SamplingCount = 100;

    [Range(0, 1)]
    public float gizmosStroke = .1f;

    private void OnValidate()
    {
        if (m_System != null)
        {
            ParticleSystem.MainModule _main;
            _main = m_System.main;
            _main.maxParticles = SamplingCount;
        }
    }

    [Button]
    public void GenerateParticleSystem()
    {
        if (GetComponent<ParticleSystem>() == null)
        {
            gameObject.AddComponent<ParticleSystem>();
            gameObject.name = "RandomSampleGroup";
            gameObject.transform.localPosition = Vector3.zero;

            ParticleSystem ps;
            ps = GetComponent<ParticleSystem>();
            SetDefaultParticleSys(ps);

            ps.Stop();
        }
    }

    [Button]
    public void RemoveParticleSystem()
    {
        if (GetComponent<ParticleSystem>() != null)
            DestroyImmediate(GetComponent<ParticleSystem>());
    }

    [Button]
    public void Sampling()
    {
        if (GetComponent<ParticleSystem>() != null)
        {
            ClearSamples();

            ParticleSystem ps = GetComponent<ParticleSystem>();
            ps.Clear();
            ps.Play();

            Invoke("InitializeIfNeeded", .1f);
            Invoke("GetParticlesInit", .1f);
        }
    }

    [Button]
    public void ClearSamples()
    {
        int childs = transform.childCount;
        for (int i = childs - 1; i >= 0; i--)        
            GameObject.DestroyImmediate(transform.GetChild(i).gameObject);        

        point = new GameObject[0];
        start = false;
    }

    private void SetDefaultParticleSys(ParticleSystem ps)
    {
        ParticleSystem.MainModule _main;
        _main = ps.main;

        _main.loop = false;
        _main.startLifetime = Mathf.Infinity;
        _main.startSize = .1f;
        _main.startSpeed = 0;
        _main.startColor = Color.red;
        _main.maxParticles = 100;
        _main.simulationSpace = ParticleSystemSimulationSpace.World;

        ParticleSystem.EmissionModule _emissiom;
        _emissiom = ps.emission;

        _emissiom.enabled = true;
        _emissiom.rateOverTime = Mathf.Infinity;

        ParticleSystem.ShapeModule _shape;
        _shape = ps.shape;

        _shape.shapeType = ParticleSystemShapeType.Mesh;
        _shape.meshShapeType = ParticleSystemMeshShapeType.Triangle;
        _shape.mesh = transform.parent.GetComponent<MeshFilter>().sharedMesh;

        ParticleSystemRenderer _render;
        _render = ps.GetComponent<ParticleSystemRenderer>();

        _render.material = new Material(Shader.Find("Sprites/Default"));
    }

    private void GetParticlesInit()
    {
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

        m_System.Stop();        
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwamPoint : MonoBehaviour
{
    public float spwamSpeed = .1f;

    void Start()
    {
        InvokeRepeating("Spwam", 0, spwamSpeed);
    }

    void Spwam()
    {
        Shinn.ObjectPool.Instance.SpawnFromPool("cube", Vector3.zero, Quaternion.identity, Vector3.one);
    }
}

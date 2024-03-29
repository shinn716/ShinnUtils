using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public enum RotateType
    {
        Constant,
        Noise
    }
    private float ReturnValueFromSelect()
    {
        switch (rotaround)
        {
            case RotateType.Constant:
                return Time.deltaTime * Speed;

            case RotateType.Noise:
                timedata += .1f;
                return BaseValue + Speed * Mathf.PerlinNoise(timedata * NoiseSpeed, 0.0F);

            default:
                return Time.deltaTime;
        }
    }

    public RotateType rotaround = RotateType.Constant;

    [Header("Constant"), Range(-100, 100)]
    public float Speed = 1;
    [Header("Noise")]
    public float BaseValue = 0;
    [Range(0, 1)]
    public float NoiseSpeed = .5f;

    public float timedata = 0;

    [Header("Lookat")]
    public bool lookat = false;
    public Transform target;

    private void FixedUpdate()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, ReturnValueFromSelect());

        if (lookat)
            transform.LookAt(target);
    }
}

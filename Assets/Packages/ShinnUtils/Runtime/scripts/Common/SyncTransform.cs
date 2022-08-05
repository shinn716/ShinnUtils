using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncTransform : MonoBehaviour
{
    #region DECLARE
    [SerializeField]
    private Transform target = null;

    [SerializeField]
    private float speed = 10;

    [Header("Rotation"), SerializeField]
    private bool syncRotation = false;

    [SerializeField]
    private bool freezeRotX = false;

    [SerializeField]
    private bool freezeRotY = false;

    [SerializeField]
    private bool freezeRotZ = false;

    private Vector3 offset = Vector3.zero;
    private Vector3 rot = Vector3.zero;

    #endregion

    #region MAIN
    private void Start()
    {
        offset = target.position - transform.position;
    }

    private void FixedUpdate()
    {
        if (target == null)
            return;

        transform.position = Vector3.Lerp(transform.position, target.position - offset, Time.fixedDeltaTime * speed);

        if (!syncRotation)
            return;

        rot = target.rotation.eulerAngles;
        rot = freezeRotX ? Vector3.Scale(rot, new Vector3(0, 1, 1)) : rot;
        rot = freezeRotY ? Vector3.Scale(rot, new Vector3(1, 0, 1)) : rot;
        rot = freezeRotZ ? Vector3.Scale(rot, new Vector3(1, 1, 0)) : rot;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rot), Time.fixedDeltaTime * speed);
    }
    #endregion
}
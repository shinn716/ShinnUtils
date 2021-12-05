using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinn
{
    public class CopyTransform : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float speed = 10;

        [Header("Rotation"), SerializeField] bool syncRotation = false;
        [SerializeField] bool freezeRotX;
        [SerializeField] bool freezeRotY;
        [SerializeField] bool freezeRotZ;

        private Vector3 offset;
        private Vector3 rot;

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
    }
}
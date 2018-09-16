using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinn
{
    public class LookAt : MonoBehaviour
    {

        [SerializeField] Transform target;
        [SerializeField] float speed = .01f;
        [SerializeField] bool onThrGround = false;

        void FixedUpdate()
        {

            Vector3 direction = target.position - transform.position;

            if (onThrGround)
                direction.y = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), speed);
        }
    }

}
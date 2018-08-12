using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShinnUtil
{

    public class Follower : MonoBehaviour
    {


        [SerializeField] Transform target;

        public Transform Target
        {
            set { target = value; }
        }


        [SerializeField] float ChaseSpeed = .1f;
        [SerializeField] float StopDist = 2;
        [SerializeField] bool OntheGround = false;

        void FixedUpdate()
        {

            Vector3 direction = target.transform.position - transform.position;

            if (OntheGround)
                direction.y = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), .1f);

            if (direction.magnitude > StopDist)
            {
                //GetComponent<Animator> ().SetBool ("run", true);
                transform.Translate(0, 0, ChaseSpeed);

                //if(anim.GetCurrentAnimatorStateInfo(0).IsName("walk") )
                //	transform.Translate (0, 0, ChaseSpeed);

            }
            else
            {
                //GetComponent<Animator> ().SetBool ("run", false);
            }


        }
    }

}

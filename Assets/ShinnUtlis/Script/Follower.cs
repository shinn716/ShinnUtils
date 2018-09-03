using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinn
{

    public class Follower : MonoBehaviour
    {
        [Header("Chase Target")]
        public bool enable = true;
        public bool Enable {
            set { enable = value; }
        }
        public Transform target;
        public Transform Target
        {
            set { target = value; }
        }

        [Header("Chase speed and rotation speed."), Range(0, 1)]
        public float chaseSpeed = .1f;
        [Range(0, 1)]
        public float rotationSpeed = .1f;

        [Header("Stop distance"), Range(0, 10)]
        public float stopDist = 2;

        [Header("Freeze RotY")]
        public bool onTheGround = false;

        [Header("Animator")]
        public bool enableAnimation = false;
        public Animator anim;
        public string moveAnimName = "walk";
        public string encounterAnimName = "attack";

        void FixedUpdate()
        {
            if (enable)
            {

                Vector3 direction = target.transform.position - transform.position;

                if (onTheGround)
                    direction.y = 0;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed);

                if (direction.magnitude > stopDist)
                {
                    if (enableAnimation)
                    {
                        if (anim == null)
                        {
                            Debug.LogError("Need assign an animator.");
                            return;
                        }

                        if (anim.GetCurrentAnimatorStateInfo(0).IsName("run"))
                        {
                            anim.SetBool("run", true);
                            anim.SetBool("attack", false);
                            //transform.Translate(0, 0, chaseSpeed);
                            transform.position = Vector3.Lerp(transform.position, target.position, chaseSpeed);
                        }
                    }
                    else
                        //transform.Translate(0, 0, chaseSpeed);
                        transform.position = Vector3.Lerp(transform.position, target.position, chaseSpeed);
                }
                else
                {
                    if (enableAnimation)
                    {
                        if (anim == null)
                        {
                            Debug.LogError("Need assign an animator.");
                            return;
                        }
                        anim.SetBool("run", false);
                        anim.SetBool("attack", true);
                    }
                }


            }

        }
    }

}

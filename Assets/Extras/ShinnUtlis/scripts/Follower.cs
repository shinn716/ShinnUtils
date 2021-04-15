using UnityEngine;

namespace Shinn
{
    public class Follower : MonoBehaviour
    {
        public enum MoveType
        {
            Lerp,
            Translate
        }

        [Header("Chase Target")]
        public bool enable = true;
        public Transform target;
        public MoveType type = MoveType.Lerp;

        [Header("Chase speed and rotation speed."), Range(0, 1)]
        public float chaseSpeed = .1f;
        [Range(0, 1)]
        public float rotationSpeed = .1f;

        [Header("Stop distance"), Range(0, 10)]
        public float stopDist = 2;

        [Header("Freeze RotY")]
        public bool onTheGround;

        private bool MoveTypeState()
        {
            switch (type)
            {
                default:
                    return false;
                case MoveType.Lerp:
                    return true;
                case MoveType.Translate:
                    return false;
            }
        }

        void FixedUpdate()
        {
            if (!enable)
                return;

            Vector3 direction = target.transform.position - transform.position;

            if (onTheGround)
                direction.y = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed);

            if (direction.magnitude > stopDist)
            {
                if (MoveTypeState())
                    transform.position = Vector3.Lerp(transform.position, target.position, chaseSpeed);
                else
                    transform.Translate(0, 0, chaseSpeed);
            }
        }
    }

}

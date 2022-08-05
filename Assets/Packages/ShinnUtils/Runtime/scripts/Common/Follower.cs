using UnityEngine;

public class Follower : MonoBehaviour
{
    #region DECLARE
    [Header("Chase Target"), SerializeField]
    private bool enable = true;

    [SerializeField]
    private Transform target = null;

    [SerializeField]
    private MoveType type = MoveType.Lerp;

    [Header("Chase speed and rotation speed."), Range(0, 1)]
    private float chaseSpeed = .1f;

    [Range(0, 1)]
    private float rotationSpeed = .1f;

    [Header("Stop distance"), Range(0, 10)]
    private float stopDist = 2;

    [Header("Freeze RotY")]
    private bool onTheGround = false;
    #endregion

    #region MAIN
    private void FixedUpdate()
    {
        if (target == null)
            return;

        if (!enable)
            return;

        Vector3 direction = target.transform.position - transform.position;

        if (onTheGround)
            direction.y = 0;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed);

        if (direction.magnitude > stopDist)
        {
            if (type == MoveType.Lerp)
                transform.position = Vector3.Lerp(transform.position, target.position, chaseSpeed);
            else
                transform.Translate(0, 0, chaseSpeed);
        }
    }
    #endregion

    #region PRIVATE
    private enum MoveType
    {
        Lerp,
        Translate
    }
    #endregion
}


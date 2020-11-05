using UnityEngine;

namespace Shinn
{
    public class StreetViewCamera : MonoBehaviour
    {
        [Range(.1f, 5)]
        public float speed = 3.5f;
        
        private float rollY;
        private float X;
        private float Y;

        private void Start()
        {
            rollY = Camera.main.fieldOfView;
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButton(1))
            {
                transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * speed, -Input.GetAxis("Mouse X") * speed, 0));
                X = transform.rotation.eulerAngles.x;
                Y = transform.rotation.eulerAngles.y;
                transform.rotation = Quaternion.Euler(X, Y, 0);
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                // scroll up
                Camera.main.fieldOfView = rollY-=3;
            }
            else if(Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                // scroll down
                Camera.main.fieldOfView = rollY+=3;
            }
        }
    }
}

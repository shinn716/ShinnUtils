using UnityEngine;

namespace Shinn.CameraTools
{
    public class MouseController : MonoBehaviour
    {
        private enum MouseSate
        {
            WheelRoll,
            CenterDrag,
            RightDrag
        }

        private MouseSate mouseSate;

        private float speed = .1f;

        public bool FreezeYaw = true;

        public float ScrollWheel { get { return Input.GetAxis("Mouse ScrollWheel"); } }

        [ContextMenu("YawSetZero")]
        public void YawSetZero()
        {
            transform.localEulerAngles = Vector3.Scale(transform.localEulerAngles, new Vector3(1, 1, 0));
        }

        private void StateSelect(MouseSate state)
        {
            float h = 0, v = 0;
            switch (state)
            {
                case MouseSate.RightDrag:
                    h = speed * 30 * -Input.GetAxis("Mouse Y");
                    v = speed * 30 * Input.GetAxis("Mouse X");
                    transform.Rotate(h, v, 0);
                    break;
                case MouseSate.CenterDrag:
                    h = speed * -Input.GetAxis("Mouse X");
                    v = speed * -Input.GetAxis("Mouse Y");
                    transform.Translate(h, v, 0);
                    break;
                case MouseSate.WheelRoll:
                    transform.Translate(0, 0, ScrollWheel);
                    break;
                default:
                    break;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButton(1))
                StateSelect(MouseSate.RightDrag);
            if (Input.GetMouseButton(2))
                StateSelect(MouseSate.CenterDrag);
            else
                StateSelect(MouseSate.WheelRoll);

            if(FreezeYaw)
                transform.localEulerAngles = Vector3.Scale(new Vector3(1,1,0), transform.localEulerAngles);
        }
    }
}

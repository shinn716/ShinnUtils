//
//  MouseController.cs
//  MouseController
//
//  Created by Shinn on 2019/8/30.
//  Copyright © 2019 Shinn. All rights reserved.
//
//
//  // MonoBehaviour Sample
//  MouseController mouseController;
//  void Start()
//  {
//      mouseController = new MouseController();
//      mouseController.Init(this);
//  }
//  
//  void Update()
//  {
//      mouseController.Loop();
//  }
//  // MonoBehaviour Sample

using UnityEngine;

namespace Shinn.CameraTools
{
    /// <summary>
    /// Mouse state.
    /// </summary>
    public enum MouseStateEvents
    {
        WheelRoll,
        CenterDrag,
        RightDrag
    }

    public class MouseController
    {
        private GameObject mainObj;
        private bool initst;

        private float ScrollWheel { get { return Input.GetAxis("Mouse ScrollWheel"); } }

        #region Main function
        /// <summary>
        /// Init
        /// </summary>
        /// <param name="mono"></param>
        public void Init(MonoBehaviour mono)
        {
            mainObj = mono.gameObject;
            initst = true;
        }

        /// <summary>
        /// Loop
        /// </summary>
        public void Loop()
        {
            if (initst)
            {
                if (Input.GetMouseButton(1))
                    MouseStateSelect(MouseStateEvents.RightDrag);

                if (Input.GetMouseButton(2))
                    MouseStateSelect(MouseStateEvents.CenterDrag);

                else
                    MouseStateSelect(MouseStateEvents.WheelRoll);

                YawSetZero(true);
            }

            else
            {
                Debug.LogWarning("Need run init.");
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            initst = false;
            mainObj = null;
        }
        #endregion

        #region Public Function
        /// <summary>
        /// Set camera move speed.
        /// </summary>
        public virtual float SetMoveSpeed { get; set; } = 1f;

        /// <summary>
        /// Set camera rotate speed.
        /// </summary>
        public virtual float SetRotateSpeed { get; set; } = 5f;

        /// <summary>
        /// Set camera rotate speed.
        /// </summary>
        public virtual float SetRollSpeed { get; set; } = .25f;

        /// <summary>
        /// set zoom in/out speed.
        /// </summary>
        /// <param name="freezeTaw"></param>
        public void YawSetZero(bool freezeTaw)
        {
            if (freezeTaw)
                mainObj.transform.localEulerAngles = Vector3.Scale(new Vector3(1, 1, 0), mainObj.transform.localEulerAngles);
        }

        /// <summary>
        /// Mouse right drag, center drag, wheel roll event.
        /// </summary>
        /// <param name="state"></param>
        public void MouseStateSelect(MouseStateEvents state)
        {
            float h, v;
            switch (state)
            {
                case MouseStateEvents.RightDrag:
                    h = SetRotateSpeed * -Input.GetAxis("Mouse Y");
                    v = SetRotateSpeed * Input.GetAxis("Mouse X");
                    Quaternion q = mainObj.transform.rotation * Quaternion.Euler(h, v, 0);
                    mainObj.transform.rotation = Quaternion.Slerp(mainObj.transform.rotation, q, Time.time);
                    break;
                case MouseStateEvents.CenterDrag:
                    h = SetMoveSpeed * -Input.GetAxis("Mouse X");
                    v = SetMoveSpeed * -Input.GetAxis("Mouse Y");
                    mainObj.transform.Translate(h, v, 0);
                    break;
                case MouseStateEvents.WheelRoll:
                    mainObj.transform.Translate(0, 0, ScrollWheel * SetRollSpeed);
                    break;
            }
        }
        #endregion
    }
}
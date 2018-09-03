using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Shinn {

    [RequireComponent(typeof(Rigidbody))]
    public class ColliderEvent : MonoBehaviour {

        public enum type{
            Trigger,
            Collision,
            None
        }

        bool tri = false;
        bool col = false;

        [Space]
        public type mytype;
        public string tagname = "TagName";

        [Space]
        [Header("CompleteEvent")]
        public bool EnableBool = false;
        public bool EnableInt = false;
        public bool EnableFloat = false;
        public bool EnableFloatArray = false;
        public bool EnableColor = false;
        public bool EnableVoid = false;

        public VoidEvent voidevents;
        public BoolEvent boolevents;
        public IntEvent intevents;
        public FloatEvent floatevents;
        public FloatArrayEvent floatarratevents;
        public ColorEvent colorevents;

        public bool boolvalue;
        public int intvalue;
        public float floatvalue;
        public float[] floatarrayvalue;
        public Color colorvalue;

        private void Start()
        {
            switch (mytype) {

                default:
                    tri = false;
                    col = false;
                    break;

                case type.Trigger:
                    tri = true;
                    col = false;
                    break;

                case type.Collision:
                    tri = false;
                    col = true;
                    break;
            }
        }

        //OnTriggerEnter、OnTriggerStay、OnTriggerExit
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == tagname && tri)
            {
                if (EnableBool)
                    boolevents.Invoke(boolvalue);

                if (EnableInt)
                    intevents.Invoke(intvalue);

                if (EnableFloat)
                    floatevents.Invoke(floatvalue);

                if (EnableFloatArray)
                    floatarratevents.Invoke(floatarrayvalue);

                if (EnableColor)
                    colorevents.Invoke(colorvalue);

                if (EnableVoid)
                    voidevents.Invoke();
            }
        }

        //OnCollisionEnter、OnCollisionStay、OnCollisionEXit
        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == tagname && col)
            {
                if (EnableBool)
                    boolevents.Invoke(boolvalue);

                if (EnableInt)
                    intevents.Invoke(intvalue);

                if (EnableFloat)
                    floatevents.Invoke(floatvalue);

                if (EnableFloatArray)
                    floatarratevents.Invoke(floatarrayvalue);

                if (EnableColor)
                    colorevents.Invoke(colorvalue);

                if (EnableVoid)
                    voidevents.Invoke();
            }
        }
    }

}

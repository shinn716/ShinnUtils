using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;

namespace Shinn
{

    public class SingleValueController : MonoBehaviour
    {

        [Header("float to")]
        public Vector2 valuerange = new Vector2(0, 1);
        public FloatEvent floatevents;
        //public bool isfloatevents = false;

        [Header("ItweenSetting")]
        public GameObject target;
        public float time;
        public float delay = 0;
        public iTween.EaseType ease;
        public iTween.LoopType loop;
        public bool ignoreTimeScalest = false;

        public bool complete = false;
        public UnityEvent unityevent;

        void OnEnable()
        {


            if (target == null)
                target = gameObject;


            // if (isfloatevents)
            iTween.ValueTo(target, iTween.Hash("from", valuerange.x, "to", valuerange.y, "onupdate", "floateventsProcess",
                                                "time", time, "delay", delay,
                                                "easetype", ease, "looptype", loop,
                                                "ignoretimescale", ignoreTimeScalest,
                                                "oncomplete", "Complete", "oncompletetarget", gameObject
                                            ));
        }


        void floateventsProcess(float newvalue)
        {
            floatevents.Invoke(newvalue);
        }

        void Complete()
        {
            if (complete)
                unityevent.Invoke();
        }

    }

}

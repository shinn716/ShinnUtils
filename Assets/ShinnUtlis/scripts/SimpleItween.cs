using UnityEngine;
using UnityEngine.UI;

namespace Shinn
{

    public class SimpleItween : MonoBehaviour
    {

        #region Itween
        public enum State
        {
            shakePosition,
            punchPosition,
            scaleTo,
            scaleFrom,
            moveTo,
            moveToPx,
            moveToPy,
            moveToPz,
            rotationTo,
            SP_fadeTo,
            colorTo,
            rotationToAndMoveTo,
        }

        public State mystate;

        [Header("ItweenSetting")]
        public GameObject target;
        public float time;
        public float delay = 0;
        public iTween.EaseType ease;
        public iTween.LoopType loop;
        public bool islocal = false;
        public bool ignoreTimeScalest = false;
        public bool AutoStart = true;
        public bool orienttopathst = false;
        public float lookaheadValue = .05f;

        [Header("ShakePosition")]
        public Vector3 shakePos;

        [Header("PunchPosition")]
        public Vector3 punchPos;

        [Header("ScaleState")]
        public Vector3 scaleValue;

        [Header("MoveTo")]
        public Transform moveloc;

        [Header("RotationTo")]
        public Vector3 rotvalue;

        [Header("ColorTo")]
        public Color startColor;
        public Color endColor;

        [Header("FadeTo")]
        public float fadeStart = 0;
        public float fadeEnd = 1;

        [Header("CompleteEvent")]
        public bool startComplete = false;

        #endregion

        #region UnityEvents
        public bool EnableBool = false;
        public bool EnableInt = false;
        public bool EnableFloat = false;
        public bool EnableFloatArray = false;
        public bool EnableVector3 = false;
        public bool EnableColor = false;
        public bool EnableVoid = false;

        public VoidEvent voidevents;
        public BoolEvent boolevents;
        public IntEvent intevents;
        public FloatEvent floatevents;
        public FloatArrayEvent floatarratevents;
        public Vector3Event vector3events;
        public ColorEvent colorevents;

        public bool boolvalue;
        public int intvalue;
        public float floatvalue;
        public float[] floatarrayvalue;
        public Vector3 vector3value;
        public Color colorvalue;
        #endregion


        #region public function
        public void CallStart()
        {
            StartSimpleItweenFunction();
        }

        public void Pause()
        {
            iTween.Pause(gameObject);
        }

        public void Resume()
        {
            iTween.Resume(gameObject);
        }

        public void Stop()
        {
            iTween.Stop(gameObject);
        }

        public void SetScale(Vector3 sclvalue)
        {
            transform.localScale = sclvalue;
        }
        #endregion

        #region private function
        private void OnEnable()
        {
            if (target == null)
                target = gameObject;

            if (AutoStart)
                StartSimpleItweenFunction();
        }


        private void StartSimpleItweenFunction()
        {
            switch (mystate)
            {
                case State.shakePosition:
                    iTween.ShakePosition(target, iTween.Hash("x", shakePos.x, "y", shakePos.y, "z", shakePos.z,
                                                                "time", time, "delay", delay,
                                                                "easetype", ease, "looptype", loop,
                                                                "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                             ));
                    break;

                case State.punchPosition:
                    iTween.PunchPosition(target, iTween.Hash("x", punchPos.x, "y", punchPos.y, "z", punchPos.z,
                                                                "time", time, "delay", delay,
                                                                "easetype", ease, "looptype", loop,
                                                                "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                            ));
                    break;

                case State.SP_fadeTo:
                    iTween.ValueTo(target, iTween.Hash("from", fadeStart, "to", fadeEnd, "onupdate", "Fadeto1",
                                                                "time", time, "delay", delay,
                                                                "easetype", ease, "looptype", loop,
                                                                "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                            ));
                    break;

                case State.scaleTo:
                    iTween.ScaleTo(target, iTween.Hash("scale", scaleValue,
                                                                "time", time, "delay", delay,
                                                                "easetype", ease, "looptype", loop,
                                                                "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                            ));
                    break;

                case State.scaleFrom:
                    iTween.ScaleFrom(target, iTween.Hash("scale", scaleValue,
                                            "time", time, "delay", delay,
                                            "easetype", ease, "looptype", loop,
                                            "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                            "oncomplete", "Complete", "oncompletetarget", gameObject,
                                            "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                        ));
                    break;

                case State.moveTo:
                    if (islocal)
                        iTween.MoveTo(target, iTween.Hash("position", moveloc.localPosition,
                                                                    "time", time, "delay", delay,
                                                                    "easetype", ease, "looptype", loop,
                                                                    "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                    "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                     "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                             ));
                    else
                        iTween.MoveTo(target, iTween.Hash("position", moveloc.position,
                                                                    "time", time, "delay", delay,
                                                                    "easetype", ease, "looptype", loop,
                                                                    "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                    "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                    "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                             ));

                    break;


                case State.moveToPx:
                    if (islocal)
                        iTween.MoveTo(target, iTween.Hash("x", moveloc.localPosition.x,
                                                          "time", time, "delay", delay,
                                                          "easetype", ease, "looptype", loop,
                                                          "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                          "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                          "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                          ));
                    else
                        iTween.MoveTo(target, iTween.Hash("x", moveloc.position.x,
                                                          "time", time, "delay", delay,
                                                          "easetype", ease, "looptype", loop,
                                                          "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                          "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                          "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                           ));

                    break;

                case State.moveToPy:
                    if (islocal)
                        iTween.MoveTo(target, iTween.Hash("y", moveloc.localPosition.y,
                                                          "time", time, "delay", delay,
                                                          "easetype", ease, "looptype", loop,
                                                          "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                          "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                          "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                          ));
                    else
                        iTween.MoveTo(target, iTween.Hash("y", moveloc.position.y,
                                                          "time", time, "delay", delay,
                                                          "easetype", ease, "looptype", loop,
                                                          "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                          "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                          "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                           ));

                    break;

                case State.moveToPz:
                    if (islocal)
                        iTween.MoveTo(target, iTween.Hash("z", moveloc.localPosition.z,
                                                          "time", time, "delay", delay,
                                                          "easetype", ease, "looptype", loop,
                                                          "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                          "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                          "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                          ));
                    else
                        iTween.MoveTo(target, iTween.Hash("z", moveloc.position.z,
                                                          "time", time, "delay", delay,
                                                          "easetype", ease, "looptype", loop,
                                                          "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                          "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                          "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                           ));

                    break;


                case State.rotationTo:
                    iTween.RotateTo(target, iTween.Hash("rotation", rotvalue,
                                                                "time", time, "delay", delay,
                                                                "easetype", ease, "looptype", loop,
                                                                "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                            ));
                    break;



                case State.colorTo:
                    iTween.ColorTo(target, iTween.Hash("color", endColor,
                                                                "time", time, "delay", delay,
                                                                "easetype", ease, "looptype", loop,
                                                                "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                            ));
                    break;


                case State.rotationToAndMoveTo:

                    if (islocal)
                    {
                        iTween.MoveTo(target, iTween.Hash("position", moveloc.localPosition,
                                                                    "time", time, "delay", delay,
                                                                    "easetype", ease, "looptype", loop,
                                                                    "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                    "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                     "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                             ));

                        iTween.RotateTo(target, iTween.Hash("rotation", moveloc.localEulerAngles,
                                                            "time", time, "delay", delay,
                                                            "easetype", ease, "looptype", loop,
                                                            "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                            "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                            "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                            ));
                    }
                    else
                    {
                        iTween.MoveTo(target, iTween.Hash("position", moveloc.position,
                                                                    "time", time, "delay", delay,
                                                                    "easetype", ease, "looptype", loop,
                                                                    "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                    "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                    "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                             ));

                        iTween.RotateTo(target, iTween.Hash("rotation", moveloc.eulerAngles,
                                                            "time", time, "delay", delay,
                                                            "easetype", ease, "looptype", loop,
                                                            "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                            "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                            "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                       ));
                    }
                    break;
            }
        }

        private void Fadeto1(float newvalue)
        {
            if (GetComponent<SpriteRenderer>() != null)
            {
                SpriteRenderer sp = GetComponent<SpriteRenderer>();
                Color orgCol = sp.color;
                sp.color = new Color(orgCol.r, orgCol.g, orgCol.b, newvalue);
            }
            else if (GetComponent<Image>() != null)
            {
                Image img = GetComponent<Image>();
                Color orgCol = img.color;
                img.color = new Color(orgCol.r, orgCol.g, orgCol.b, newvalue);
            }
            else if (GetComponent<Text>() != null)
            {
                Text text = GetComponent<Text>();
                Color orgCol = text.color;
                text.color = new Color(orgCol.r, orgCol.g, orgCol.b, newvalue);
            }
        }

        private void Complete()
        {
            if (startComplete)
            {
                if (EnableBool)
                    boolevents.Invoke(boolvalue);

                if (EnableInt)
                    intevents.Invoke(intvalue);

                if (EnableFloat)
                    floatevents.Invoke(floatvalue);

                if (EnableFloatArray)
                    floatarratevents.Invoke(floatarrayvalue);

                if (EnableVector3)
                    vector3events.Invoke(vector3value);

                if (EnableColor)
                    colorevents.Invoke(colorvalue);

                if (EnableVoid)
                    voidevents.Invoke();
            }
        }

        private void OnDisable()
        {
            var itween = target.GetComponent<iTween>();
            if (itween != null)
                Destroy(itween);
        }

        #endregion



    }

}

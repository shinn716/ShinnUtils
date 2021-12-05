//
//  SimpleItween.cs
//  SimpleItween
//  itween http://www.pixelplacement.com/itween/index.php
//
//  Created by Shinn on 2019/8/25.
//  Copyright © 2019 Shinn. All rights reserved.
//

using UnityEngine;
using UnityEngine.UI;

namespace Shinn
{
    public class SimpleItween : MonoBehaviour
    {
        #region Itween
        [SerializeField]
        private enum State
        {
            shakePosition,
            punchPosition,
            scaleTo,
            scaleFrom,
            moveTo,
            moveToPxObj,
            moveToPyObj,
            moveToPzObj,
            rotationTo,
            SP_fadeTo,
            colorTo,
            rotationToAndMoveTo,
            moveToPos
        }

        [SerializeField] State mystate = State.moveTo;
        [SerializeField] GameObject target = null;
        [SerializeField] float time = 0;
        [SerializeField] float delay = 0;
        [SerializeField] iTween.EaseType ease = iTween.EaseType.easeOutQuad;
        [SerializeField] iTween.LoopType loop = iTween.LoopType.none;
        [SerializeField] bool islocal = false;
        [SerializeField] bool ignoreTimeScalest = false;
        [SerializeField] bool AutoStart = true;
        [SerializeField] bool orienttopathst = false;
        [SerializeField] float lookaheadValue = .05f;
        [SerializeField] Vector3 shakePos = Vector3.zero;
        [SerializeField] Vector3 punchPos = Vector3.zero;
        [SerializeField] Vector3 scaleValue = Vector3.zero;
        [SerializeField] Transform moveloc = null;
        [SerializeField] Vector3 rotvalue = Vector3.zero;
        //[SerializeField] Color startColor = Color.white;
        [SerializeField] Color endColor = Color.white;
        [Range(0, 1)] [SerializeField] float fadeStart = 0;
        [Range(0, 1)] [SerializeField] float fadeEnd = 1;
        [SerializeField] bool startComplete = false;
        [SerializeField] Vector3 posVect = Vector3.zero;
        #endregion

        #region UnityEvents
        [SerializeField] bool EnableBool = false;
        [SerializeField] bool EnableInt = false;
        [SerializeField] bool EnableFloat = false;
        //[SerializeField] bool EnableFloatArray = false;
        [SerializeField] bool EnableVector3 = false;
        [SerializeField] bool EnableColor = false;
        [SerializeField] bool EnableVoid = false;

        [SerializeField] VoidEvent voidevents = null;
        [SerializeField] BoolEvent boolevents = null;
        [SerializeField] IntEvent intevents = null;
        [SerializeField] FloatEvent floatevents = null;
        //[SerializeField] FloatArrayEvent floatarratevents = null;
        [SerializeField] Vector3Event vector3events = null;
        [SerializeField] ColorEvent colorevents = null;

        [SerializeField] bool boolvalue = false;
        [SerializeField] int intvalue = 0;
        [SerializeField] float floatvalue = 0;
        [SerializeField] Vector3 vector3value = Vector3.zero;
        [SerializeField] Color colorvalue = Color.white;
        //[SerializeField] float[] floatarrayvalue;
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
        private void Start()
        {
            if (target == null)
                target = gameObject;
        }

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





                case State.moveToPos:
                    if (islocal)
                    {
                        if (target.GetComponent<RectTransform>() != null)
                        {
                            Vector2 rect = target.GetComponent<RectTransform>().anchoredPosition;
                            //print(target.GetComponent<RectTransform>().anchoredPosition);
                            iTween.ValueTo(target, iTween.Hash(
                            "from", rect.x,
                            "to", posVect.x,
                            "time", time, "delay", delay,
                            "easetype", ease, "looptype", loop,
                            "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                            "oncomplete", "Complete", "oncompletetarget", gameObject,
                            "orienttopath", orienttopathst, "lookahead", lookaheadValue,
                            "onupdatetarget", gameObject, "onupdate", "RectMoveToX"
                            ));

                            iTween.ValueTo(target, iTween.Hash(
                            "from", rect.y,
                            "to", posVect.y,
                            "time", time, "delay", delay,
                            "easetype", ease, "looptype", loop,
                            "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                            "oncomplete", "Complete", "oncompletetarget", gameObject,
                            "orienttopath", orienttopathst, "lookahead", lookaheadValue,
                            "onupdatetarget", gameObject, "onupdate", "RectMoveToY"
                            ));
                        }
                        else
                        {
                            iTween.MoveTo(target, iTween.Hash("position", posVect,
                                      "time", time, "delay", delay,
                                      "easetype", ease, "looptype", loop,
                                      "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                      "oncomplete", "Complete", "oncompletetarget", gameObject,
                                      "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                      ));
                        }
                    }
                    else
                    {
                        if (target.GetComponent<RectTransform>() != null)
                        {
                            Vector2 rect = target.GetComponent<RectTransform>().anchoredPosition;
                            //print(target.GetComponent<RectTransform>().anchoredPosition);
                            iTween.ValueTo(target, iTween.Hash(
                            "from", rect.x,
                            "to", posVect.x,
                            "time", time, "delay", delay,
                            "easetype", ease, "looptype", loop,
                            "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                            "oncomplete", "Complete", "oncompletetarget", gameObject,
                            "orienttopath", orienttopathst, "lookahead", lookaheadValue,
                            "onupdatetarget", gameObject, "onupdate", "RectMoveToX"
                            ));

                            iTween.ValueTo(target, iTween.Hash(
                            "from", rect.y,
                            "to", posVect.y,
                            "time", time, "delay", delay,
                            "easetype", ease, "looptype", loop,
                            "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                            "oncomplete", "Complete", "oncompletetarget", gameObject,
                            "orienttopath", orienttopathst, "lookahead", lookaheadValue,
                            "onupdatetarget", gameObject, "onupdate", "RectMoveToY"
                            ));
                        }
                        else
                        {
                            iTween.MoveTo(target, iTween.Hash("position", posVect,
                              "time", time, "delay", delay,
                              "easetype", ease, "looptype", loop,
                              "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                              "oncomplete", "Complete", "oncompletetarget", gameObject,
                              "orienttopath", orienttopathst, "lookahead", lookaheadValue
                               ));
                        }
                    }

                    break;





                case State.moveToPxObj:
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

                case State.moveToPyObj:
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

                case State.moveToPzObj:
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
            if (target.GetComponent<SpriteRenderer>() != null)
            {
                SpriteRenderer sp = target.GetComponent<SpriteRenderer>();
                Color orgCol = sp.color;
                sp.color = new Color(orgCol.r, orgCol.g, orgCol.b, newvalue);
            }
            else if (target.GetComponent<Image>() != null)
            {
                Image img = target.GetComponent<Image>();
                Color orgCol = img.color;
                img.color = new Color(orgCol.r, orgCol.g, orgCol.b, newvalue);
            }
            else if (target.GetComponent<Text>() != null)
            {
                Text text = target.GetComponent<Text>();
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

                //if (EnableFloatArray)
                //    floatarratevents.Invoke(floatarrayvalue);

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

        private void RectMoveToX(float px)
        {
            try
            {
                target.GetComponent<RectTransform>().anchoredPosition = new Vector2(px, target.GetComponent<RectTransform>().anchoredPosition.y);
            }
            catch (System.Exception e)
            {
                print(e);
            }
        }

        private void RectMoveToY(float py)
        {
            try
            {
                target.GetComponent<RectTransform>().anchoredPosition = new Vector2(target.GetComponent<RectTransform>().anchoredPosition.x, py);
            }
            catch (System.Exception e)
            {
                print(e);
            }
        }

        #endregion
    }

}

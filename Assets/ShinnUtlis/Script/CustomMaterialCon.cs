using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Shinn
{


    public class CustomMaterialCon : MonoBehaviour
    {

        public enum state
        {
            ColorControl,
            ValueControl
        }


        [Header("General setting.")]
        public Material[] mat;
        public string priority = "_emission";
        public bool autoStart = true;
        public state type;

        [Header("Color")]
        //public bool conColor = false;
        public Color startColor = Color.black;
        public Color endColor = Color.white;

        [Header("value")]
        //public bool conValue = false;
        public Vector2 range = new Vector2(0, 1);

        [Space]
        public bool ignoreTimeScalest = false;
        public float delay = 0;
        public float time = 2;
        public iTween.EaseType ease;
        public iTween.LoopType loop;

        public bool complete = false;
        public UnityEvent unityevent;


        void Start()
        {

            if (mat == null)
            {
                for(int i=0; i< mat.Length; i++)
                    mat[i] = GetComponent<Renderer>().material;
            }


            if (autoStart)
            {
                Go();
            }

        }

        public void Go()
        {


            switch (type)
            {

                default:
                    break;

                case state.ColorControl:

                    iTween.ValueTo(gameObject, iTween.Hash("from", startColor.r, "to", endColor.r,
                                                            "onupdate", "ColorRProcess",
                                                            "time", time, "delay", delay,
                                                            "easetype", ease, "looptype", loop,
                                                            "ignoretimescale", ignoreTimeScalest,
                                                            "oncomplete", "Complete", "oncompletetarget", gameObject
                                        ));

                    iTween.ValueTo(gameObject, iTween.Hash("from", startColor.g, "to", endColor.g,
                                                            "onupdate", "ColorGProcess",
                                                            "time", time, "delay", delay,
                                                            "easetype", ease, "looptype", loop,
                                                            "ignoretimescale", ignoreTimeScalest
                                                        ));

                    iTween.ValueTo(gameObject, iTween.Hash("from", startColor.b, "to", endColor.b,
                                                            "onupdate", "ColorBProcess",
                                                            "time", time, "delay", delay,
                                                            "easetype", ease, "looptype", loop,
                                                            "ignoretimescale", ignoreTimeScalest
                                                        ));


                    iTween.ValueTo(gameObject, iTween.Hash("from", startColor.a, "to", endColor.a,
                                                            "onupdate", "ColorAProcess",
                                                            "time", time, "delay", delay,
                                                            "easetype", ease, "looptype", loop,
                                                            "ignoretimescale", ignoreTimeScalest
                                                        ));
                    break;


                case state.ValueControl:
                    for (int i = 0; i < mat.Length; i++)
                        mat[i].SetFloat(priority, range.x);
                    iTween.ValueTo(gameObject, iTween.Hash("from", range.x, "to", range.y,
                                                            "onupdate", "ValueProcess",
                                                            "time", time, "delay", delay,
                                                            "easetype", ease, "looptype", loop,
                                                            "ignoretimescale", ignoreTimeScalest,
                                                            "oncomplete", "Complete", "oncompletetarget", gameObject
                                              ));
                    break;

            }
        }

        #region itween valueto Processing
        void ValueProcess(float newvalue)
        {
            for (int i = 0; i < mat.Length; i++)
                mat[i].SetFloat(priority, newvalue);
        }

        void ColorRProcess(float newvalue)
        {
            for (int i = 0; i < mat.Length; i++)
                mat[i].SetColor(priority, new Color(newvalue, mat[i].GetColor(priority).g, mat[i].GetColor(priority).b, mat[i].GetColor(priority).a));
        }

        void ColorGProcess(float newvalue)
        {
            for (int i = 0; i < mat.Length; i++)
                mat[i].SetColor(priority, new Color(mat[i].GetColor(priority).r, newvalue, mat[i].GetColor(priority).b, mat[i].GetColor(priority).a));
        }

        void ColorBProcess(float newvalue)
        {
            for (int i = 0; i < mat.Length; i++)
                mat[i].SetColor(priority, new Color(mat[i].GetColor(priority).r, mat[i].GetColor(priority).g, newvalue, mat[i].GetColor(priority).a));
        }

        void ColorAProcess(float newvalue)
        {
            for (int i = 0; i < mat.Length; i++)
                mat[i].SetColor(priority, new Color(mat[i].GetColor(priority).r, mat[i].GetColor(priority).g, mat[i].GetColor(priority).b, newvalue));
        }
        #endregion


        void Complete()
        {
            if (complete)
                unityevent.Invoke();
        }



    }

}

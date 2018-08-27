using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleItween : MonoBehaviour
{
    public enum state
    {
        shakePosition,
        scaleto,
        moveto,
        rotationto,
        SP_fadeto,
        colorto,
    }

    public state mystate;

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

    [Header("ScaleState")]
    public Vector3 scaleValue;

    [Header("MoveTo")]
    public Transform moveloc;

    [Header("RotationTo")]
    public Vector3 rotvalue;

    [Header("ColorTo")]
    public Color endColor;

    [Header("FadeTo")]
    public float fadeStart = 0;
    public float fadeEnd = 1;

    [Header("CompleteEvent")]
    public bool startComplete = false;


    //public UnityEvent<> unityevent;
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
    

    /*
    ...
    hashtable.Add("oncomplete", "afterPlayerMove");
    
    //Create oncompleteparams hashtable
    Hashtable paramHashtable = new Hashtable();
    paramHashtable.Add("value1", _fieldIndex);
    paramHashtable.Add("value2", floatVal);
    paramHashtable.Add("value3", stringVal);
    paramHashtable.Add("value4", boolVal);
    paramHashtable.Add("value5", gObjVal);
    //Include the oncompleteparams parameter  to the hashtable
    hashtable.Add("oncompleteparams", paramHashtable);
    
    
    public void afterPlayerMove(object cmpParams)
    {
      Hashtable hstbl = (Hashtable)cmpParams;
      Debug.Log("Your int value " + (int)hstbl["value1"]);
      Debug.Log("Your float value " + (float)hstbl["value2"]);
      Debug.Log("Your string value " + (string)hstbl["value3"]);
      Debug.Log("Your bool value " + (bool)hstbl["value4"]);
      Debug.Log("Your GameObject value " + (GameObject)hstbl["value5"]);
    }
    
    */

    void Start()
    {

        if (target == null)
            target = gameObject;
        

        if (AutoStart)
            Select();
    }


    public void CallStart()
    {
        Select();
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


    void Select()
    {
        switch (mystate)
        {
            case state.shakePosition:
                iTween.ShakePosition(target, iTween.Hash(   "x", shakePos.x, "y", shakePos.y, "z", shakePos.z,
                                                                "time", time, "delay", delay,
                                                                "easetype", ease, "looptype", loop,
                                                                "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                         ));
                break;

            case state.SP_fadeto:
                iTween.ValueTo(target, iTween.Hash(     "from", fadeStart, "to", fadeEnd, "onupdate", "fadeto1",
                                                            "time", time, "delay", delay,
                                                            "easetype", ease, "looptype", loop,
                                                            "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                            "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                            "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                        ));
                break;

            case state.scaleto:
                iTween.ScaleFrom(target, iTween.Hash(   "scale", scaleValue,
                                                            "time", time, "delay", delay,
                                                            "easetype", ease, "looptype", loop,
                                                            "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                            "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                            "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                        ));
                break;

            case state.moveto:
                if (islocal)
                    iTween.MoveTo(target, iTween.Hash(      "position", moveloc.localPosition,
                                                                "time", time, "delay", delay,
                                                                "easetype", ease, "looptype", loop,
                                                                "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                 "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                         ));
                else
                    iTween.MoveTo(target, iTween.Hash(      "position", moveloc.position,
                                                                "time", time, "delay", delay,
                                                                "easetype", ease, "looptype", loop,
                                                                "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                                "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                         ));

                break;
                

            case state.rotationto:
                iTween.RotateTo(target, iTween.Hash(    "rotation", rotvalue,
                                                            "time", time, "delay", delay,
                                                            "easetype", ease, "looptype", loop,
                                                            "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                            "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                            "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                        ));
                break;



            case state.colorto:
                iTween.ColorTo(target, iTween.Hash(     "color", endColor,
                                                            "time", time, "delay", delay,
                                                            "easetype", ease, "looptype", loop,
                                                            "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                            "oncomplete", "Complete", "oncompletetarget", gameObject,
                                                            "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                        ));
                break;
                
        }
    }




    void fadeto1(float newvalue)
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            SpriteRenderer sp = GetComponent<SpriteRenderer>();
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, newvalue);
        }
    }

    void Complete()
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

            if (EnableColor)
                colorevents.Invoke(colorvalue);

            if (EnableVoid)
                voidevents.Invoke();
        }
    }




}
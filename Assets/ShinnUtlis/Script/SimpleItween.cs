using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleItween : MonoBehaviour
{
    public enum state
    {
        shakePosition,
        SP_fadein,
		SP_fadeout,
        scaleIn,
        moveto, 
        rotationto
    }

    public state mystate;

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

    [Header("CompleteEvent")]
    public UnityEvent unityevent;
    
    
    
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
			iTween.ShakePosition(gameObject, iTween.Hash(   "x", shakePos.x, "y", shakePos.y, "z", shakePos.z , 
                                                            "time", time, "delay", delay, 
                                                            "easetype", ease, "looptype", loop, 
                                                            "islocal", islocal, "ignoretimescale", ignoreTimeScalest, 
                                                            "oncomplete", "Complete", "oncompletetarget", gameObject,
							    "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                        ));
                break;

			case state.SP_fadein:
				iTween.ValueTo(gameObject, iTween.Hash(     "from", 0, "to", 1, "onupdate", "fadeto1",
                                                            "time", time, "delay", delay,
                                                            "easetype", ease, "looptype", loop,
                                                            "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                            "oncomplete", "Complete", "oncompletetarget", gameObject,
							    "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                        ));
                break;

			case state.SP_fadeout:
				iTween.ValueTo(gameObject, iTween.Hash(     "from", 1, "to", 0, "onupdate", "fadeto2",
                                                            "time", time, "delay", delay,
                                                            "easetype", ease, "looptype", loop,
                                                            "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                            "oncomplete", "Complete", "oncompletetarget", gameObject,
							    "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                        ));
                break;
                
            case state.scaleIn:
                iTween.ScaleFrom(gameObject, iTween.Hash(	"scale", scaleValue,
                                                            "time", time, "delay", delay,
                                                            "easetype", ease, "looptype", loop,
                                                            "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                            "oncomplete", "Complete", "oncompletetarget", gameObject,
							    "orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                        ));
                break;

            case state.moveto:
                if(islocal)
                    iTween.MoveTo(gameObject, iTween.Hash(	    "position", moveloc.localPosition,
                                                                "time", time, "delay", delay,
                                                                "easetype", ease, "looptype", loop,
                                                                "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                "oncomplete", "Complete", "oncompletetarget", gameObject, 
								"orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                            ));
                else
                    iTween.MoveTo(gameObject, iTween.Hash(      "position", moveloc.position,
                                                                "time", time, "delay", delay,
                                                                "easetype", ease, "looptype", loop,
                                                                "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                                                "oncomplete", "Complete", "oncompletetarget", gameObject,
								"orienttopath", orienttopathst, "lookahead", lookaheadValue
                                                            ));

                break;


            case state.rotationto:
                iTween.RotateTo(gameObject, iTween.Hash(    "rotation", rotvalue,
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

    void fadeto2(float newvalue)
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            SpriteRenderer sp = GetComponent<SpriteRenderer>();
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, newvalue);
        }
    }

    void Complete()
    {
        unityevent.Invoke();
    }

}

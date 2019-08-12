using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class iPathWalk : MonoBehaviour {

    public bool autostart = false;
    public string whitchPath = "New Path 1";

    [Header("Itween Setting")]
    public float time;
    public float delay = 0;
    public iTween.EaseType ease;
    public iTween.LoopType loop;
    public bool islocal = false;
    public bool ignoreTimeScalest = false;
    public bool orienttopathst = false;
    public float lookaheadValue = .05f;

    [Header("CompleteEvent")]
    public UnityEvent unityevent;

    void Start () {

        if (autostart)
            itweenGo();


    }


    public void itweenGo()
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(whitchPath), 
                                  "time", time, "delay", delay,
                                  "easetype", ease, "looptype", loop,
                                  "islocal", islocal, "ignoretimescale", ignoreTimeScalest,
                                  "oncomplete", "Complete", "oncompletetarget", gameObject,
                                  "orienttopath", orienttopathst, "lookahead", lookaheadValue
                     ));


       
    }

    void Complete()
    {
        unityevent.Invoke();
    }


}

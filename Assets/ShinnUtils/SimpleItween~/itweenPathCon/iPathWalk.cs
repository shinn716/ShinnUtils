using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class iPathWalk : MonoBehaviour {

    [SerializeField] bool autostart = false;
    [SerializeField] string whitchPath = "New Path 1";

    [Header("Itween Setting")]
    [SerializeField] float time = 1;
    [SerializeField] float delay = 0;
    [SerializeField] iTween.EaseType ease = iTween.EaseType.easeInQuad;
    [SerializeField] iTween.LoopType loop = iTween.LoopType.none;
    [SerializeField] bool islocal = false;
    [SerializeField] bool ignoreTimeScalest = false;
    [SerializeField] bool orienttopathst = false;
    [SerializeField] float lookaheadValue = .05f;

    [Header("CompleteEvent")]
    [SerializeField] UnityEvent unityevent = null;

    void Start ()
    {
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

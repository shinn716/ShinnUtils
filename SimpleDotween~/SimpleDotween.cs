//
//  SimpleDotween.cs
//  SimpleDotween
//
//  Created by Shinn on 2022/1/25.
//  Copyright © 2021 Shinn. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;
using System;

public class SimpleDotween : MonoBehaviour
{
    [SerializeField]
    private enum State
    {
        shakePosition,
        punchPosition,

        scaleTo,
        scaleFrom,
        moveToTarget,
        moveToTarget_LOCAL,
        moveToPos,
        moveToPos_LOCAL,

        rotationTo,
        rotationTo_LOCAL,

        fadeTo,

        colorTo,
        colorFrom,

        //rotationToAndMoveTo,
    }


    [SerializeField] GameObject target = null;
    [SerializeField] State mystate = State.scaleTo;
    [SerializeField] float time = 0;
    [SerializeField] float delay = 0;
    [SerializeField] Ease ease = Ease.OutExpo;
    [SerializeField] bool ignoreTimeScale = false;
    [SerializeField] bool autorun = true;

    [Header("Looptimes: -1 -> loop, 0 -> noloop, 1~xx -> looptimes")]
    [SerializeField] int looptimes = 0;
    [SerializeField] LoopType looptype = LoopType.Incremental;

    [SerializeField] Transform moveloc = null;
    [SerializeField] Vector3 punchValue = Vector3.one;
    [SerializeField] Vector3 shakeValue = Vector3.one;
    [SerializeField] Vector3 scaleValue = Vector3.zero;
    [SerializeField] Vector3 posValue = Vector3.zero;
    [SerializeField] Vector2 pos2Value = Vector3.zero;
    [SerializeField] Vector3 rotvalue = Vector3.zero;

    [SerializeField] Color endColor = Color.white;
    [Range(0, 1)] [SerializeField] float fadeStart = 0;
    [Range(0, 1)] [SerializeField] float fadeEnd = 1;

    [SerializeField] bool startComplete = false;
    [SerializeField] UnityEvent onCompleteVoidEvents = new UnityEvent();

    //[SerializeField] UnityEvent<int> onCompleteIntEvents = new UnityEvent<int>();
    //[SerializeField] VoidEvent voidevents = null;
    //[SerializeField] BoolEvent boolevents = null;
    //[SerializeField] IntEvent intevents = null;
    //[SerializeField] FloatEvent floatevents = null;
    ////[SerializeField] FloatArrayEvent floatarratevents = null;
    //[SerializeField] Vector3Event vector3events = null;
    //[SerializeField] ColorEvent colorevents = null;

    private Vector3 orgscale = Vector3.zero;
    private Color orgColor = Color.white;
    private Tween dotween = null;

    public void Run()
    {
        StartDoItween(mystate);
    }

    public void Pause()
    {
        DOTween.Pause(target.gameObject);
    }

    public void Resume()
    {
        if (dotween.IsPlaying())
            dotween.Play();
    }

    public void Stop()
    {
        DOTween.Kill(target.gameObject);
    }

    private void Start()
    {
        if (target == null)
            target = gameObject;
    }

    public void OnEnable()
    {
        if (target == null)
            target = gameObject;
        if (autorun)
            StartDoItween(mystate);
    }

    private void StartDoItween(State state)
    {
        switch (state)
        {
            case State.punchPosition:
                dotween = target.transform.DOPunchPosition(punchValue, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                break;
            case State.shakePosition:
                dotween = target.transform.DOPunchPosition(shakeValue, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                break;

            case State.scaleFrom:
                orgscale = target.transform.localScale;
                target.transform.localScale = scaleValue;
                dotween = target.transform.DOScale(orgscale, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                break;
            case State.scaleTo:
                dotween = target.transform.DOScale(scaleValue, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                break;

            case State.moveToTarget:
                dotween = target.transform.DOMove(moveloc.position, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                break;
            case State.moveToTarget_LOCAL:
                dotween = target.transform.DOLocalMove(moveloc.localPosition, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                break;
            case State.moveToPos:
                if (target.GetComponent<RectTransform>())
                {
                    Vector2 rect = target.GetComponent<RectTransform>().anchoredPosition;
                    dotween = DOTween.To(() => rect, x => rect = x, pos2Value, time).OnUpdate(() =>
                    {
                        target.GetComponent<RectTransform>().anchoredPosition = rect;
                    }).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                }
                else
                    dotween = target.transform.DOLocalMove(posValue, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                break;
            case State.moveToPos_LOCAL:
                dotween = target.transform.DOLocalMove(posValue, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                break;

            case State.rotationTo:
                dotween = target.transform.DORotate(rotvalue, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                break;
            case State.rotationTo_LOCAL:
                dotween = target.transform.DOLocalRotate(rotvalue, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                break;

            case State.colorTo:
                if(target.GetComponent<Image>())
                    dotween = target.transform.GetComponent<Image>().DOColor(endColor, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                else if (target.GetComponent<RawImage>())
                    dotween = target.transform.GetComponent<RawImage>().DOColor(endColor, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                else if (target.GetComponent<SpriteRenderer>())
                    dotween = target.transform.GetComponent<SpriteRenderer>().DOColor(endColor, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                else
                    dotween = target.transform.GetComponent<Renderer>().material.DOColor(endColor, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                break;

            case State.colorFrom:
                if (target.GetComponent<Image>())
                {
                    var renderer = target.transform.GetComponent<Image>();
                    orgColor = renderer.color;
                    renderer.color = endColor;
                    dotween = renderer.DOColor(orgColor, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                }
                else if (target.GetComponent<RawImage>())
                {
                    var renderer = target.transform.GetComponent<RawImage>();
                    orgColor = renderer.color;
                    renderer.color = endColor;
                    dotween = renderer.DOColor(orgColor, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                }
                else if (target.GetComponent<SpriteRenderer>())
                {
                    var renderer = target.transform.GetComponent<SpriteRenderer>();
                    orgColor = renderer.color;
                    renderer.color = endColor;
                    dotween = renderer.DOColor(orgColor, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                }
                else
                {
                    var renderer = target.transform.GetComponent<Renderer>();
                    orgColor = renderer.material.color;
                    renderer.material.color = endColor;
                    dotween = renderer.material.DOColor(orgColor, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                }
                break;

            case State.fadeTo:
                if (target.GetComponent<Image>())
                {
                    var renderer = target.transform.GetComponent<Image>();
                    Color orgcolor = renderer.color;
                    orgcolor.a = fadeStart;
                    renderer.color = orgcolor;
                    dotween = renderer.DOFade(fadeEnd, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                }
                else if (target.GetComponent<RawImage>())
                {
                    var renderer = target.transform.GetComponent<RawImage>();
                    Color orgcolor = renderer.color;
                    orgcolor.a = fadeStart;
                    renderer.color = orgcolor;
                    dotween = renderer.DOFade(fadeEnd, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                }
                else if (target.GetComponent<SpriteRenderer>())
                {
                    var renderer = target.transform.GetComponent<SpriteRenderer>();
                    Color orgcolor = renderer.color;
                    orgcolor.a = fadeStart;
                    renderer.color = orgcolor;
                    dotween = renderer.DOFade(fadeEnd, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                }
                else
                {
                    var renderer = target.transform.GetComponent<Renderer>();
                    Color orgcolor = renderer.material.color;
                    orgcolor.a = fadeStart;
                    renderer.material.color = orgcolor;
                    dotween = renderer.material.DOFade(fadeEnd, time).SetEase(ease).SetDelay(delay).SetLoops(looptimes, looptype).SetUpdate(ignoreTimeScale).OnComplete(Complete);
                }
                break;
        }

        void Complete()
        {
            if(startComplete)
                onCompleteVoidEvents?.Invoke();
        }
    }
}

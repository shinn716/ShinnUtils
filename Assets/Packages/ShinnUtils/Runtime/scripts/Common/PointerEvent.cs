// Sample: 
//    {
//    btnRecord.OnPointerEvent += delegate (GameObject go)
//    {
//        // OnClick TODO:
//        btnRecord.onClick = () =>
//        {
//            print("onClick");
//        };
//        // Long press TODO: 
//        btnRecord.onLongpress = () =>
//        {
//            print("onLongpress");
//        };
//        // Long press TODO: 
//        btnRecord.onTouchUp = () =>
//        {
//            print("onTouchUp");
//        };
//    };

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

namespace Shinn
{
    public class PointerEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        public delegate void UIPointerEvent(GameObject go);
        public event UIPointerEvent OnPointerEvent;

        [Header("Custom events (Suggest use actions)")]
        public UnityEvent onLongPressEvents;
        public UnityEvent onTouchUpEvents;
        public UnityEvent onClickEvents;

        public Action onLongpress { get; set; } = null;
        public Action onTouchUp { get; set; } = null;
        public Action onClick { get; set; } = null;

        private bool m_touch = false;
        private bool m_onClick = false;
        private bool m_Down = false;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (!m_onClick)
                onClick?.Invoke();
        }
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (OnPointerEvent != null)
                OnPointerEvent(this.gameObject);
            StartCoroutine(LongPressCo());
        }
        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            m_touch = false;
            StartCoroutine(DelayAndSetOnClickCo());
        }


        private IEnumerator LongPressCo(float _delaytime = .5f)
        {
            m_touch = true;

            // Wait for false touch
            yield return new WaitForSeconds(_delaytime);
            if (!m_touch)
                yield break;

            // Start recording
            if (!m_Down)
            {
                m_Down = true;
                onLongpress?.Invoke();
            }

            // Animate the countdown
            var startTime = Time.time;
            while (m_touch)
            {
                m_onClick = true;
                var ratio = (Time.time - startTime) / 10;
                m_touch = ratio <= 1f;
                yield return null;
            }
            onTouchUp?.Invoke();
            m_Down = false;
        }
        private IEnumerator DelayAndSetOnClickCo()
        {
            yield return null;
            m_onClick = false;
        }
    }
}
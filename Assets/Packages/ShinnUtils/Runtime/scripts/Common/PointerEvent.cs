// Sample: 
//    {
//    btnRecord.OnPointerEvent += delegate (GameObject go)
//    {
//        // OnClick TODO:
//        btnRecord.OnClick = () =>
//        {
//            print("onClick");
//        };
//        // Long press TODO: 
//        btnRecord.OnLongpress = () =>
//        {
//            print("onLongpress");
//        };
//        // Long press TODO: 
//        btnRecord.OnTouchUp = () =>
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
        public event UIPointerEvent OnPointerEventListener;

        [Header("Custom events (Suggest use actions)")]
        [SerializeField] private UnityEvent onLongPressEvents;
        [SerializeField] private UnityEvent onLongPressLoopEvents;
        [SerializeField] private UnityEvent onTouchUpEvents;
        [SerializeField] private UnityEvent onClickEvents;

        public Action OnLongpress { get; set; } = null;
        public Action OnLongPressLoop { get; set; } = null;
        public Action OnTouchUp { get; set; } = null;
        public Action OnClick { get; set; } = null;

        private bool m_touch = false;
        private bool m_onClick = false;
        private bool m_Down = false;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (!m_onClick)
            {
                OnClick?.Invoke();
                onClickEvents?.Invoke();
            }
        }
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (OnPointerEventListener != null)
                OnPointerEventListener(this.gameObject);
            StartCoroutine(LongPressCo());
        }
        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            m_touch = false;
            StartCoroutine(DelayAndSetOnClickCo());
        }


        private IEnumerator LongPressCo(float _delaytime = .15f)
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
                OnLongpress?.Invoke();
                onLongPressEvents?.Invoke();
            }

            // Animate the countdown
            var startTime = Time.time;
            while (m_touch)
            {
                m_onClick = true;
                //var ratio = (Time.time - startTime) / 10;
                //m_touch = ratio <= 1f;

                OnLongPressLoop?.Invoke();
                onLongPressLoopEvents?.Invoke();
                yield return null;
            }
            OnTouchUp?.Invoke();
            onTouchUpEvents?.Invoke();
            m_Down = false;
        }
        private IEnumerator DelayAndSetOnClickCo()
        {
            yield return null;
            m_onClick = false;
        }
    }
}
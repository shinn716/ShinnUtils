using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Shinn
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CustomCanvasGroup : MonoBehaviour
    {
        public CanvasGroup canvasGroup { get; private set; } = null;

        public Action OnEnableAction { get; set; } = null;
        public Action OnDisableAction { get; set; } = null;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void EnableCanvas(bool _enable)
        {
            canvasGroup.alpha = _enable ? 1 : 0;
            canvasGroup.blocksRaycasts = _enable;
            canvasGroup.interactable = _enable;

            if (_enable)
                OnEnableAction?.Invoke();
            else
                OnDisableAction?.Invoke();
        }
    }
}
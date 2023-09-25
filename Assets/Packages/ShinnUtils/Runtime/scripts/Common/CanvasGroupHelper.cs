using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

namespace Shinn
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupHelper : MonoBehaviour
    {
        [SerializeField] bool scaleEFX = false;
        [SerializeField] float during = .5f;

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

        public void Show()
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            if (scaleEFX)
            {
                transform.localScale = Vector3.zero;
                transform.DOScale(new Vector3(1, 1, 1), during).SetEase(Ease.OutExpo);
            }
        }

        public void Hide()
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            if (scaleEFX)
            {
                transform.localScale = Vector3.one;
                transform.DOScale(new Vector3(0, 0, 0), during / 2).SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    canvasGroup.alpha = 0;
                });
            }
        }
    }
}
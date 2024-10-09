using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupHelper : MonoBehaviour
{
    [SerializeField] private bool scaleEFX = false;
    [SerializeField] private float during = 0.5f;

    public CanvasGroup canvasGroup { get; private set; }
    public Action OnEnableAction { get; set; }
    public Action OnDisableAction { get; set; }

    private Vector3 originSize;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        originSize = transform.localScale;
    }

    public void SetCanvasGroupState(bool enable)
    {
        if (canvasGroup.alpha == (enable ? 1 : 0)) return;  // 防止重複操作
        canvasGroup.alpha = enable ? 1 : 0;
        canvasGroup.blocksRaycasts = enable;
        canvasGroup.interactable = enable;

        if (enable)
        {
            OnEnableAction?.Invoke();
        }
        else
        {
            OnDisableAction?.Invoke();
        }
    }

    public void Show(bool show)
    {
        if (show)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public void Show()
    {
        SetCanvasGroupState(true);

        if (scaleEFX)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(originSize, during).SetEase(Ease.OutExpo);
        }
    }

    public void Hide()
    {
        if (scaleEFX)
        {
            transform.DOScale(Vector3.zero, during / 2).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                SetCanvasGroupState(false);
            });
        }
        else
        {
            SetCanvasGroupState(false);
        }
    }
}

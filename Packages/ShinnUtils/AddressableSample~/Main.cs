using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public static Main instance;

    public event Action<TMP_FontAsset> FinishLoadText;
    public TMP_FontAsset FinalText { get; private set; }
    public event Action<Texture2D> FinishLoadCursor;
    public Texture2D CursorHand { get; private set; }

    [SerializeField] CanvasGroup canvasGroupTransition;
    [SerializeField] CanvasGroup progressBarGroup;
    [SerializeField] Slider progressBar;

    float value = 0;
    bool loadingInFirstTime = false;

    private async void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        // await LoadScene("home");
        await LoadFontAsset();
        await LoadCursor();
    }

    private async Task LoadCursor()
    {
        AsyncOperationHandle<Texture2D> handle = Addressables.LoadAssetAsync<Texture2D>("group/cursorhand");
        await handle.Task;

        if (handle.Status.Equals(AsyncOperationStatus.Succeeded))
        {
            FinishLoadCursor?.Invoke(handle.Result);
            CursorHand = handle.Result;
        }
        Addressables.Release(handle);
    }

    private async Task LoadFontAsset()
    {
        AsyncOperationHandle<TMP_FontAsset> textHandle = Addressables.LoadAssetAsync<TMP_FontAsset>("group/notosanscjktc_regularsdf");
        await textHandle.Task;

        if (textHandle.Status.Equals(AsyncOperationStatus.Succeeded))
        {
            FinishLoadText?.Invoke(textHandle.Result);
            FinalText = textHandle.Result;
        }
        Addressables.Release(textHandle);
    }

    public async Task LoadScene(string name, Action finish = null)
    {
        Transition(finish);
        AsyncOperationHandle<SceneInstance> sceneHandle = Addressables.LoadSceneAsync(name);

        if (!loadingInFirstTime)
        {
            progressBarGroup.alpha = 1f;
            while (sceneHandle.Status != AsyncOperationStatus.Succeeded)
            {
                // print("Loading progress: " + name + " " + (sceneHandle.PercentComplete * 100) + "%");
                progressBar.value = sceneHandle.PercentComplete;
                await UniTask.Yield();
            }
        }

        await sceneHandle.Task;

        if (sceneHandle.Status.Equals(AsyncOperationStatus.Succeeded))
        {
            if (!loadingInFirstTime)
            {
                progressBarGroup.alpha = 0f;
            }
        }

        loadingInFirstTime = true;
        // Addressables.Release(sceneHandle);
    }

    private void Transition(Action finish = null)
    {
        value = 1;
        canvasGroupTransition.alpha = value;
        canvasGroupTransition.blocksRaycasts = true;
        DOTween.To(() => value, x => value = x, 0, .5f).SetEase(Ease.OutSine).SetDelay(.5f).OnUpdate(() =>
        {
            canvasGroupTransition.alpha = value;
        }).OnComplete(async () =>
        {
            await UniTask.Delay(TimeSpan.FromSeconds(.5f));
            canvasGroupTransition.blocksRaycasts = false;
            finish?.Invoke();
        });
    }
}

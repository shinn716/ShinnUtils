using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneForTimelineEvents : Singleton<SceneForTimelineEvents>
{
    public int[] LoadLevel;
    private AsyncOperation[] asyncOperation;

    private void Awake()
    {
        asyncOperation = new AsyncOperation[LoadLevel.Length];
        for (int i=0; i<LoadLevel.Length; i++)
            StartCoroutine(LoadScene(i, LoadLevel[i]));
    }

    private IEnumerator LoadScene(int index, int level)
    {
        yield return null;
        asyncOperation[index] = SceneManager.LoadSceneAsync(level);
        asyncOperation[index].allowSceneActivation = false;
    }

    public void NextScene(int index)
    {
        asyncOperation[index].allowSceneActivation = true;
    }
}

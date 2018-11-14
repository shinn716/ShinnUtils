using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneForTimelineEvents : MonoBehaviour {

    public int LoadLevel = 0;
    AsyncOperation asyncOperation;

    private void Start()
    {
        StartCoroutine(LoadScene(LoadLevel));
    }

    public void nextScene()
    {
        asyncOperation.allowSceneActivation = true;
    }


    IEnumerator LoadScene(int level)
    {
        yield return null;
        asyncOperation = SceneManager.LoadSceneAsync(level);
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncOperationTools : MonoBehaviour {

    [ReadOnly, SerializeField]
    int CurrentLevel = 0;

    public KeyCode nextKey = KeyCode.N;
    public bool DontDestroy = false;
    public bool ShowProcess = false;

    private bool processGUI = false;
    private bool finish = false;

    AsyncOperation asyncOperation;
    

    private void Start()
    {
        if (DontDestroy)
            DontDestroyOnLoad(gameObject);
        Next();
    }

    public void Next()
    {
        finish = false;
        if (CurrentLevel > SceneManager.sceneCountInBuildSettings-2)
        {
            CurrentLevel = 0;
            StartCoroutine(LoadScene(CurrentLevel));
        }
        else
        {
            CurrentLevel++;
            StartCoroutine(LoadScene(CurrentLevel));
        }
    }

    private void OnGUI()
    {
        if (ShowProcess && processGUI)
        {
            float processValue = asyncOperation.progress*100;
            if (processValue >= 90f)            
                GUI.Label(new Rect(10, 10, 200, 50), "Finish" );            
            else
                GUI.Label(new Rect(10, 10, 200, 50), "Loading progress " + processValue.ToString() + "%");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(nextKey) && finish)
        {
            asyncOperation.allowSceneActivation = true;
            Next();
        }
    }


    IEnumerator LoadScene(int level)
    {
        yield return null;
        processGUI = true;        
        asyncOperation = SceneManager.LoadSceneAsync(level);
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);

        while (!asyncOperation.isDone)
        {
            print("Loading progress: " + (asyncOperation.progress * 100) + "%");
            if (asyncOperation.progress >= 0.9f)
            {
                print("Press the " + nextKey.ToString() + " bar to continue");
                finish = true;
            }
            yield return null;
        }
    }

}

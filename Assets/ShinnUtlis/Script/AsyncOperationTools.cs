using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncOperationTools : MonoBehaviour {

    public int currentLevel=0;
    public KeyCode nextKey = KeyCode.N;

    private void Start()
    {
        Next();
    }

    public void Next()
    {
        if (currentLevel >= Application.levelCount - 1)
        {
            currentLevel = 0;
            StartCoroutine(LoadScene(currentLevel));
        }
        else
        {
            currentLevel++;
            StartCoroutine(LoadScene(currentLevel));
        }
    }


    IEnumerator LoadScene(int level)
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(level);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            print("Loading progress: " + (asyncOperation.progress * 100) + "%");

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                print("Press the " + nextKey.ToString() + " bar to continue");
                //Wait to you press the space key to activate the Scene
                if (Input.GetKeyDown(nextKey))
                {
                    //Activate the Scene
                    asyncOperation.allowSceneActivation = true;
                    Next();
                }

            }

            yield return null;
        }
    }

}

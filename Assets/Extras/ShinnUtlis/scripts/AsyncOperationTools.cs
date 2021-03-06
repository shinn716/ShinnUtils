using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shinn
{

    public class AsyncOperationTools : MonoBehaviour
    {
        private static AsyncOperationTools s_Instance;

        [ReadOnly, SerializeField]
        private int CurrentLevel;

        public KeyCode nextKey = KeyCode.N;
        public bool DontDestroy;
        public bool ShowProcess;

        private bool processGUI;
        private bool finish;

        private AsyncOperation asyncOperation;

        protected Rect viewWindow;

        private void Start()
        {
            viewWindow = new Rect(10, 10, 150, 20);

            if (DontDestroy)
            {
                if (s_Instance == null)
                {
                    s_Instance = this;
                    DontDestroyOnLoad(gameObject);
                }
                else if (this != s_Instance)
                {
                    Destroy(gameObject);
                }
            }

            Next();
        }

        public void Next()
        {
            finish = false;
            if (CurrentLevel > SceneManager.sceneCountInBuildSettings - 2)
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
                using (new GUILayout.HorizontalScope())
                {
                    viewWindow = GUILayout.Window(GetInstanceID(), viewWindow, Window, "Progress");
                }
            }
        }

        protected void Window(int id)
        {
            using (new GUILayout.VerticalScope())
            {
                float processValue = asyncOperation.progress * 100;
                if (processValue >= 90f)
                    GUILayout.Label("Finish");
                else
                    GUILayout.Label("Loading progress " + processValue.ToString() + "%");
            }
            GUI.DragWindow();
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

}

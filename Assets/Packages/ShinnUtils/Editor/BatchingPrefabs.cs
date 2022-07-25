using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Text;

namespace Shinn.Tools
{
    // Batch prefabs
    public class BatchingPrefabs : EditorWindow
    {
        private GameObject[] selectedGameObjects;
        private string stringOutputPath = "Assets/Resources/";
        private string stringFeedback = string.Empty;

        [MenuItem("DevTools/Batching Prefabs")]
        private static void OpenMenu()
        {
            // Open menu
            GetWindow<BatchingPrefabs>("Batching Prefabs");
        }

        //[InitializeOnLoadMethod]
        private void Awake()
        {
            OnSelectionChange();
        }

        private void OnGUI()
        {
            // text label
            GUILayout.Label($"Now select [{selectedGameObjects.Length}] objects.");

            // browser 
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Export path: ");
            stringOutputPath = EditorGUILayout.TextField(stringOutputPath);

            if (GUILayout.Button("browse"))
                EditorApplication.delayCall += OpenPrefabFolder;
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            // Create prefab
            if (GUILayout.Button("Create", GUILayout.MinHeight(20)))
            {
                if (selectedGameObjects.Length <= 0)
                {
                    // open notification  
                    this.ShowNotification(new GUIContent("Nothing select."));
                    return;
                }

                if (!Directory.Exists(stringOutputPath))
                    Directory.CreateDirectory(stringOutputPath);

                foreach (GameObject m in selectedGameObjects)
                    CreatePrefab(m, m.name);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Output");
                sb.AppendLine($"Path: {stringOutputPath}");
                sb.AppendLine($"========================");

                for (int i=0; i< selectedGameObjects.Length; i++)
                    sb.AppendLine($"{i} [{selectedGameObjects[i].name}]");

                //foreach (GameObject m in selectedGameObjects)
                //    sb.AppendLine($"[{m.name}]");
                stringFeedback = sb.ToString();

                AssetDatabase.Refresh();
            }

            GUILayout.Label(stringFeedback);
        }

        private void OpenPrefabFolder()
        {
            string path = EditorUtility.OpenFolderPanel("Export path", "", "");
            if (!path.Contains(Application.dataPath))
            {
                Debug.LogError("URL need in assets/");
                return;
            }

            if (path.Length != 0)
            {
                int firstindex = path.IndexOf("Assets");
                stringOutputPath = path.Substring(firstindex) + "/";
                EditorUtility.FocusProjectWindow();
            }
        }

        private void CreatePrefab(GameObject go, string name)
        {
            PrefabUtility.SaveAsPrefabAsset(go, stringOutputPath + name + ".prefab");
        }

        private void OnInspectorUpdate()
        {
            // refresh window
            this.Repaint();
        }

        private void OnSelectionChange()
        {
            // select gameobject
            selectedGameObjects = Selection.gameObjects;
        }
    }
}
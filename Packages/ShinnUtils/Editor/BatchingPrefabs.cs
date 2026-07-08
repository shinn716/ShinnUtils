using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Text;

namespace Shinn
{
    // Batch prefabs
    public class BatchingPrefabs : EditorWindow
    {
        private GameObject[] selectedGameObjects;
        private string stringOutputPath = "Assets/Resources/";
        private string stringFeedback = string.Empty;

        [MenuItem("Tools/ShinnDev/Batching Prefabs")]
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
            EditorGUILayout.LabelField("Batching Prefabs", EditorStyles.boldLabel);

            int count = selectedGameObjects != null ? selectedGameObjects.Length : 0;
            EditorGUILayout.HelpBox(
                count > 0
                    ? $"{count} selected object(s) will be saved as prefabs."
                    : "Select GameObjects in the Hierarchy to export as prefabs.",
                count > 0 ? MessageType.Info : MessageType.Warning);

            EditorGUILayout.Space();

            // Export path row
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Export path");
            stringOutputPath = EditorGUILayout.TextField(stringOutputPath);
            if (GUILayout.Button("Browse", GUILayout.Width(70)))
                EditorApplication.delayCall += OpenPrefabFolder;
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            using (new EditorGUI.DisabledScope(count == 0))
            {
                if (GUILayout.Button("Create Prefabs", GUILayout.Height(24)))
                    CreateSelectedPrefabs();
            }

            if (!string.IsNullOrEmpty(stringFeedback))
                EditorGUILayout.HelpBox(stringFeedback, MessageType.Info);
        }

        private void CreateSelectedPrefabs()
        {
            if (selectedGameObjects == null || selectedGameObjects.Length <= 0)
            {
                this.ShowNotification(new GUIContent("Nothing selected."));
                return;
            }

            if (!Directory.Exists(stringOutputPath))
                Directory.CreateDirectory(stringOutputPath);

            foreach (GameObject m in selectedGameObjects)
                CreatePrefab(m, m.name);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Output");
            sb.AppendLine($"Path: {stringOutputPath}");
            sb.AppendLine("========================");
            for (int i = 0; i < selectedGameObjects.Length; i++)
                sb.AppendLine($"{i} [{selectedGameObjects[i].name}]");
            stringFeedback = sb.ToString();

            AssetDatabase.Refresh();
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

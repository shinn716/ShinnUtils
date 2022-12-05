using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Text;
using System;
using System.Threading.Tasks;

namespace Shinn
{
    public class CopyFileTools : EditorWindow
    {
        [SerializeField, ReadOnly] private List<string> filelist = new List<string>();

        private string sourcesPath = "C:\\";
        private string targetPath = "D:\\";
        private Editor editor;
        private StringBuilder sb = new StringBuilder();
        private Vector2 scrollFilelist = Vector2.zero;
        private Vector2 scrollLoglist = Vector2.zero;
        private bool showList = true;

        [MenuItem("Tools/ShinnDev/Copy File Menu")]
        private static void OpenMenu()
        {
            // Open menu
            GetWindow<CopyFileTools>("Copy File Menu");
        }

        private void OnGUI()
        {
            GUILayout.Label("Copy all source to destination: ");

            GUILayout.Label("Sources: ");
            sourcesPath = GUILayout.TextField(sourcesPath);
            if (GUILayout.Button("browse"))
                EditorApplication.delayCall += OpenBrowserSource;

            GUILayout.Label("Destination: ");
            targetPath = GUILayout.TextField(targetPath);
            if (GUILayout.Button("browse"))
                EditorApplication.delayCall += OpenBrowserTarget;

            showList = GUILayout.Toggle(showList, "Show list in GUI.");

            GUILayout.Space(10f);
            if (GUILayout.Button("Get all files"))
            {
                GetAllFilesInSources();
            }
            if (GUILayout.Button("Clear"))
            {
                sb.Clear();
                filelist.Clear();
            }
            if (GUILayout.Button("*Copy all files*"))
            {
                DuplicateAllFiles();
            }

            EditorGUILayout.BeginVertical();

            scrollFilelist = EditorGUILayout.BeginScrollView(scrollFilelist, GUILayout.Width(position.width), GUILayout.Height(position.height / 6));
            editor = Editor.CreateEditor(this);
            editor.OnInspectorGUI();
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();


            GUILayout.Space(10f);
            EditorGUILayout.BeginVertical();
            scrollLoglist = EditorGUILayout.BeginScrollView(scrollLoglist, GUILayout.Width(position.width), GUILayout.Height(position.height / 2));


            GUILayout.Label("[log]");
            if (sb.Length > 0)
                GUILayout.Label(sb.ToString());

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }


        private void OpenBrowserSource()
        {
            string path = EditorUtility.OpenFolderPanel("Select path", "", "");
            sourcesPath = path;
            EditorUtility.FocusProjectWindow();
        }
        private void OpenBrowserTarget()
        {
            string path = EditorUtility.OpenFolderPanel("Target path", "", "");
            targetPath = path;
            EditorUtility.FocusProjectWindow();
        }
        private void GetAllFilesInSources()
        {
            filelist.Clear();
            sb.Clear();

            try
            {
                sb.AppendLine("File count: " + Directory.GetFiles(sourcesPath, "*.*", SearchOption.AllDirectories).Length);
                if (!showList)
                    return;

                foreach (string newPath in Directory.GetFiles(sourcesPath, "*.*", SearchOption.AllDirectories))
                    filelist.Add(newPath);
            }
            catch (Exception e)
            {
                sb.AppendLine("[error]" + e.ToString());
            }
        }
        private void DuplicateAllFiles()
        {
            sb.AppendLine("Start Process... please wait");
            ProcessDuplicateAllFiles();
        }
        private async void ProcessDuplicateAllFiles()
        {
            var task01 = Task.Run(() =>
            {
                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(sourcesPath, "*",
                   SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(sourcesPath, targetPath));

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(sourcesPath, "*.*",
                   SearchOption.AllDirectories))
                {
                    sb.AppendLine(newPath);
                    File.Copy(newPath, newPath.Replace(sourcesPath, targetPath), true);
                }
            }
            );
            await Task.Yield();
        }


        [CustomEditor(typeof(CopyFileTools), true)]
        public class ListEditorDrawer : Editor
        {
            public override void OnInspectorGUI()
            {
                var list = serializedObject.FindProperty("filelist");
                EditorGUILayout.PropertyField(list, new GUIContent("file list"), true);
            }
        }
    }


    #region ReadOnlyAttribute 
    /// <summary>
    /// Read Only attribute.
    /// Attribute is use only to mark ReadOnly properties.
    /// </summary>
    public class ReadOnlyAttribute : PropertyAttribute { }

    /// <summary>
    /// This class contain custom drawer for ReadOnly attribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        /// <summary>
        /// Unity method for drawing GUI in Editor
        /// </summary>
        /// <param name="position">Position.</param>
        /// <param name="property">Property.</param>
        /// <param name="label">Label.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Saving previous GUI enabled value
            var previousGUIState = GUI.enabled;
            // Disabling edit for property
            GUI.enabled = false;
            // Drawing Property
            EditorGUI.PropertyField(position, property, label);
            // Setting old GUI enabled value
            GUI.enabled = previousGUIState;
        }
    }
    #endregion
}
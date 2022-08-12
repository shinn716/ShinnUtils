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
        private Dictionary<string, FileInfo> fileinfolist = new Dictionary<string, FileInfo>();

        [MenuItem("ShiDevTools/Copy File Menu")]
        private static void OpenMenu()
        {
            // Open menu
            GetWindow<CopyFileTools>("Copy File Menu");
        }

        private void OnGUI()
        {
            GUILayout.Label("Copy all source to destination: ");


            GUILayout.Label("Sources path: ");
            sourcesPath = GUILayout.TextField(sourcesPath);
            if (GUILayout.Button("browse"))
                EditorApplication.delayCall += OpenBrowserSource;


            GUILayout.Label("Export path: ");
            targetPath = GUILayout.TextField(targetPath);
            if (GUILayout.Button("browse"))
                EditorApplication.delayCall += OpenBrowserTarget;


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
            if (GUILayout.Button("Copy to the destination"))
            {
                sb.Clear();
                Dduplicate();
            }

            editor = Editor.CreateEditor(this);
            editor.OnInspectorGUI();

            GUILayout.Label(sb.ToString());
        }


        private void OpenBrowserSource()
        {
            string path = EditorUtility.OpenFolderPanel("Select path", "", "");
            sourcesPath = path;
            EditorUtility.FocusProjectWindow();
        }
        private void OpenBrowserTarget()
        {
            string path = EditorUtility.OpenFolderPanel("Export path", "", "");
            targetPath = path;
            EditorUtility.FocusProjectWindow();
        }

        private void GetAllFilesInSources()
        {
            filelist.Clear();
            fileinfolist.Clear();
            sb.Clear();
            var info = new DirectoryInfo(sourcesPath);
            var fileInfo = info.GetFiles();
            foreach (var file in fileInfo)
            {
                filelist.Add(file.ToString());
                fileinfolist.Add(file.ToString(), file);
                sb.AppendLine(file.Name);
            }
        }
        private void Dduplicate()
        {
            sb.AppendLine("Start Process... please wait");
            Process();
        }
        private async void Process(Action callback = null)
        {
            var task01 = Task.Run(() =>
            {
                sb.AppendLine($"Copy files count: {fileinfolist.Count}");
                foreach (var i in fileinfolist)
                {
                    string[] splitArray = i.Key.Split(char.Parse("\\"));
                    var copyname = splitArray[splitArray.Length - 1];
                    File.Copy(i.Key, Path.Combine(targetPath, copyname), true);
                    sb.AppendLine($"Finish Process: {i.Key}  {(float)i.Value.Length / 1024 / 1024} MB");
                }
            }
            );
            callback?.Invoke();
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
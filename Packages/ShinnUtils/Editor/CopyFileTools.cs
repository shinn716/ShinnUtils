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
        private List<string> filelist = new List<string>();

        private string sourcesPath = "C:\\";
        private string targetPath = "D:\\";
        private StringBuilder sb = new StringBuilder();
        private Vector2 scrollFilelist = Vector2.zero;
        private Vector2 scrollLoglist = Vector2.zero;
        private bool showList = true;
        private bool isCopying = false;

        // IMGUI has no list virtualization; cap visible rows so huge results don't stall the window.
        private const int MaxRows = 500;

        [MenuItem("Tools/ShinnDev/Copy File Menu")]
        private static void OpenMenu()
        {
            // Open menu
            GetWindow<CopyFileTools>("Copy File Menu");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Copy Files", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Recursively copy a folder tree to a destination.", EditorStyles.miniLabel);
            EditorGUILayout.Space();

            // Source
            EditorGUILayout.LabelField("Source", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            sourcesPath = EditorGUILayout.TextField(sourcesPath);
            if (GUILayout.Button("Browse", GUILayout.Width(70)))
                EditorApplication.delayCall += OpenBrowserSource;
            EditorGUILayout.EndHorizontal();

            // Destination
            EditorGUILayout.LabelField("Destination", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            targetPath = EditorGUILayout.TextField(targetPath);
            if (GUILayout.Button("Browse", GUILayout.Width(70)))
                EditorApplication.delayCall += OpenBrowserTarget;
            EditorGUILayout.EndHorizontal();

            showList = EditorGUILayout.Toggle("Show file list", showList);

            bool validSource = !string.IsNullOrEmpty(sourcesPath) && Directory.Exists(sourcesPath);
            if (!validSource)
                EditorGUILayout.HelpBox("Source folder does not exist.", MessageType.Warning);
            if (isCopying)
                EditorGUILayout.HelpBox("Copying… please wait.", MessageType.Info);

            GUILayout.Space(10f);
            using (new EditorGUI.DisabledScope(isCopying))
            {
                EditorGUILayout.BeginHorizontal();
                using (new EditorGUI.DisabledScope(!validSource))
                {
                    if (GUILayout.Button("Get all files"))
                        GetAllFilesInSources();
                }
                if (GUILayout.Button("Clear"))
                {
                    sb.Clear();
                    filelist.Clear();
                }
                EditorGUILayout.EndHorizontal();

                using (new EditorGUI.DisabledScope(!validSource))
                {
                    if (GUILayout.Button("Copy all files", GUILayout.Height(24)))
                    {
                        if (EditorUtility.DisplayDialog("Copy all files",
                            $"Copy everything from:\n{sourcesPath}\n\nto:\n{targetPath}\n\nExisting files with the same name will be overwritten.",
                            "Copy", "Cancel"))
                        {
                            DuplicateAllFiles();
                        }
                    }
                }
            }

            GUILayout.Space(8f);

            // File list viewer — one selectable (copyable) row per path.
            if (showList)
            {
                EditorGUILayout.LabelField($"Files ({filelist.Count})", EditorStyles.boldLabel);
                scrollFilelist = EditorGUILayout.BeginScrollView(scrollFilelist, EditorStyles.helpBox, GUILayout.Height(position.height * 0.35f));
                if (filelist.Count == 0)
                {
                    EditorGUILayout.LabelField("No files. Set a source folder and click 'Get all files'.", EditorStyles.miniLabel);
                }
                else
                {
                    int shown = Mathf.Min(filelist.Count, MaxRows);
                    for (int i = 0; i < shown; i++)
                        EditorGUILayout.SelectableLabel(filelist[i], EditorStyles.miniLabel, GUILayout.Height(EditorGUIUtility.singleLineHeight));
                    if (filelist.Count > shown)
                        EditorGUILayout.LabelField($"… and {filelist.Count - shown} more (list capped at {MaxRows})", EditorStyles.miniLabel);
                }
                EditorGUILayout.EndScrollView();

                GUILayout.Space(8f);
            }

            // Log — selectable/copyable, height computed from content so the scroll view works.
            EditorGUILayout.LabelField("Log", EditorStyles.boldLabel);
            scrollLoglist = EditorGUILayout.BeginScrollView(scrollLoglist, EditorStyles.helpBox, GUILayout.ExpandHeight(true));
            string logText = sb.Length > 0 ? sb.ToString() : "—";
            float logHeight = EditorStyles.wordWrappedMiniLabel.CalcHeight(new GUIContent(logText), position.width - 30f);
            EditorGUILayout.SelectableLabel(logText, EditorStyles.wordWrappedMiniLabel, GUILayout.Height(logHeight));
            EditorGUILayout.EndScrollView();
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
                // Enumerate once and reuse; the previous code walked the tree twice.
                string[] files = Directory.GetFiles(sourcesPath, "*.*", SearchOption.AllDirectories);
                sb.AppendLine("File count: " + files.Length);
                foreach (string newPath in files)
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
            isCopying = true;
            ProcessDuplicateAllFiles();
        }
        private async void ProcessDuplicateAllFiles()
        {
            // Snapshot paths for the background thread; don't touch UI state from it.
            string src = sourcesPath;
            string dst = targetPath;
            var log = new List<string>();
            int copied = 0;

            try
            {
                await Task.Run(() =>
                {
                    //Now Create all of the directories
                    foreach (string dirPath in Directory.GetDirectories(src, "*", SearchOption.AllDirectories))
                        Directory.CreateDirectory(dirPath.Replace(src, dst));

                    //Copy all the files & Replaces any files with the same name
                    foreach (string newPath in Directory.GetFiles(src, "*.*", SearchOption.AllDirectories))
                    {
                        try
                        {
                            File.Copy(newPath, newPath.Replace(src, dst), true);
                            copied++;
                        }
                        catch (Exception e)
                        {
                            log.Add("[error] " + newPath + " : " + e.Message);
                        }
                    }
                });
            }
            catch (Exception e)
            {
                log.Add("[error] " + e.Message);
            }

            // await resumes on Unity's main thread, so touching sb / Repaint here is safe.
            foreach (string line in log)
                sb.AppendLine(line);
            sb.AppendLine($"Done. Copied {copied} file(s).");
            isCopying = false;
            Repaint();
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

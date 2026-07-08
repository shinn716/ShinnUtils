using UnityEngine;
using UnityEditor;
using System.Text;
using System.Collections.Generic;

namespace Shinn
{
    public class FindMissingScriptsRecursively : EditorWindow
    {
        [SerializeField, ReadOnly]
        private List<GameObject> missingGameObjects = new List<GameObject>();

        private Editor editor;
        private int go_count = 0;
        private int components_count = 0;
        private int missing_count = 0;
        private StringBuilder sb = new StringBuilder();
        private GameObject[] selectedGameObjects;

        [MenuItem("Tools/ShinnDev/Find Missing Scripts")]
        public static void ShowWindow()
        {
            GetWindow<FindMissingScriptsRecursively>("Find Missing Scripts");
        }


        private void Awake()
        {
            OnSelectionChange();
        }

        private void OnEnable()
        {
            // Create the embedded inspector once; recreating it every OnGUI leaks Editor objects.
            editor = Editor.CreateEditor(this);
        }

        private void OnDisable()
        {
            if (editor != null)
                DestroyImmediate(editor);
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Find Missing Scripts", EditorStyles.boldLabel);

            int count = selectedGameObjects != null ? selectedGameObjects.Length : 0;
            EditorGUILayout.HelpBox(
                count > 0
                    ? $"{count} object(s) selected. The search includes all children."
                    : "Select one or more GameObjects in the Hierarchy to search.",
                count > 0 ? MessageType.Info : MessageType.Warning);

            using (new EditorGUI.DisabledScope(count == 0))
            {
                if (GUILayout.Button("Find Missing Scripts in Selection", GUILayout.Height(24)))
                {
                    sb.Clear();
                    FindInSelected();
                }
            }

            EditorGUILayout.Space();

            if (editor == null)
                editor = Editor.CreateEditor(this);
            editor.OnInspectorGUI();

            if (sb.Length > 0)
                EditorGUILayout.HelpBox(sb.ToString(),
                    missing_count > 0 ? MessageType.Warning : MessageType.Info);
        }
        private void FindInSelected()
        {
            missingGameObjects.Clear();
            go_count = 0;
            components_count = 0;
            missing_count = 0;

            foreach (GameObject g in selectedGameObjects)
            {
                FindInGO(g);
            }

            sb.AppendLine(string.Format("Searched {0} GameObjects, {1} components, found {2} missing", go_count, components_count, missing_count));
        }

        private void FindInGO(GameObject g)
        {
            go_count++;
            Component[] components = g.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                components_count++;
                if (components[i] == null)
                {
                    missing_count++;
                    string s = g.name;
                    Transform t = g.transform;
                    while (t.parent != null)
                    {
                        s = t.parent.name + "/" + s;
                        t = t.parent;
                    }

                    sb.AppendLine($"*{s} has an empty script attached in position: [{i}] {g}");
                    missingGameObjects.Add(g);
                }
            }

            foreach (Transform childT in g.transform)
            {
                FindInGO(childT.gameObject);
            }
        }

        private void OnSelectionChange()
        {
            selectedGameObjects = Selection.gameObjects;
        }

        [CustomEditor(typeof(FindMissingScriptsRecursively), true)]
        public class ListEditorDrawer : Editor
        {
            public override void OnInspectorGUI()
            {
                // Editor is cached, so pull the latest values from the target each repaint;
                // otherwise runtime changes to 'missingGameObjects' won't show.
                serializedObject.Update();
                var list = serializedObject.FindProperty("missingGameObjects");
                EditorGUILayout.PropertyField(list, new GUIContent("Missing targets"), true);
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}

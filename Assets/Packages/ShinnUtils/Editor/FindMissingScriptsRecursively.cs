using UnityEngine;
using UnityEditor;
using System.Text;

namespace Shinn
{
    public class FindMissingScriptsRecursively : EditorWindow
    {
        private static int go_count = 0, components_count = 0, missing_count = 0;
        private static StringBuilder sb = new StringBuilder();
        private static GameObject[] selectedGos;
        Editor editor;

        [SerializeField] private GameObject[] list;

        [MenuItem("ShiDevTools/Find Missing Scripts")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(FindMissingScriptsRecursively));
        }

        private void Awake()
        {
            editor = Editor.CreateEditor(this);
        }

        public void OnGUI()
        {
            Debug.Log("gui");
            list = selectedGos;

            if (editor != null)
                editor.OnInspectorGUI();

            if (GUILayout.Button("Find Missing Scripts in selected GameObjects"))
            {
                sb.Clear();
                FindInSelected();
            }

            GUILayout.Label(sb.ToString());
        }
        private static void FindInSelected()
        {
            selectedGos = Selection.gameObjects;
            go_count = 0;
            components_count = 0;
            missing_count = 0;
            foreach (GameObject g in selectedGos)
            {
                FindInGO(g);
            }
            sb.AppendLine(string.Format("Searched {0} GameObjects, {1} components, found {2} missing", go_count, components_count, missing_count));
        }

        private static void FindInGO(GameObject g)
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
                    //Debug.Log(s + " has an empty script attached in position: " + i, g);
                    sb.AppendLine($"*{s} has an empty script attached in position: [{i}] {g}");
                }
            }
            // Now recurse through each child GO (if there are any):
            foreach (Transform childT in g.transform)
            {
                //Debug.Log("Searching " + childT.name  + " " );
                FindInGO(childT.gameObject);
            }
        }

        [CustomEditor(typeof(FindMissingScriptsRecursively), true)]
        public class ListTestEditorDrawer : Editor
        {
            public override void OnInspectorGUI()
            {
                var list = serializedObject.FindProperty("list");
                EditorGUILayout.PropertyField(list, new GUIContent("Selected gameobjects:"), true);
            }
        }
    }
}
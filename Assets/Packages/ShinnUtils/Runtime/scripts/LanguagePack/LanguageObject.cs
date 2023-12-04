// Author: John Tsai
// Last update: 11302023

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Shinn
{
    [CreateAssetMenu(fileName = "LanguageObject", menuName = "Language/LanguageObject")]
    public class LanguageObject : ScriptableObject
    {
        #region DECLARE
        [Serializable]
        public class JsonData
        {
            public string[] language;
            public LanguageData[] datas;

            [Serializable]
            public class LanguageData
            {
                [TextArea]
                public string[] context;
                public string GUID;
            }
        }

        public JsonData allDatas;

        [Tooltip("Copy to GUID")]
        [SerializeField, Space] private string autoGenGUID;
        #endregion

        #region MAIN
        private void OnValidate()
        {
            autoGenGUID = Guid.NewGuid().ToString("N");
        }
        #endregion

        #region PUBLIC
        public JsonData.LanguageData Find(string uuid)
        {
            var dataGuids = new List<string>(allDatas.datas.Length);
            foreach (var data in allDatas.datas)
                dataGuids.Add(data.GUID);

            int index = dataGuids.IndexOf(uuid);
            if (index.Equals(-1))
            {
                Debug.LogError("<color=red>[Error]</color>Not found " + "<color=red>" + uuid + "</color>");
                return null;
            }
            else
                return allDatas.datas[index];
        }
        #endregion

        #region EDITOR
#if UNITY_EDITOR
        [MenuItem("Tools/ShinnDev/Language Object/Import")]
        public static void Import()
        {
            string dir = EditorUtility.OpenFilePanel("Load JSON from file", "", ",json,Json,JSON");

            try
            {
                StreamReader reader = new StreamReader(dir);
                var content = reader.ReadToEnd();
                reader.Close();

                Debug.Log($"<color=green>[Load success]</color> {dir}");

                var data = JsonConvert.DeserializeObject<JsonData>(content);
                var target = OnSelectionChange();
                target.allDatas = data;
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        [MenuItem("Tools/ShinnDev/Language Object/Export")]
        public static void Export()
        {
            var target = OnSelectionChange();
            string ToJson = JsonConvert.SerializeObject(target.allDatas);
            string path = EditorUtility.OpenFolderPanel("Save JSON to location", "", "");
            string fullname = Path.Combine(path, target.name + ".json");

            File.WriteAllText(fullname, ToJson);
            Debug.Log($"<color=green>[Export success]</color> {fullname}\n{ToJson}");
        }


        [Button(nameof(CopyGUID))]
        public bool buttonCopyGUID;
        public void CopyGUID()
        {
            GUIUtility.systemCopyBuffer = autoGenGUID;
        }

        private static LanguageObject OnSelectionChange()
        {
            if (Selection.activeObject is LanguageObject)
            {
                if (Selection.assetGUIDs.Length > 0)
                    return Selection.activeObject as LanguageObject;
            }
            return null;
        }
#endif
        #endregion
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ButtonAttribute))]
public class ButtonDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string methodName = (attribute as ButtonAttribute).MethodName;
        UnityEngine.Object target = property.serializedObject.targetObject;
        System.Type type = target.GetType();
        System.Reflection.MethodInfo method = type.GetMethod(methodName);
        if (method == null)
        {
            GUI.Label(position, "Method could not be found. Is it public?");
            return;
        }
        if (method.GetParameters().Length > 0)
        {
            GUI.Label(position, "Method cannot have parameters.");
            return;
        }
        if (GUI.Button(position, method.Name))
        {
            method.Invoke(target, null);
        }
    }
}
#endif
public class ButtonAttribute : PropertyAttribute
{
    public string MethodName { get; }
    public ButtonAttribute(string methodName)
    {
        MethodName = methodName;
    }
}

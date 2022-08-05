// Author: John Tsai

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
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
            public string[] label;
            public LanguageData[] datas;

            [Serializable]
            public class LanguageData
            {
                [TextArea]
                public string[] languageList;
                public string UUID;
            }
        }

        public JsonData allDatas;
        
        [SerializeField, Space] private string AutoGenUUID;
        #endregion

        #region MAIN
        private void OnValidate()
        {
            AutoGenUUID = Guid.NewGuid().ToString("N");
        }
        #endregion

        #region PUBLIC
        public JsonData.LanguageData Find(string uuid)
        {
            List<string> datauuid = new List<string>();
            foreach (var i in allDatas.datas)
                datauuid.Add(i.UUID);

            int index = Array.IndexOf(datauuid.ToArray(), uuid);
            if (index.Equals(-1))
            {
                Debug.LogError("<color=red>[Error]</color>Not found " + uuid);
                return null;
            }
            else
                return allDatas.datas[index];
        }
        #endregion

        #region EDITOR
#if UNITY_EDITOR
        [MenuItem("ShiDevTools/Language Object/Import")]
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

        [MenuItem("ShiDevTools/Language Object/Export")]
        public static void Export()
        {
            var target = OnSelectionChange();
            string ToJson = JsonConvert.SerializeObject(target.allDatas);
            string path = EditorUtility.OpenFolderPanel("Save JSON to location", "", "");
            string fullname = Path.Combine(path, target.name + ".json");

            File.WriteAllText(fullname, ToJson);
            Debug.Log($"<color=green>[Export success]</color> {fullname}\n{ToJson}");
        }

        private static LanguageObject OnSelectionChange()
        {
            if (Selection.activeObject is LanguageObject)
            {
                if (Selection.assetGUIDs.Length > 0)
                {
                    var target = Selection.activeObject as LanguageObject;
                    return target;
                }
            }
            return null;
        }
#endif
        #endregion
    }

}
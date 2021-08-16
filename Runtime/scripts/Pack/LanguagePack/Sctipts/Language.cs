// Author: John Tsai

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Shinn
{
    public class Language : MonoBehaviour
    {
        #region DECLARE
        public static Language instance;
        public LanguageObject pack;

        public string[] mode;
        public int currentMode = 0;

        private List<Text> alltexts = new List<Text>();
        #endregion

        #region MAIN
        private void Awake()
        {
            instance = this;
            alltexts = Resources.FindObjectsOfTypeAll<Text>().ToList();
        }

        private void Start()
        {
            mode = pack.allDatas.label;
            UpdateAllText(currentMode);
        }
        #endregion

        #region PUBLIC
        public string FindText(string uuid)
        {
            return pack.Find(uuid).languageList[currentMode];
        }

        public void UpdateAllText(int modeIndex)
        {
            for (int i = 0; i < alltexts.Count; i++)
                for (int j = 0; j < pack.allDatas.datas.Length; j++)
                    alltexts[i].text = pack.allDatas.datas[j].languageList[modeIndex].Trim();
        }
        #endregion

        #region Test
        [ContextMenu("Test_FindText")]
        private void Test_FindText()
        {
            print(FindText("f92630ed6b71490696de53b15d5d96ae"));
        }

        [ContextMenu("Test_UpdateAllTest")]
        private void Test_UpdateAllTest()
        {
            UpdateAllText(currentMode);
        }
        #endregion
    }
}

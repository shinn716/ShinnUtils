// Author: John Tsai
// Last update: 11302023

using UnityEngine;
using System;
using UnityEngine.Events;

namespace Shinn
{
    public class MultipleTextManager : MonoBehaviour
    {
        #region DECLARE
        public static MultipleTextManager instance;
        public LanguageObject pack;
        public int currentLanguageIndex = 0;
        public event Action<int> UpdateMTC;
        [Space]
        public UnityEvent<int> SetLanguageOnStart;
        #endregion

        #region MAIN
        private void Awake()
        {
            instance = this;
            currentLanguageIndex = PlayerPrefs.GetInt("language");
        }

        private void Start()
        {
            SetLanguageOnStart.Invoke(currentLanguageIndex);
        }
        #endregion

        #region PUBLIC
        public string FindText(string uuid, int _currentLanguageIndex)
        {
            return pack.Find(uuid).context[_currentLanguageIndex];
        }

        public void SetLanguage(int index)
        {
            UpdateMTC.Invoke(index);
            currentLanguageIndex = index;
            PlayerPrefs.SetInt("language", index);
        }
        #endregion

        #region Test
        [ContextMenu("CreateNewGUID")]
        private void CreateNewGUID()
        {
            foreach (var i in pack.allDatas.datas)
                i.GUID = Guid.NewGuid().ToString("N");
        }
        #endregion
    }
}

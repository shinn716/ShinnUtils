// Sources: https://stackoverflow.com/questions/42034245/unity-eventmanager-with-delegate-instead-of-unityevent
// Custom event manager

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Shinn
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager instance;

        private Dictionary<string, Action<object[]>> eventDictionary;
        
        private void Awake()
        {
            instance = this;

            if (eventDictionary == null)
                eventDictionary = new Dictionary<string, Action<object[]>>();
        }

        /// <summary>
        /// 新增事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="listener"></param>
        public void AddListening(string eventName, Action<object[]> listener)
        {
            if (instance == null)
                return;
                
            if (instance.eventDictionary.TryGetValue(eventName, out Action<object[]> thisEvent))
            {
                thisEvent += listener;
                instance.eventDictionary[eventName] = thisEvent;
            }
            else
            {
                thisEvent += listener;
                instance.eventDictionary.Add(eventName, thisEvent);
            }
        }

        /// <summary>
        /// 移除特定事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="listener"></param>
        public void RemoveListening(string eventName, Action<object[]> listener)
        {
            if (instance == null)
                return;

            if (instance.eventDictionary.TryGetValue(eventName, out Action<object[]> thisEvent))
            {
                thisEvent -= listener;
                instance.eventDictionary[eventName] = thisEvent;
            }
        }

        /// <summary>
        /// 特定事件觸發
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="eventParam"></param>
        public void TriggerEvent(string eventName, params object[] eventParam)
        {
            if (instance == null)
                return;

            if (instance.eventDictionary.TryGetValue(eventName, out Action<object[]> thisEvent))
                thisEvent.Invoke(eventParam);
        }
    }
}
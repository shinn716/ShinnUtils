using System.Runtime.InteropServices;
using UnityEngine;

public static class MessageSender
{
#if REACT_MSG && UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")] public extern static void unityMessage(string _msg);
#else
    private static void unityMessage(string msg) { Debug.Log($"send msg:\n{msg}"); }
#endif

    public static void SendMessage<T>(TYPE type, T data)
    {
        ReactMessage message = new()
        {
            type = type.ToString(),
            data = JsonUtility.ToJson(data),
        };
        unityMessage(JsonUtility.ToJson(message));
    }

    public enum TYPE
    {
        area_event,
        scene_data,
        hotspot_event,
        scene_status_event,
        measurement_event
    }
}

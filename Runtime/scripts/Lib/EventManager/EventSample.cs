using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shinn;

public class EventSample : MonoBehaviour
{
    public AudioSource AS;

    public static string EVENT_INIT = nameof(EVENT_INIT);

    // Start is called before the first frame update

    private void Awake()
    {
        if (GetComponent<EventManager>() == null)
            gameObject.AddComponent<EventManager>();
    }

    void Start()
    {
        EventManager.instance.AddListening(EVENT_INIT, OnSomethingHappened);
    }

    private void OnApplicationQuit()
    {
        EventManager.instance.RemoveListening(EVENT_INIT, OnSomethingHappened);
    }

    [ContextMenu("TriggerEvent")]
    void TriggerEvent()
    {
        EventManager.instance.TriggerEvent(EVENT_INIT, AS);
    }

    // get values passed by events
    void OnSomethingHappened(object[] parms)
    {
        AudioSource tmp = (AudioSource) parms[0];
        print("hello " + tmp.gameObject.name);
    }
}

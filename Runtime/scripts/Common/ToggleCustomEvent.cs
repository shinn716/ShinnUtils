using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Toggle))]
public class ToggleCustomEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent OnEnableEvent = new UnityEvent();
    [Header("isOn")]
    [SerializeField] private UnityEvent isOnEvents = new UnityEvent();
    [SerializeField] private UnityEvent isOffEvents = new UnityEvent();

    private void OnEnable()
    {
        OnEnableEvent.Invoke();
    }

    public void OnValueChange(bool isOn)
    {
        if (isOn)
            isOnEvents.Invoke();
        else
            isOffEvents.Invoke();
    }
}

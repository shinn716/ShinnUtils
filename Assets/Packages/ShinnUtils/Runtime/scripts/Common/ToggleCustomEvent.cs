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

    private Toggle toggle;

    private void OnEnable()
    {
        if (OnEnableEvent != null)
            OnEnableEvent.Invoke();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnValueChange);
    }

    private void OnValueChange(bool isOn)
    {
        if (isOn)
            isOnEvents.Invoke();
        else
            isOffEvents.Invoke();
    }
}

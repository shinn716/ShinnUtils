using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class ComponentDetails : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private string targetName;
    [SerializeField, ReadOnly] private string targetTag;
    [SerializeField, ReadOnly] private int targetLayer;
    [SerializeField, ReadOnly] private Vector3 targetPosition;
    [SerializeField, ReadOnly] private Vector3 targetRotation;
    [SerializeField, ReadOnly] private Vector3 targetScale;
    [SerializeField, ReadOnly] private Component[] components;


    [ContextMenu("Clear")]
    private void Clear()
    {
        target = null;
        targetName = string.Empty;
        targetTag = string.Empty;
        targetLayer = 0;
        targetPosition = Vector3.zero;
        targetRotation = Vector3.zero;
        targetScale = Vector3.one;
        components = new Component[0];
    }

    private void OnValidate()
    {
        if (target == null)
            return;

        targetName = target.name;
        targetTag = target.tag;
        targetLayer = target.gameObject.layer;
        targetPosition = target.localPosition;
        targetRotation = target.localRotation.eulerAngles;
        targetScale = target.localScale;
        components = target.GetComponents<Component>();
    }


    public class ReadOnlyAttribute : PropertyAttribute { }

    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        /// <summary>
        /// Unity method for drawing GUI in Editor
        /// </summary>
        /// <param name="position">Position.</param>
        /// <param name="property">Property.</param>
        /// <param name="label">Label.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Saving previous GUI enabled value
            var previousGUIState = GUI.enabled;
            // Disabling edit for property
            GUI.enabled = false;
            // Drawing Property
            EditorGUI.PropertyField(position, property, label);
            // Setting old GUI enabled value
            GUI.enabled = previousGUIState;
        }
    }

}

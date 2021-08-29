using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SimpleBehaviour))]
public class SimpleBehaviourEditor : Editor
{
    SerializedProperty BouncePeriodProp;
    SerializedProperty BounceHeightProp;
    SerializedProperty ConfigurationsProp;

    void OnEnable()
    {
        BouncePeriodProp = serializedObject.FindProperty("BouncePeriod");
        BounceHeightProp = serializedObject.FindProperty("BounceHeight");
        ConfigurationsProp = serializedObject.FindProperty("Configurations");
    }

    public override void OnInspectorGUI()
    {
        // draw the default inspector
        //DrawDefaultInspector();

        // draw the two properties
        EditorGUILayout.PropertyField(BouncePeriodProp);
        EditorGUILayout.PropertyField(BounceHeightProp);
        EditorGUILayout.PropertyField(ConfigurationsProp);

        // should we randomise the values
        if (GUILayout.Button("Randomise Values v1"))
        {
            BounceHeightProp.floatValue = Random.Range(0.5f, 5f);
            BouncePeriodProp.floatValue = Random.Range(1f, 10f);
        }

        // should we randomise the values
        if (GUILayout.Button("Randomise Values v2"))
        {
            var linkedObject = serializedObject.targetObject as SimpleBehaviour;
            linkedObject.RandomiseValues();
        }

        serializedObject.ApplyModifiedProperties();
    }
}

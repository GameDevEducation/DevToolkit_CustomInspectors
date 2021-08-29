using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Config))]
public class ConfigPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var prop_ID = property.FindPropertyRelative("ID");

        // group the variables into a foldout
        property.isExpanded = EditorGUI.Foldout(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
                                                property.isExpanded, "Key: " + prop_ID.stringValue);
        position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;                                                

        if (property.isExpanded)
        {
            // show the ID property        
            EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), prop_ID);
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // show the value type property
            var prop_ValueType = property.FindPropertyRelative("ValueType");
            EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
                                    prop_ValueType, new GUIContent("Type"));
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // draw only the corresponding value
            EType valueType = (EType)prop_ValueType.enumValueIndex;
            if (valueType == EType.Float)
            {
                var prop_Value = property.FindPropertyRelative("FloatValue");
                EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
                                        prop_Value, new GUIContent("Value"));
            }
            else if (valueType == EType.Integer)
            {
                var prop_Value = property.FindPropertyRelative("IntValue");
                EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
                                        prop_Value, new GUIContent("Value"));
            }
            else if (valueType == EType.String)
            {
                var prop_Value = property.FindPropertyRelative("StringValue");
                EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
                                        prop_Value, new GUIContent("Value"));
            }
            else if (valueType == EType.IntegerList)
            {
                var prop_Value = property.FindPropertyRelative("IntegerValues");
                float height = EditorGUI.GetPropertyHeight(prop_Value, true);

                EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, height),
                                        prop_Value, new GUIContent("Value"), true);
            }
        }

        property.serializedObject.ApplyModifiedProperties();
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        if (property.isExpanded)
        {
            height += 2 * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);

            // update the height depending on the type
            var prop_ValueType = property.FindPropertyRelative("ValueType");
            if ((EType)prop_ValueType.enumValueIndex == EType.IntegerList)
            {
                var prop_Value = property.FindPropertyRelative("IntegerValues");
                height += EditorGUI.GetPropertyHeight(prop_Value, true);
            }
            else
                height += 1 * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);

            return height;
        }
        else
            return height;
    }
}

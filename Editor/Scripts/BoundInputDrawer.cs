using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using ChukStuph.Input;

[CustomPropertyDrawer(typeof(BoundInput))]
public class BoundInputDrawer : PropertyDrawer
{
    private const float Spacing = 2f;

    private static readonly Dictionary<string, bool> foldouts =
        new Dictionary<string, bool>();

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        string key = property.propertyPath;

        if (!foldouts.ContainsKey(key))
            foldouts[key] = true;

        float y = position.y;

        var inputProp = property.FindPropertyRelative("input");
        string foldoutLabel = GetFoldoutLabel(inputProp);

        foldouts[key] = EditorGUI.Foldout(
            new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight),
            foldouts[key],
            foldoutLabel,
            true
        );
        y += EditorGUIUtility.singleLineHeight + Spacing;

        if (foldouts[key])
        {
            EditorGUI.indentLevel++;

            var inputRect = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(inputRect, inputProp);
            y += EditorGUIUtility.singleLineHeight + Spacing;

            if (inputProp.objectReferenceValue != null)
            {
                var inputObj = inputProp.objectReferenceValue as InputReactionSO;

                if (inputObj is InputReactionButtonSO)
                {
                    DrawEventField(property, "onPressed", ref y, position);
                    DrawEventField(property, "onHeld", ref y, position);
                    DrawEventField(property, "onReleased", ref y, position);
                }
                else if (inputObj is InputReactionValueSO<float>)
                {
                    DrawEventField(property, "onValueChangedFloat", ref y, position);
                    DrawEventField(property, "getValueFloat", ref y, position);
                }
                else if (inputObj is InputReactionValueSO<Vector2>)
                {
                    DrawEventField(property, "onValueChangedVector2", ref y, position);
                    DrawEventField(property, "getValueVector2", ref y, position);
                }
            }

            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
    }

    private string GetFoldoutLabel(SerializedProperty inputProp)
    {
        if (inputProp.objectReferenceValue == null)
            return "Unassigned";

        var inputObj = inputProp.objectReferenceValue as InputReactionSO;
        string typeLabel = "Unknown";

        if (inputObj is InputReactionButtonSO) typeLabel = "Button";
        else if (inputObj is InputReactionValueSO<float>) typeLabel = "Float";
        else if (inputObj is InputReactionValueSO<Vector2>) typeLabel = "Vector2";

        return $"{inputObj.name} [{typeLabel}]";
    }

    private void DrawEventField(SerializedProperty parent, string fieldName, ref float y, Rect position)
    {
        var prop = parent.FindPropertyRelative(fieldName);
        if (prop == null) return;

        float height = EditorGUI.GetPropertyHeight(prop);
        var rect = new Rect(position.x, y, position.width, height);
        EditorGUI.PropertyField(rect, prop);
        y += height + Spacing;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight + Spacing;

        string key = property.propertyPath;
        if (!foldouts.ContainsKey(key)) foldouts[key] = true;

        if (foldouts[key])
        {
            height += EditorGUIUtility.singleLineHeight + Spacing;

            var inputProp = property.FindPropertyRelative("input");
            if (inputProp.objectReferenceValue != null)
            {
                var inputObj = inputProp.objectReferenceValue as InputReactionSO;

                if (inputObj is InputReactionButtonSO)
                {
                    height += GetEventHeight(property, "onPressed");
                    height += GetEventHeight(property, "onHeld");
                    height += GetEventHeight(property, "onReleased");
                }
                else if (inputObj is InputReactionValueSO<float>)
                {
                    height += GetEventHeight(property, "onValueChangedFloat");
                    height += GetEventHeight(property, "getValueFloat");
                }
                else if (inputObj is InputReactionValueSO<Vector2>)
                {
                    height += GetEventHeight(property, "onValueChangedVector2");
                    height += GetEventHeight(property, "getValueVector2");
                }
            }
        }

        return height;
    }

    private float GetEventHeight(SerializedProperty parent, string fieldName)
    {
        var prop = parent.FindPropertyRelative(fieldName);
        if (prop == null) return 0f;
        return EditorGUI.GetPropertyHeight(prop) + Spacing;
    }
}

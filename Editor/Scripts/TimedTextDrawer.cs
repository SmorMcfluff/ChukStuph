using UnityEngine;
using UnityEditor;
using ChukStuph.Audio;

namespace ChukStuph.EditorScripts
{
    [CustomPropertyDrawer(typeof(TimedText))]
    public class TimedTextDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            float spacing = EditorGUIUtility.standardVerticalSpacing;
            float timestampWidth = position.width / 8;
            float lyricWidth = position.width - timestampWidth;

            Rect timestampRect = new(position.x, position.y, timestampWidth, position.height);
            Rect lyricRect = new(position.x + timestampWidth + spacing, position.y, lyricWidth - 2, position.height);

            EditorGUI.PropertyField(timestampRect, property.FindPropertyRelative("timestamp"), GUIContent.none);
            EditorGUI.PropertyField(lyricRect, property.FindPropertyRelative("text"), GUIContent.none);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}

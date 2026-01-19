using UnityEngine;
using UnityEditor;
using ChukStuph.Input;

namespace ChukStuph.EditorScripts
{

    [CustomEditor(typeof(InputBinder))]
    [CanEditMultipleObjects]
    public class InputBinderEditor : Editor
    {
        private SerializedProperty bindingsProp;

        private void OnEnable()
        {
            bindingsProp = serializedObject.FindProperty("bindings");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(bindingsProp, new GUIContent("Bindings"), true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
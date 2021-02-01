using UnityEngine;
using UnityEditor;

namespace Extend.DTAnimation.Editor
{
    public static class TriggerDrawer
    {
        public static void OnGUI(SerializedProperty property)
        {
            for (int i = 0; i < property.arraySize; i++)
            {
                EditorGUILayout.PropertyField(property.GetArrayElementAtIndex(i));
            }
        }
    }

    [CustomPropertyDrawer(typeof(AudioTrigger))]
    public class AudioTriggerPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return DTEditorUtil.LINE_HEIGHT * 2;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = DTEditorUtil.LINE_HEIGHT;
            EditorGUI.LabelField(position, "AudioTrigger");
            position.y += DTEditorUtil.LINE_HEIGHT;
            EditorGUI.indentLevel ++;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("m_audio"));
            EditorGUI.indentLevel --;
        }
    }

    [CustomPropertyDrawer(typeof(LogTrigger))]
    public class LogTriggerPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return DTEditorUtil.LINE_HEIGHT * 2;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = DTEditorUtil.LINE_HEIGHT;
            EditorGUI.LabelField(position, "LogTrigger");
            position.y += DTEditorUtil.LINE_HEIGHT;
            EditorGUI.indentLevel ++;
            SerializedProperty audio = property.FindPropertyRelative("m_content");
            EditorGUI.PropertyField(position, audio);
            EditorGUI.indentLevel --;
        }
    }
}
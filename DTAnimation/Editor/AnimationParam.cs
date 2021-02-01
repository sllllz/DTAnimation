using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace Extend.DTAnimation.Editor
{
    public class AnimationInlineParam {
        private class Param {
            public string FieldName;
            public float WidthInPercent;
            public GUIContent Label;
            public float LabelPercent = 0.5f;
            public bool UseButton = false;
        }

        private readonly List<Param> m_parameters = new List<Param>();
        private string m_conditionField;
        private bool m_fieldValue;

        public AnimationInlineParam Add(string fieldName, float percent, string displayName = "", float labelPercent = 0.3f, bool useButton = false) {
            m_parameters.Add(new Param {
                FieldName = fieldName,
                WidthInPercent = percent,
                Label = new GUIContent(displayName),
                LabelPercent = labelPercent,
                UseButton = useButton
            });
            return this;
        }

        public AnimationInlineParam Condition(string fieldName, bool value) {
            m_conditionField = fieldName;
            m_fieldValue = value;
            return this;
        }

        public bool GetVisible(SerializedProperty property) {
            if( string.IsNullOrEmpty(m_conditionField) ) {
                return true;
            }

            var fieldProp = property.FindPropertyRelative(m_conditionField);
            return fieldProp.boolValue == m_fieldValue;
        }

        public void OnGUI(Rect position, SerializedProperty property) {
            Assert.IsTrue(m_parameters.Count > 0);
            if( m_parameters.Count == 1 ) {
                DrawGUI(position, m_parameters[0], property);
            }
            else {
                var originLabelWidth = EditorGUIUtility.labelWidth;
                for( var i = 0; i < m_parameters.Count; i++ ) {
                    var parameter = m_parameters[i];
                    var rect = position;
                    rect.width *= parameter.WidthInPercent;
                    if( i < m_parameters.Count - 1 )
                        rect.xMax -= 5;
                    EditorGUIUtility.labelWidth = rect.width * parameter.LabelPercent;
                    DrawGUI(rect, parameter, property);
                    position.x = rect.xMax + 5;
                }

                EditorGUIUtility.labelWidth = originLabelWidth;
            }
        }

        private static void DrawGUI(Rect position, Param parameter, SerializedProperty property) {
            var p = property.FindPropertyRelative(parameter.FieldName);
            if( p == null ) {
                Debug.LogError($"field with name : {parameter.FieldName} not exist");
                return;
            }
            if(parameter.UseButton)
            {
                position.x += 15;
                if(GUI.Button(position, p.boolValue ? "From" : "To"))
                {
                    p.boolValue = !(p.boolValue);
                } 
            }
            else
                EditorGUI.PropertyField(position, p, parameter.Label);
        }
    }

    public class AnimationParamGUI {
        private readonly Dictionary<int, AnimationInlineParam> m_params;

        public AnimationParamGUI() {
            m_params = new Dictionary<int, AnimationInlineParam>();
        }

        public AnimationInlineParam GetRow(int index) {
            Assert.IsTrue(index >= 0);
            if(!m_params.ContainsKey(index))
                m_params[index] = new AnimationInlineParam();
            return m_params[index];
        }

        private int m_activeParamCount;
        public Rect OnGUI(Rect position, SerializedProperty property) {

            foreach( var inlineParam in m_params.Values ) {
                if(!inlineParam.GetVisible(property))
                    continue;
                inlineParam.OnGUI(position, property);
                position.y += DTEditorUtil.LINE_HEIGHT;
            }
            return position;
        }

        public int GetRowNum()
        {
            return m_params.Count;
        }
    }
}
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace Extend.DTAnimation.Editor
{
    [CustomPropertyDrawer(typeof(BaseCombine))]
    public class BaseCombinePropertyDrawer : PropertyDrawer
    {
        const int maxAnimationHeightSize = 4;
        public class AddParam{
            public SerializedProperty property;
            public Type t;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            var height = DTEditorUtil.LINE_HEIGHT;
            var enabledProp = property.FindPropertyRelative("m_enabled");
            if( !enabledProp.boolValue )
                return height;

            SerializedProperty animations = property.FindPropertyRelative("animations");
            int size = animations.arraySize;
            if(size <= 0)
                return height;
            
            height += DTEditorUtil.LINE_HEIGHT * size;
            int activeCount = 0;
            for (int i = 0; i < size; i++)
            {
                SerializedProperty aniProperty = animations.GetArrayElementAtIndex(i);
                SerializedProperty enable = aniProperty.FindPropertyRelative("m_enabled");
                if(enable.boolValue)
                {
                    activeCount ++;
                }
            }
            height += activeCount * DTEditorUtil.LINE_HEIGHT * maxAnimationHeightSize;
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = EditorGUIUtility.singleLineHeight;
            var enabledProp = property.FindPropertyRelative("m_enabled");
            if(!enabledProp.boolValue) { return; }
            DrawButtons(position, property);
            position.y += DTEditorUtil.LINE_HEIGHT;

            EditorGUI.indentLevel++;
            SerializedProperty animations = property.FindPropertyRelative("animations");
            int size = animations.arraySize;
            for (int i = 0; i < size; i++)
            {
                SerializedProperty aniProperty = animations.GetArrayElementAtIndex(i);
                SerializedProperty enable = aniProperty.FindPropertyRelative("m_enabled");
                var buttonrect = position;
                float arrowWidth = 30;
                buttonrect.width = position.width - arrowWidth * 3;
                string[] names = aniProperty.managedReferenceFullTypename.Split(' ');
                names = names[names.Length - 1].Split('.');
                enable.boolValue = EditorGUI.ToggleLeft(buttonrect, names[names.Length - 1].Replace("Animation", ""), enable.boolValue);
                buttonrect.x += buttonrect.width;
                buttonrect.width = arrowWidth;
                if(GUI.Button(buttonrect, "x"))
                {
                    RemoveAnimation(property, i);
                    break;
                }
                buttonrect.x += buttonrect.width;
                if(GUI.Button(buttonrect, "↓"))
                {
                    MoveAnimation(property, i, 1);
                    break;
                }
                buttonrect.x += buttonrect.width;
                if(GUI.Button(buttonrect, "↑"))
                {
                    MoveAnimation(property, i, -1);
                    break;
                }
                position.y += DTEditorUtil.LINE_HEIGHT;
                if(enable.boolValue)
                {
                    EditorGUI.PropertyField(position, aniProperty);
                    position.y += DTEditorUtil.LINE_HEIGHT * maxAnimationHeightSize;
                }
            }
            EditorGUI.indentLevel--;
        }

        void DrawButtons(Rect position, SerializedProperty property)
        {
            if(EditorGUI.DropdownButton(position, new GUIContent("Add Animation"), FocusType.Passive))
            {
                ShowAddMenu(property);
            }
        }

        void ShowAddMenu(SerializedProperty property)
        {
            GenericMenu menu = new GenericMenu();

            Dictionary<string, List<Type>> animations = new Dictionary<string, List<Type>>();
            for (int i = 0; i < (int)AnimationType.Max; i++)
            {
                animations[((AnimationType)i).ToString()] = new List<Type>();
            }
            var types = TypeCache.GetTypesWithAttribute(typeof(AnimationAttribute));
            foreach (var type in types)
            {
                AnimationAttribute atr = type.GetCustomAttribute<AnimationAttribute>();
                animations[atr.aType.ToString()].Add(type);
            }

            foreach (var item in animations)
            {
                string atr = item.Key;
                if(item.Value.Count == 0)
                {
                    menu.AddDisabledItem(new GUIContent(atr), false);
                    continue;
                }
                foreach (var type in item.Value)
                {
                    AddParam param = new AddParam {
                        property = property,
                        t = type
                    };
                    menu.AddItem(new GUIContent(atr + "/" + type.Name.ToString()), false, AddAnimation, param);
                }
            }
            menu.ShowAsContext();
        }

        void AddAnimation(object param)
        {
            AddParam p = (AddParam)param;
            DTAnimationEditor.NewAnimation(p.property, p.t);
        }

        void RemoveAnimation(SerializedProperty property, int index)
        {
            SerializedProperty animations = property.FindPropertyRelative("animations");
            int size = animations.arraySize;
            if(index >= 0 && index < size)
                animations.DeleteArrayElementAtIndex(index);
            property.serializedObject.ApplyModifiedProperties();
        }

        void MoveAnimation(SerializedProperty property, int curIndex, int offset)
        {
            SerializedProperty animations = property.FindPropertyRelative("animations");
            int size = animations.arraySize;
            int newIndex = curIndex + offset;
            if(newIndex < 0 || newIndex >= size)
                return;
            animations.MoveArrayElement(curIndex, newIndex);
            property.serializedObject.ApplyModifiedProperties();
        }
    }
}
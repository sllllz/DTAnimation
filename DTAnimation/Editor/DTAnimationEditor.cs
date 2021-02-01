namespace Extend.DTAnimation.Editor
{
    using UnityEngine;
    using UnityEditor;
    using UnityEditorInternal;
    using System;
    using System.Reflection;

    [CustomEditor(typeof(DTAnimation))]
    public class DTAnimationEditor : Editor
    {
        ReorderableList combineList;
        GenericMenu addCombineMenu;
        GenericMenu templateMenu;
        GenericMenu triggerMenu;
        bool showAnimation = true;

        public static int selectIndex = 0;

        void OnEnable()
        {
            combineList = new ReorderableList(serializedObject, serializedObject.FindProperty("m_combines"), true, false, true, true);
            combineList.drawElementCallback = (Rect rect, int index, bool selected, bool focused) =>
            {
                SerializedProperty property = combineList.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;
                rect.height = DTEditorUtil.LINE_HEIGHT;
                float width = rect.width / 2;
                float enabledWitth = 20;
                rect.width = width;
                string[] names = property.managedReferenceFullTypename.Split(' ');
                names = names[names.Length - 1].Split('.');
                EditorGUI.LabelField(rect, names[names.Length - 1]);

                rect.x += width;
                rect.width = width - enabledWitth;
                SerializedProperty name = property.FindPropertyRelative("m_editorName");
                name.stringValue = EditorGUI.TextField(rect, name.stringValue);

                rect.x += rect.width + 5;
                rect.width = enabledWitth;
                SerializedProperty enabled = property.FindPropertyRelative("m_enabled");
                enabled.boolValue = EditorGUI.ToggleLeft(rect, "", enabled.boolValue);
                
                if(selected && enabled.boolValue)
                {
                    selectIndex = index;
                    EditorGUILayout.PropertyField(property);
                }
            };

            combineList.drawHeaderCallback = (Rect rect) => {
                GUI.Label(rect, "combines");
            };

            combineList.onRemoveCallback = (ReorderableList list) => {
                RemoveCombine(list.index);
            };

            combineList.onAddCallback = (ReorderableList list) => {
                addCombineMenu.ShowAsContext();
            };
            
            InitDatas();
        }

        public override void OnInspectorGUI()
        {
            bool isInpreview = DTAnimationPreview.IsInPreview();
            bool isInPlaying = Application.isPlaying;

            if(isInPlaying)
            {
                GUIStyle s = new GUIStyle();
                s.normal.textColor = Color.red;
                GUILayout.Label("Only Can Modify In Editor Mode", s);
            }

            EditorGUI.BeginDisabledGroup(isInpreview || isInPlaying);

            EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("Animation", showAnimation ? DTEditorUtil.ButtonSelectedStyle : GUI.skin.button, GUILayout.Height(DTEditorUtil.LINE_HEIGHT * 2))) {showAnimation = true;}
            if(GUILayout.Button("Trigger", showAnimation ? GUI.skin.button : DTEditorUtil.ButtonSelectedStyle, GUILayout.Height(DTEditorUtil.LINE_HEIGHT * 2))) {showAnimation = false;}
            EditorGUILayout.EndHorizontal();

            if(showAnimation)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Preview"))
                {
                    if(combineList.index == -1)
                    {
                        EditorUtility.DisplayDialog("警告", "请选中一个Combine才可以预览", "是");
                        return;
                    }
                    DTAnimationPreview.Preview(combineList.serializedProperty.GetArrayElementAtIndex(combineList.index));
                }
                if (EditorGUILayout.DropdownButton(new GUIContent("CreateByTemplate"), FocusType.Passive))
                {
                    templateMenu.ShowAsContext();
                }
                EditorGUILayout.EndHorizontal();

                combineList.DoLayoutList();
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                if (EditorGUILayout.DropdownButton(new GUIContent("AddTrigger"), FocusType.Passive))
                {
                    triggerMenu.ShowAsContext();
                }
                EditorGUILayout.EndHorizontal();

                TriggerDrawer.OnGUI(serializedObject.FindProperty("m_executors"));
            }

            serializedObject.ApplyModifiedProperties();
            EditorGUI.EndDisabledGroup();
        }

        void AddCombineCall(object type)
        {
            CombineType cType = (CombineType)type;
            NewCombine(serializedObject, (CombineType)type);
        }

        void RemoveCombine(int index)
        {
            SerializedProperty combines = serializedObject.FindProperty("m_combines");
            int size = combines.arraySize;
            if(index >= 0 && index < size)
            {
                combines.DeleteArrayElementAtIndex(index);
            }
        }

        void AddByTemplate(object obj)
        {
            SerializedProperty combines = serializedObject.FindProperty("m_combines");
            BaseTemplate template = Activator.CreateInstance((Type)obj) as BaseTemplate;
            foreach (var item in template.combines)
            {
                NewCombine(serializedObject, item.combineType);
                SerializedProperty combine = combines.GetArrayElementAtIndex(combines.arraySize - 1);
                combine.FindPropertyRelative("m_editorName").stringValue = item.editorName;
                foreach (var type in item.animations)
                {
                    NewAnimation(combine, type);
                }
            }
            serializedObject.ApplyModifiedProperties();
        }

        void AddTrigger(object obj)
        {
            SerializedProperty executors = serializedObject.FindProperty("m_executors");
            executors.arraySize += 1;
            var prop = executors.GetArrayElementAtIndex(executors.arraySize - 1);
            prop.managedReferenceValue = Activator.CreateInstance((Type)obj);
            serializedObject.ApplyModifiedProperties();
        }

        void InitDatas()
        {
            templateMenu = new GenericMenu();
            var types = TypeCache.GetTypesWithAttribute(typeof(DotweenTemplateAttribute));
            foreach (var type in types)
            {
                DotweenTemplateAttribute atr = type.GetCustomAttribute<DotweenTemplateAttribute>();
                templateMenu.AddItem(new GUIContent(atr.dType.ToString()), false, AddByTemplate, type);
            }

            triggerMenu = new GenericMenu();
            types = TypeCache.GetTypesWithAttribute(typeof(TriggerAttribute));
            foreach (var type in types)
            {
                TriggerAttribute atr = type.GetCustomAttribute<TriggerAttribute>();
                triggerMenu.AddItem(new GUIContent(atr.tType.ToString()), false, AddTrigger, type);
            }

            addCombineMenu = new GenericMenu();
            for (int i = 0; i < (int)CombineType.Max; i++)
            {
                CombineType cType = (CombineType)i;
                addCombineMenu.AddItem(new GUIContent(cType.ToString()), false, AddCombineCall, cType);
            }
        }

        public static void NewCombine(SerializedObject serializedObject, CombineType cType)
        {
            Type combine;
            if(cType == CombineType.Concerrence)
            {
                combine = typeof(ConcurrenceCombine);
            }
            else if(cType == CombineType.Sequence)
            {
                combine = typeof(SequenceCombine);
            }
            else
            {
                Debug.LogError("AddCombine Wrong Type");
                return;
            }
            SerializedProperty combines = serializedObject.FindProperty("m_combines");
            combines.arraySize = combines.arraySize + 1;
            SerializedProperty prop = combines.GetArrayElementAtIndex(combines.arraySize - 1);
            prop.managedReferenceValue = Activator.CreateInstance(combine);
            serializedObject.ApplyModifiedProperties();
        }

        public static void NewAnimation(SerializedProperty property, Type type)
        {
            SerializedProperty animations = property.FindPropertyRelative("animations");
            int size = animations.arraySize;
            animations.arraySize++;
            SerializedProperty prop = animations.GetArrayElementAtIndex(animations.arraySize - 1);
            prop.managedReferenceValue = Activator.CreateInstance(type);
            property.serializedObject.ApplyModifiedProperties();
        }
    }


    [CustomEditor(typeof(SequenceDTAnimation))]
    public class SequenceDTAnimationEditor : DTAnimationEditor{}

    [CustomEditor(typeof(ConcurrenceDTAnimation))]
    public class ConcurrenceDTAnimationEditor : DTAnimationEditor{}
}
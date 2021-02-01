using UnityEngine;
using UnityEditor;

namespace Extend.DTAnimation.Editor
{
    [CustomPropertyDrawer(typeof(BaseAnimation))]
    public class BaseAnimationPropertyDrawer : PropertyDrawer
    {
        protected AnimationParamGUI combine;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return GetCombine().GetRowNum() * DTEditorUtil.LINE_HEIGHT;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var wi = EditorGUIUtility.labelWidth;
            GetCombine().OnGUI(position, property);
            EditorGUIUtility.labelWidth = wi;
        }

        AnimationParamGUI GetCombine() {
            if(combine == null)
            {
                combine = new AnimationParamGUI();
                combine.GetRow(0).Add("m_duration", 0.5f, "Duration").Add("m_delay", 0.5f, "Delay");
                combine.GetRow(1).Add("m_ease", 1, "Ease", 0.1f);
                combine.GetRow(2).Add("m_isFrom", 0.2f, "From", 0.5f, true).Add("m_to", 0.8f);
                ModifyCombine();
            }

            return combine;
        }

        protected virtual void ModifyCombine(){}
    }

    [CustomPropertyDrawer(typeof(PunchAnimation))]
    public class PunchAnimationPropertyDrawer : BaseAnimationPropertyDrawer
    {
        protected override void ModifyCombine() {
            combine.GetRow(3).Add("m_vibrato", 0.5f, "Vibrato").Add("m_elasticity", 0.5f, "Elasticity");
        }
    }

    [CustomPropertyDrawer(typeof(ShakeAnimation))]
    public class ShakeAnimationPropertyDrawer : BaseAnimationPropertyDrawer
    {
        protected override void ModifyCombine() {
            combine.GetRow(3).Add("m_vibrato", 0.5f, "Vibrato", 0.5f).Add("m_randomness", 0.5f, "Randomness", 0.5f);
        }
    }

    [CustomPropertyDrawer(typeof(FadeAnimation))]
    public class FadeAnimationPropertyDrawer : BaseAnimationPropertyDrawer
    {
        protected override void ModifyCombine() {
            combine.GetRow(3).Add("m_fadeTarget", 1, "TargetType");
        }
    }

    [CustomPropertyDrawer(typeof(ScaleAnimation))]
    public class ScaleAnimationPropertyDrawer : BaseAnimationPropertyDrawer
    {
        protected override void ModifyCombine() {
            combine.GetRow(3).Add("m_relative", 1, "Releative");
        }
    }

    [CustomPropertyDrawer(typeof(RotateAnimation))]
    public class RotateAnimationPropertyDrawer : BaseAnimationPropertyDrawer
    {
        protected override void ModifyCombine() {
            combine.GetRow(3).Add("m_relative", 0.5f, "Releative", 0.5f).Add("m_islocalRotate", 0.5f, "LocalRotate", 0.5f);
        }
    }

    [CustomPropertyDrawer(typeof(MoveAnimation))]
    public class MoveAnimationPropertyDrawer : BaseAnimationPropertyDrawer
    {
        protected override void ModifyCombine() {
            combine.GetRow(3).Add("m_relative", 0.5f, "Releative", 0.5f).Add("m_islocalMove", 0.5f, "LocalMove", 0.5f);
        }
    }
}
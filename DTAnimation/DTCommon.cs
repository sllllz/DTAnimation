using System;

namespace Extend.DTAnimation
{
    public enum AnimationType
    {
        Move,
        Scale,
        Rotation,
        Alpha,
        Punch,
        Shake,
        Path,
        Max
    }

    public enum CombineType
    {
        Concerrence,
        Sequence,
        Max
    }

    public enum DotweenTemplateType
    {
        Button,
        UIView,
        UIFly,
        Max,
    }

    public enum TriggerType
    {
        Sound,
        Log
    }

    public class AnimationAttribute : Attribute
    {
        public AnimationType aType;

        public AnimationAttribute(AnimationType aType)
        {
            this.aType = aType;
        }
    }

    public class CombineAttribute : Attribute
    {
        public CombineType cType;
        public CombineAttribute(CombineType cType)
        {
            this.cType = cType;
        }
    }

    public class DotweenTemplateAttribute : Attribute
    {
        public DotweenTemplateType dType;
        public DotweenTemplateAttribute(DotweenTemplateType dType)
        {
            this.dType = dType;
        }
    }

    public class TriggerAttribute : Attribute
    {
        public TriggerType tType;
        public TriggerAttribute(TriggerType tType)
        {
            this.tType = tType;
        }
    }
}
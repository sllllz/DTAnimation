using UnityEngine;
using DG.Tweening;
using System;

namespace Extend.DTAnimation
{
    [Serializable, Animation(AnimationType.Rotation)]
    public class RotateAnimation : BaseAnimation
    {
        [SerializeField]
        Vector3 m_to;
        Vector3 To
        {
            get => m_to;
            set
            {
                if (m_to == value)
                    return;
                m_to = value;
                m_isdirty = true;
            }
        }

        [SerializeField]
        bool m_islocalRotate;
        public bool IsLocalRotate
        {
            get => m_islocalRotate;
            set
            {
                if (m_islocalRotate == value)
                    return;
                m_islocalRotate = value;
                m_isdirty = true;
            }
        }

        protected override Tween GenerateTween(UnityEngine.Object t)
        {
            Transform trans = t as Transform;
            if(IsLocalRotate)
                return trans.DOLocalRotate(To, Duration);
            else
                return trans.DORotate(To, Duration);
        }
    }
}
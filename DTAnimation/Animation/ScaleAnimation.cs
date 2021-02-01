using UnityEngine;
using DG.Tweening;
using System;

namespace Extend.DTAnimation
{
    [Serializable, Animation(AnimationType.Scale)]
    public class ScaleAnimation : BaseAnimation
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

        protected override Tween GenerateTween(UnityEngine.Object t)
        {
            Transform trans = t as Transform;
            return trans.DOScale(To, Duration);
        }
    }
}
using UnityEngine;
using DG.Tweening;
using System;

namespace Extend.DTAnimation
{
    [Serializable, Animation(AnimationType.Move)]
    public class MoveAnimation : BaseAnimation
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
        bool m_islocalMove;
        public bool IsLocalMove
        {
            get => m_islocalMove;
            set
            {
                if (m_islocalMove == value)
                    return;
                m_islocalMove = value;
                m_isdirty = true;
            }
        }

        protected override Tween GenerateTween(UnityEngine.Object t)
        {
            Transform trans = t as Transform;
            RectTransform rect = trans as RectTransform;

            Tween tween = null;
            if(IsLocalMove)
                tween = trans.DOLocalMove(To, Duration);
            else if(rect != null)
                tween = rect.DOAnchorPos3D(To, Duration);
            else
                tween = trans.DOMove(To, Duration);

            return tween;
        }
    }
}
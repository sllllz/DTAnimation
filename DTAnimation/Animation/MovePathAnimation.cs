using UnityEngine;
using DG.Tweening;
using System;

namespace Extend.DTAnimation
{
    [Serializable, Animation(AnimationType.Path)]
    public class MovePathAnimation : BaseAnimation
    {
        [SerializeField]
        Vector3 m_offset;
        Vector3 Offset
        {
            get => m_offset;
            set
            {
                if (m_offset == value)
                    return;
                m_offset = value;
                m_isdirty = true;
            }
        }

        [SerializeField]
        PathType m_pathType;
        PathType pathType
        {
            get => m_pathType;
            set
            {
                if (m_pathType == value)
                    return;
                m_pathType = value;
                m_isdirty = true;
            }
        }

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
            if(pathType == PathType.CubicBezier)
            {
                Vector3[] path = new Vector3[3];
                path[0] = trans.localPosition;
                path[1] = path[0] + Offset;
                path[2] = To;
                return trans.DOLocalPath(path, Duration, pathType).SetDelay(Delay).SetEase(ease);
            }
            else
            {
                Vector3[] path = new Vector3[3];
                path[0] = trans.localPosition;
                path[1] = path[0] + Offset;
                path[2] = To;
                return trans.DOLocalPath(path, Duration, pathType).SetDelay(Delay).SetEase(ease);
            }
        }
    }
}

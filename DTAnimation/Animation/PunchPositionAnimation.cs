using UnityEngine;
using DG.Tweening;
using System;

namespace Extend.DTAnimation
{
    [Serializable, Animation(AnimationType.Punch)]
    public class PunchPositionAnimation : PunchAnimation
    {
        protected override Tween GenerateTween(UnityEngine.Object t)
        {
            Transform trans = t as Transform;
            RectTransform rect = trans as RectTransform;
            if(rect == null)
                return trans.DOPunchPosition(To, Duration, Vibrato, Elasticity);
            else
                return rect.DOPunchAnchorPos(To, Duration, Vibrato, Elasticity);
        }
    }
}
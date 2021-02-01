using UnityEngine;
using DG.Tweening;
using System;

namespace Extend.DTAnimation
{
    [Serializable, Animation(AnimationType.Punch)]
    public class PunchScaleAnimation : PunchAnimation
    {
        protected override Tween GenerateTween(UnityEngine.Object t)
        {
            Transform transform = t as Transform;
            return transform.DOPunchScale(To, Duration, Vibrato, Elasticity);
        }
    }
}

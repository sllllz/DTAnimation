using UnityEngine;
using DG.Tweening;
using System;

namespace Extend.DTAnimation
{
    [Serializable, Animation(AnimationType.Punch)]
    public class PunchRotateAnimation : PunchAnimation
    {
        protected override Tween GenerateTween(UnityEngine.Object t)
        {
            Transform transform = t as Transform;
            return transform.DOPunchRotation(To, Duration, Vibrato, Elasticity);
        }
    }
}

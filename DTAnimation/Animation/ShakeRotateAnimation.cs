using UnityEngine;
using DG.Tweening;
using System;

namespace Extend.DTAnimation
{
    [Serializable, Animation(AnimationType.Shake)]
    public class ShakeRotateAnimation : ShakeAnimation
    {
        protected override Tween GenerateTween(UnityEngine.Object t)
        {
            Transform transform = t as Transform;
            return transform.DOShakeRotation(Duration, To, Vibrato, Randomness);
        }
    }
}

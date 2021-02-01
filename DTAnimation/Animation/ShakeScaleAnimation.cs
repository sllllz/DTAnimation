using UnityEngine;
using DG.Tweening;
using System;

namespace Extend.DTAnimation
{
    [Serializable, Animation(AnimationType.Shake)]
    public class ShakeScaleAnimation : ShakeAnimation
    {
        protected override Tween GenerateTween(UnityEngine.Object t)
        {
            Transform transform = t as Transform;
            return transform.DOShakeScale(Duration, To, Vibrato, Randomness);
        }
    }
}

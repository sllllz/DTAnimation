using UnityEngine;
using DG.Tweening;
using System;

namespace Extend.DTAnimation
{
    [Serializable, Animation(AnimationType.Shake)]
    public class ShakePositionAnimation : ShakeAnimation
    {
        protected override Tween GenerateTween(UnityEngine.Object t)
        {
            Transform trans = t as Transform;
            RectTransform rect = trans as RectTransform;
            if(rect == null)
                return trans.DOShakePosition(Duration, To, Vibrato, Randomness);
            else
                return rect.DOShakeAnchorPos(Duration, To, Vibrato, Randomness);
        }
    }
}
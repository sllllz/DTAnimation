using UnityEngine;
using DG.Tweening;
using System;
using System.Linq;

namespace Extend.DTAnimation
{
    [Serializable, Combine(CombineType.Concerrence)]
    public class ConcurrenceCombine : BaseCombine
    {
        public override void Active(Transform t) 
        {
            foreach (var animation in Animations)
            {
                Tween tween = animation.Active(t);
                if(tween == null)
                    continue;
                tween.onComplete += CombineComplete;
                tween.Restart();
            }
            
            OnCombinePlay();
        }

        public override bool IsCombineComplete() 
        {
            if(Animations.Any(animation => animation.tween != null && !animation.tween.IsComplete()))
                return false;

            return true;
        }

        protected override void OnCombineComplete() 
        {
            foreach (var animation in Animations)
            {
                Tween tween = animation.tween;
                if(tween == null)
                    continue;
                tween.onComplete -= CombineComplete;
            }
            base.OnCombineComplete();
        }
    }
}

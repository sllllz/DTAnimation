using UnityEngine;
using DG.Tweening;
using System;
using System.Linq;

namespace Extend.DTAnimation
{
    [Serializable, Combine(CombineType.Sequence)]
    public class SequenceCombine : BaseCombine
    {
        int index = 0;

        public override void Active(Transform t) 
        {
            index = 0;
            foreach (var animation in Animations)
            {
                Tween tween = animation.Active(t);
                if(tween == null)
                    continue;
                tween.onComplete += PlayNext;
            }
            
            OnCombinePlay();
            PlayNext();
        }

        public override bool IsCombineComplete() 
        {
            if(Animations.Any(animation => animation.tween != null && !animation.tween.IsComplete()))
                return false;
            if(index < animationCounts)
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
                tween.onComplete -= PlayNext;
            }
            base.OnCombineComplete();
        }

        void PlayNext()
        {
            for (int i = index; i < Animations.Count; i++)
            {
                var animation = GetEnabledAnimation(i);
                if(animation != null)
                {
                    index = i + 1;
                    animation.tween.Restart();
                    return;
                }
            }
            CombineComplete();
        }
    }
}

using UnityEngine;
using System;

namespace Extend.DTAnimation
{
    public class ConcurrenceDTAnimation : DTAnimation
    {
        public override void Play(Action playCall = null, Action endCall = null)
        {
            Stop();
            base.Play(playCall, endCall);
            for (int i = 0; i < Combines.Count; i++)
            {
                var combine = GetEnabledCombine(i);
                if(combine != null)
                {
                    combine.CombineCompleteCall = AnimationComplete;
                    combine.Active(transform);
                }
            }
            OnAnimationPlay();
            AnimationComplete();
        }

        protected override void OnAnimationComplete()
        {
            foreach (var combine in Combines)
            {
                combine.CombineCompleteCall = null;
            }
        }

        protected override bool IsAnimationComplete()
        {
            for (int i = 0; i < Combines.Count; i++)
            {
                var combine = GetEnabledCombine(i);
                if(!combine.IsCombineComplete())
                    return false;
            }
            return true;
        }

        public override void Stop()
        {
            for (int i = 0; i < Combines.Count; i++)
            {
                var combine = GetEnabledCombine(i);
                if(combine != null)
                {
                    combine.Stop();
                }
            }
        }
    }
}
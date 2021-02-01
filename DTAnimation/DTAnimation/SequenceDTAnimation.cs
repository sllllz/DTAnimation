using UnityEngine;
using System;
using System.Linq;

namespace Extend.DTAnimation
{
    public class SequenceDTAnimation : DTAnimation
    {
        int m_curIndex = 0;
        BaseCombine m_showCombine;
        
        public override void Play(Action playCall = null, Action endCall = null)
        {
            Stop();
            base.Play(playCall, endCall);
            m_curIndex = 0;
            OnAnimationPlay();
            PlayNext();
        }

        void PlayNext()
        {
            for (int i = m_curIndex; i < Combines.Count; i++)
            {
                m_showCombine = GetEnabledCombine(i);
                if(m_showCombine != null)
                {
                    m_curIndex = i + 1;
                    m_showCombine.CombineCompleteCall = PlayNext;
                    m_showCombine.Active(transform);
                    return;
                }
            }

            AnimationComplete();
        }

        protected override void OnAnimationComplete()
        {
            m_curIndex = 0;
            foreach (var combine in Combines)
            {
                combine.CombineCompleteCall = null;
            }
            m_showCombine = null;
        }
        
        protected override bool IsAnimationComplete()
        {
            if(Combines.Any(combine => combine.Enabled && !combine.IsCombineComplete()))
                return false;
            
            if(m_curIndex < Combines.Count)
                return false;
            
            return true;
        }

        public override void Stop()
        {
            m_curIndex = Combines.Count;
            if(m_showCombine != null)
                m_showCombine.Stop();
        }
    }
}
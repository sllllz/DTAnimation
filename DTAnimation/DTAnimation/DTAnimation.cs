using UnityEngine;
using System;
using System.Collections.Generic;

namespace Extend.DTAnimation
{
    public abstract class DTAnimation : MonoBehaviour
    {
        [SerializeReference]
        List<BaseCombine> m_combines = new List<BaseCombine>();
        public List<BaseCombine> Combines => m_combines;

        [SerializeReference]
        List<ITrigger> m_executors = new List<ITrigger>();

        void OnDisable(){Stop();}

        protected Action animationPlayCall;
        protected Action animationCompleteCall;

        public virtual void Play(Action playCall = null, Action endCall = null)
        {
            animationPlayCall = playCall; 
            animationCompleteCall = endCall;
        }
        
        protected void AnimationComplete()
        {
            if(!IsAnimationComplete()) return;
            OnAnimationComplete();
            animationCompleteCall?.Invoke();
        }

        protected virtual void OnAnimationPlay(){
            animationPlayCall?.Invoke();
            foreach (var executor in m_executors)
            {
                executor.Execute();
            }
        }

        public abstract void Stop();
        protected abstract bool IsAnimationComplete();
        protected abstract void OnAnimationComplete();

        public BaseCombine GetEnabledCombine(int i)
        {
            if(i < 0 || i >= m_combines.Count || !m_combines[i].Enabled)
                return null;
            return m_combines[i];
        }
        
        public BaseAnimation GetEnabledAnimation(int combineIndex, int aniIndex)
        {
            BaseCombine combine = GetEnabledCombine(combineIndex);
            if(combine == null)
                return null;
            
            return combine.GetEnabledAnimation(aniIndex);
        }
    }
}

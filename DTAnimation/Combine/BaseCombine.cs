using UnityEngine;
using DG.Tweening;
using System;
using System.Collections.Generic;

namespace Extend.DTAnimation
{
    [Serializable]
    public class BaseCombine : IAniamationPreview
    {
        [SerializeField]
        string m_editorName = "combine";

        [SerializeReference]
        List<BaseAnimation> animations = new List<BaseAnimation>();
        public List<BaseAnimation> Animations => animations;
        public int animationCounts => animations.Count;

        [SerializeField]
        bool m_enabled;
        public bool Enabled => m_enabled;

        [SerializeField]
        bool m_loop;
        public bool Loop => m_loop;

        public Action CombineCompleteCall;

        public virtual void Active(Transform t) {}
        public virtual bool IsCombineComplete() { return true;}
        protected void OnCombinePlay() {}
        protected virtual void OnCombineComplete() {}

        protected void CombineComplete()
        {
            if(!IsCombineComplete())
                return;
            
            OnCombineComplete();
            CombineCompleteCall?.Invoke();
        }

        public void CollectPreviewTween(Transform t)
        {
            foreach (var animation in Animations)
            {
                animation.Active(t);
            }
        }

        public void Stop() {
            foreach (var animation in Animations)
            {
                if(animation.tween != null)
                    animation.tween.Complete();
            }
        }

        public BaseAnimation GetEnabledAnimation(int index)
        {
            if(index < 0 || index >= animations.Count || !animations[index].Enabled)
                return null;
            
            return animations[index];
        }
    }
}

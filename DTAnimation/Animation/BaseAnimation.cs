using UnityEngine;
using DG.Tweening;
using System;

namespace Extend.DTAnimation
{
    [Serializable]
    public abstract class BaseAnimation
    {
        protected bool m_isdirty = false;

        [SerializeField]
        bool m_enabled;
        public bool Enabled => m_enabled;

        [SerializeField]
        bool m_relative;
        public bool Releative{
            get => m_relative;
            set
            {
                if (m_relative == value)
                    return;
                m_relative = value;
                m_isdirty = true;
            }
        }

        [SerializeField]
        bool m_isFrom;
        public bool IsForm{
            get => m_isFrom;
            set
            {
                if (m_isFrom == value)
                    return;
                m_isFrom = value;
                m_isdirty = true;
            }
        }

        [SerializeField]
        float m_duration = 1;
        public float Duration
        {
            get => m_duration;
            set
            {
                if (m_duration == value)
                    return;
                m_duration = value;
                m_isdirty = true;
            }
        }

        [SerializeField]
        float m_delay;
        public float Delay
        {
            get => m_delay;
            set
            {
                if (m_delay == value)
                    return;

                m_delay = value;
                m_isdirty = true;
            }
        }

        [SerializeField]
        Ease m_ease;
        public Ease ease
        {
            get => m_ease;
            set {
                if (m_ease == value)
                    return;
                m_ease = value;
                m_isdirty = true;
            }
        }

        Tween m_cacheTween;

        public Tween tween => m_cacheTween;

        public Tween Active(Transform t)
        {
            if(!Enabled)
                return null;
            
            if(m_cacheTween == null || !m_cacheTween.IsActive() || m_isdirty || !Application.isPlaying)
            {
                if(m_cacheTween != null)
                    m_cacheTween.Kill();
                m_cacheTween = GenerateTween(t);
                if (IsForm) {
                    ((Tweener)m_cacheTween).From(Releative);
                } else {
                    m_cacheTween.SetRelative(Releative);
                }
                m_cacheTween.SetDelay(Delay).SetEase(ease).SetAutoKill(false);
                m_isdirty = false;
                m_cacheTween.onPlay += TweenOnPlay;
                m_cacheTween.onComplete += TweenOnComplete;
                m_cacheTween.onKill += TweenOnKill;
                m_cacheTween.Pause();
            }
            return m_cacheTween;
        }

        protected void TweenOnKill()
        {
            m_cacheTween = null;
        }

        protected void TweenOnPlay()
        {
        }

        protected void TweenOnComplete()
        {
        }

        protected abstract Tween GenerateTween(UnityEngine.Object t);
    }
}

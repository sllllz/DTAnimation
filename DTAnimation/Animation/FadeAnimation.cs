using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace Extend.DTAnimation
{
    [Serializable, Animation(AnimationType.Alpha)]
    public class FadeAnimation : BaseAnimation
    {
        public enum FadeTargetType
        {
            CanvasGrooup,
            Material,
            Image,
            Text,
            Light,
            SpriteRender,
        }
        
        [SerializeField]
        float m_to = 0;
        public float To
        {
            get => m_to;
            set
            {
                if (m_to == value)
                    return;
                m_to = value;
                m_isdirty = true;
            }
        }

        [SerializeField]
        FadeTargetType m_fadeTarget;
        public FadeTargetType FadeTarget
        {
            get => m_fadeTarget;
            set
            {
                if (m_fadeTarget == value)
                    return;
                m_fadeTarget = value;
                m_isdirty = true;
            }
        }

        protected override Tween GenerateTween(UnityEngine.Object t)
        {
            Transform transform = t as Transform;
            Tween tween = null;
            if(FadeTarget == FadeTargetType.CanvasGrooup)
                return transform.GetComponent<CanvasGroup>().DOFade(To, Duration);
            else if(FadeTarget == FadeTargetType.Material)
                return transform.GetComponent<Renderer>().material.DOFade(To, Duration);
            else if(FadeTarget == FadeTargetType.Image)
                return transform.GetComponent<Image>().DOFade(To, Duration);
            else if(FadeTarget == FadeTargetType.Text)
                return transform.GetComponent<Text>().DOFade(To, Duration);
            else if(FadeTarget == FadeTargetType.Light)
                return transform.GetComponent<Light>().DOIntensity(To, Duration);
            else if(FadeTarget == FadeTargetType.SpriteRender)
                return transform.GetComponent<SpriteRenderer>().DOFade(To, Duration);

            return tween;
        }
    }
}

using UnityEngine;

namespace Extend.DTAnimation
{
    public abstract class ShakeAnimation : BaseAnimation
    {
        [SerializeField]
        Vector3 m_to;
        protected Vector3 To
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
        int m_vibrato = 10;
        protected int Vibrato
        {
            get => m_vibrato;
            set
            {
                if (m_vibrato == value)
                    return;
                m_vibrato = value;
                m_isdirty = true;
            }
        }

        [SerializeField]
        float m_randomness = 90;
        protected float Randomness
        {
            get => m_randomness;
            set
            {
                if (m_randomness == value)
                    return;
                m_randomness = value;
                m_isdirty = true;
            }
        }
    }
}

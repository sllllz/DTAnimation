using UnityEngine;

namespace Extend.DTAnimation
{
    public abstract class PunchAnimation : BaseAnimation
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
        float m_elasticity = 1;
        protected float Elasticity
        {
            get => m_elasticity;
            set
            {
                if (m_elasticity == value)
                    return;
                m_elasticity = value;
                m_isdirty = true;
            }
        }
    }
}

using System;
using UnityEngine;

namespace Extend.DTAnimation
{
    [Serializable, TriggerAttribute(TriggerType.Log)]
    public class LogTrigger : ITrigger
    {
        [SerializeField]
        string m_content;

        public void Execute(){
            Debug.Log(m_content);
        }
    }
}

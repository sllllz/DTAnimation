using System;
using UnityEngine;

namespace Extend.DTAnimation
{
    [Serializable, TriggerAttribute(TriggerType.Sound)]
    public class AudioTrigger : ITrigger
    {
        [SerializeField]
        AudioClip m_audio;

        AudioSource audioSource;
        public void Execute(){
            if(audioSource == null){
                GameObject o = GameObject.Find("AudioSource");
                if(o == null)
                {
                    o = new GameObject("AudioSource");
                    o.AddComponent<AudioSource>();
                }
                audioSource = o.GetComponent<AudioSource>();
            }

            audioSource.PlayOneShot(m_audio);
        }
    }
}

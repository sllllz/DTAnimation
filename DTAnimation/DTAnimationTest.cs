using UnityEngine;
using System;

namespace Extend.DTAnimation
{
    [Serializable]
    public class DTAnimationTest : MonoBehaviour
    {
        Action playCall => ()=> {Debug.LogError("playCall");};
        Action endCall => ()=> {Debug.LogError("endCall");};
        
        void OnGUI()
        {
            GUILayout.BeginHorizontal();
            if(GUILayout.Button("ShowSequence", GUILayout.Width(100), GUILayout.Height(100)))
            {
                GetComponent<SequenceDTAnimation>().Play(playCall, endCall);
            }
            if(GUILayout.Button("StopSequence", GUILayout.Width(100), GUILayout.Height(100)))
            {
                GetComponent<SequenceDTAnimation>().Stop();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if(GUILayout.Button("ShowConcurrence", GUILayout.Width(100), GUILayout.Height(100)))
            {
                GetComponent<ConcurrenceDTAnimation>().Play(playCall, endCall);
            }
            if(GUILayout.Button("StopConcurrence", GUILayout.Width(100), GUILayout.Height(100)))
            {
                GetComponent<ConcurrenceDTAnimation>().Stop();
            }
            GUILayout.EndHorizontal();
        }
    }
}
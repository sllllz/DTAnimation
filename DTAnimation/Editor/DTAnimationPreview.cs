using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using DG.DOTweenEditor;
using System.Linq;
using DG.Tweening;

namespace Extend.DTAnimation.Editor
{
    public class DTAnimationPreview {

        public class PreviewTweenInfo{
            public Tween tween;
            public bool isFrom;
        }

        public static SerializedProperty curPreviewProperty;
        public static Action PreviewEndCall;
        static List<PreviewTweenInfo> preivewTweens = new List<PreviewTweenInfo>();
        public static void Preview(SerializedProperty property)
        {
            if(curPreviewProperty != null)
                return;
            StartupGlobalPreview();

            curPreviewProperty = property;
            Transform transform = PreviewComponent(property, out var combine);
            if(combine == null)
            {
                Stop();
                return;
            }
            combine.CollectPreviewTween(transform);
            foreach (var item in combine.Animations)
            {
                AddPreviewTween(item);
            }

            CombineType type = combine.GetType().GetCustomAttribute<CombineAttribute>().cType;
            if(type == CombineType.Concerrence)
            {
                PreviewConcerrence(transform, combine);
            }
            else if(type == CombineType.Sequence)
            {
                PreviewSequence(transform, combine);
            }
            else
            {
                Debug.LogError("Wrong Preview Type");
            }
        }

        static void PreviewConcerrence(Transform transform, BaseCombine combine)
        {
            PreviewEndCall = Stop;
            ExePreview(transform, preivewTweens);
        }

        static void PreviewSequence(Transform transform, BaseCombine combine)
        {
            int index = 0;
            PreviewEndCall = () => {
                if(index >= preivewTweens.Count)
                {
                    Stop();
                    return;
                }
                List<PreviewTweenInfo> temp = new List<PreviewTweenInfo>();
                temp.Add(preivewTweens[index++]);
                ExePreview(transform, temp);
            };

            PreviewEndCall();
        }

        static void AddPreviewTween(BaseAnimation animation)
        {
            if(animation.Enabled && animation.tween != null)
            {
                preivewTweens.Add(new PreviewTweenInfo{
                    tween = animation.tween,
                    isFrom = animation.IsForm
                });
            }
        }
        static void ExePreview(Transform transform, List<PreviewTweenInfo> tweens)
        {
            if(tweens.Count == 0)
            {
                PreviewEndCall?.Invoke();
                return;
            }

            foreach (var preview in tweens)
            {
                var tween = preview.tween;
                DOTweenEditorPreview.PrepareTweenForPreview(tween);
                tween.onComplete += ()=> {
                    if( tweens.Any(t => !t.tween.IsComplete()) ) {
                        return;
                    }

                    PreviewEndCall?.Invoke();
                };
            }
        }

        static void StartupGlobalPreview()
        {
            DOTweenEditorPreview.Start();
            UnityEditor.EditorApplication.playModeStateChanged += StopAllPreviews;
        }

        static void StopAllPreviews(PlayModeStateChange state)
        {
            Stop();
        }

        public static void Stop()
        {
            if(curPreviewProperty == null)
                return;

            DOTweenEditorPreview.Stop();
            curPreviewProperty = null;
            PreviewEndCall = null;
            UnityEditor.EditorApplication.playModeStateChanged -= StopAllPreviews;

            for (int i = preivewTweens.Count - 1; i >= 0; i--)
            {
                var tInfo = preivewTweens[i];
                if (tInfo.isFrom) {
                    int totLoops = tInfo.tween.Loops();
                    if (totLoops < 0 || totLoops > 1) {
                        tInfo.tween.Goto(tInfo.tween.Duration(false));
                    } else tInfo.tween.Complete();
                } else tInfo.tween.Rewind();
                tInfo.tween.Kill();
            }
            preivewTweens.Clear();
        }

        static Transform PreviewComponent(SerializedProperty property, out BaseCombine combine) {
            var previewComponent = property.serializedObject.targetObject as DTAnimation;
            combine = previewComponent.GetEnabledCombine(DTAnimationEditor.selectIndex);
            return previewComponent.transform;
        }

        public static bool IsInPreview()
        {
            return curPreviewProperty != null;
        }
    }
}

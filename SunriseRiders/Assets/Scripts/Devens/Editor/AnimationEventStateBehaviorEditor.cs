using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Devens.Editor
{
    [CustomEditor(typeof(AnimationEventStateBehavior))]
    public class AnimationEventStateBehaviorEditor : UnityEditor.Editor
    {
        private AnimationClip previewClip;
        private float previewTime;
        private bool isPreviewing;

        [MenuItem("GameObject/Enforce T-Pose", false, 0)]
        public static void EnforceTPose()
        {
            GameObject selected = Selection.activeGameObject;
            if (!selected || !selected.TryGetComponent(out Animator animator) || !animator.avatar)
            {
                return;
            }

            SkeletonBone[] skeletonBones = animator.avatar.humanDescription.skeleton;

            foreach (HumanBodyBones hbb in Enum.GetValues(typeof(HumanBodyBones)))
            {
                if (hbb == HumanBodyBones.LastBone)
                {
                    continue;
                }

                Transform boneTransform = animator.GetBoneTransform(hbb);

                if (!boneTransform)
                {
                    continue;
                }

                SkeletonBone skeletonBone = skeletonBones.FirstOrDefault(sb => sb.name == boneTransform.name);
                if (skeletonBone.name == null)
                {
                    continue;
                }

                if (hbb == HumanBodyBones.Hips)
                {
                    boneTransform.localPosition = skeletonBone.position;
                }

                boneTransform.localRotation = skeletonBone.rotation;
            }

            Debug.Log("T-Pose enforced successfully on " + selected.name);
        }
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var stateBehavior = (AnimationEventStateBehavior) target;

            if (Validate(stateBehavior, out string errorMessage))
            {
                GUILayout.Space(10);

                if (isPreviewing)
                {
                    if (GUILayout.Button("Stop Preview"))
                    {
                        EnforceTPose();
                        isPreviewing = false;
                    }
                    else
                    {
                        PreviewAnimationClip(stateBehavior); 
                    }
                }
                else if (GUILayout.Button("Preview"))
                {
                    isPreviewing = true;
                }
                

                GUILayout.Label($"Previewing at {previewTime:F2}s", EditorStyles.helpBox);
            }
            else
            {
                EditorGUILayout.HelpBox(errorMessage, MessageType.Info);
            }
        }

        bool Validate(AnimationEventStateBehavior stateBehavior, out string errorMessage)
        {
            AnimatorController animatorController = GetValidAnimatorController(out errorMessage);
            if (animatorController == null)
            {
                return false;
            }

            var matchingState = animatorController.layers
                .SelectMany(layer => layer.stateMachine.states)
                .FirstOrDefault(state => state.state.behaviours.Contains(stateBehavior));
            
            previewClip = matchingState.state?.motion as AnimationClip;
            if (previewClip == null)
            {
                errorMessage = "No valid AnimationClip found for the current state.";
                return false;
            }

            return true;
        }
        
        AnimatorController GetValidAnimatorController(out string errorMessage)
        {
            errorMessage = string.Empty;
            var targetGameObject = Selection.activeGameObject;
            if (targetGameObject == null)
            {
                errorMessage = "Please select a GameObject with an Animator to preview.";
                return null;
            }

            var animator = targetGameObject.GetComponent<Animator>();
            if(animator == null)
            {
                errorMessage = "The selected GameObject does not have an Animator component";
                return null;
            }
            
            var animatorController = animator.runtimeAnimatorController as AnimatorController;
            if (animatorController == null)
            {
                errorMessage = "The selected Animator does not have a valid AnimatorController";
                return null;
            }

            return animatorController;
        }

        void PreviewAnimationClip(AnimationEventStateBehavior stateBehavior)
        {
            if (previewClip == null)
            {
                return;
            }

            previewTime = stateBehavior.triggerTime * previewClip.length;
            
            AnimationMode.StartAnimationMode();
            AnimationMode.SampleAnimationClip(Selection.activeGameObject, previewClip, previewTime);
            AnimationMode.StopAnimationMode();
        }
    }
}

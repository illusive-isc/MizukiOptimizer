using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.IKUSIAOverride
{
    [AddComponentMenu("")]
    public class IKUSIAOverrideCore : ScriptableObject
    {
        protected VRCAvatarDescriptor descriptor;
        protected AnimatorController paryi_FX;

        internal readonly List<string> Parameters = new();
        internal readonly List<string> menuPath = new();
        internal readonly List<string> delPath = new();
        internal readonly List<string> Layers = new();

        public static void EditorOnly(Transform obj)
        {
            if (obj)
            {
                EditorOnly(obj.gameObject);
            }
        }

        public static void EditorOnly(GameObject obj)
        {
            if (obj)
            {
                obj.tag = "EditorOnly";
            }
        }

        public static void DestroyObj(Transform obj)
        {
            if (obj)
            {
                DestroyObj(obj.gameObject);
            }
        }

        public static void DestroyObj(GameObject obj)
        {
            if (obj)
            {
                DestroyImmediate(obj);
            }
        }

        public static void DestroyComponent<T>(Transform obj)
            where T : Component
        {
            if (obj)
            {
                DestroyImmediate(obj.GetComponent<T>());
            }
        }

        public void ExeDestroyObj(Transform obj)
        {
            if (obj)
            {
                DestroyObj(obj);
            }
        }

        public static void SetWeight(SkinnedMeshRenderer obj, string weightName, float weight)
        {
            if (obj)
            {
                var blendShapeIndex = obj.sharedMesh.GetBlendShapeIndex(weightName);
                if (blendShapeIndex != -1)
                {
                    obj.SetBlendShapeWeight(blendShapeIndex, weight);
                }
            }
        }

        protected bool CheckBT(Motion motion, List<string> strings)
        {
            if (motion is BlendTree blendTree)
            {
                return !strings.Contains(blendTree.blendParameter);
            }
            else
            {
                return false;
            }
        }

        protected void DeleteFxBT(List<string> Parameters)
        {
            foreach (var layer in paryi_FX.layers.Where(layer => layer.name == "MainCtrlTree"))
            {
                foreach (var state in layer.stateMachine.states)
                {
                    if (state.state.motion is BlendTree blendTree)
                    {
                        blendTree.children = blendTree
                            .children.Where(c => CheckBT(c.motion, Parameters))
                            .ToArray();
                    }
                }
            }
        }

        protected void EditVRCExpressions(VRCExpressionsMenu menu, List<string> menuPath)
        {
            if (menu == null || menuPath == null || menuPath.Count == 0)
                return;
            RemoveMenuItemRecursivelyInternal(menu, menuPath, 0);
        }

        protected bool RemoveMenuItemRecursivelyInternal(
            VRCExpressionsMenu menu,
            List<string> menuPath,
            int currentDepth
        )
        {
            if (currentDepth >= menuPath.Count)
                return false;

            var targetName = menuPath[currentDepth];

            for (int i = menu.controls.Count - 1; i >= 0; i--)
            {
                var control = menu.controls[i];

                if (control.name == targetName)
                {
                    if (currentDepth == menuPath.Count - 1)
                    {
                        menu.controls.RemoveAt(i);
                        return true;
                    }
                    else if (control.subMenu != null)
                    {
                        return RemoveMenuItemRecursivelyInternal(
                            control.subMenu,
                            menuPath,
                            currentDepth + 1
                        );
                    }
                }
            }

            return false;
        }

        protected void Initialize(VRCAvatarDescriptor descriptor, AnimatorController paryi_FX)
        {
            this.descriptor = descriptor;
            this.paryi_FX = paryi_FX;
        }

        protected void ChangeObj(List<string> delPath)
        {
            foreach (var path in delPath)
                DestroyObj(descriptor.transform.Find(path));
        }

        protected void DeleteFx(List<string> Layers)
        {
            paryi_FX.layers = paryi_FX
                .layers.Where(layer => !Layers.Contains(layer.name))
                .ToArray();
        }

        protected void RemoveStatesAndTransitions(
            AnimatorStateMachine stateMachine,
            params AnimatorState[] statesToRemove
        )
        {
            // Remove transitions leading to states to be removed
            foreach (var state in stateMachine.states)
            {
                state.state.transitions = state
                    .state.transitions.Where(t =>
                        t.destinationState == null || !statesToRemove.Contains(t.destinationState)
                    )
                    .ToArray();
            }
            // Remove AnyState transitions leading to states to be removed
            stateMachine.anyStateTransitions = stateMachine
                .anyStateTransitions.Where(t =>
                    t.destinationState == null || !statesToRemove.Contains(t.destinationState)
                )
                .ToArray();
            // Filter out states to keep
            stateMachine.states = stateMachine
                .states.Where(s => !statesToRemove.Contains(s.state))
                .ToArray();
        }
    }
}
#endif

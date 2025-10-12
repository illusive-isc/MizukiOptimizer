using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.MizukiOptimizer
{
    [AddComponentMenu("")]
    internal class MizukiOptimizerBase : ScriptableObject
    {
        HashSet<string> paramList = new();
        protected VRCAvatarDescriptor descriptor;
        protected AnimatorController animator;
        protected static List<string> exsistParams = new() { "TRUE", "paryi_AFK" };

        internal readonly List<string> Parameters = new();
        internal readonly List<string> menuPath = new();
        internal readonly List<string> delPath = new();
        internal readonly List<string> Layers = new();
        protected static readonly List<string> VRCParameters = new()
        {
            "IsLocal",
            "PreviewMode",
            "Viseme",
            "Voice",
            "GestureLeft",
            "GestureRight",
            "GestureLeftWeight",
            "GestureRightWeight",
            "AngularY",
            "VelocityX",
            "VelocityY",
            "VelocityZ",
            "VelocityMagnitude",
            "Upright",
            "Grounded",
            "Seated",
            "AFK",
            "TrackingType",
            "VRMode",
            "MuteSelf",
            "InStation",
            "Earmuffs",
            "IsOnFriendsList",
            "AvatarVersion",
        };

        protected void AddIfNotInParameters(
            HashSet<string> paramList,
            List<string> exeistParams,
            string parameter,
            bool isActive = true
        )
        {
            if (isActive && !VRCParameters.Contains(parameter) && !exeistParams.Contains(parameter))
            {
                paramList.Add(parameter);
            }
        }

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

        protected void DeleteParam(List<string> Parameters)
        {
            animator.parameters = animator
                .parameters.Where(parameter => !Parameters.Contains(parameter.name))
                .ToArray();
        }

        protected void DeleteFxBT(List<string> Parameters)
        {
            foreach (var layer in animator.layers.Where(layer => layer.name == "MainCtrlTree"))
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

        protected void DeleteVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param,
            List<string> Parameters,
            List<string> menuPath
        )
        {
            if (menu == null || menuPath == null || menuPath.Count == 0)
                return;
            // パラメーターの削除
            param.parameters = param
                .parameters.Where(parameter => !Parameters.Contains(parameter.name))
                .ToArray();
            RemoveMenuItemRecursivelyInternal(menu, menuPath, 0);
        }

        private bool RemoveMenuItemRecursivelyInternal(
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

        protected void Initialize(VRCAvatarDescriptor descriptor, AnimatorController animator)
        {
            this.descriptor = descriptor;
            this.animator = animator;
        }

        protected void ChangeObj(List<string> delPath)
        {
            foreach (var path in delPath)
                DestroyObj(descriptor.transform.Find(path));
        }

        protected void DeleteFx(List<string> Layers)
        {
            animator.layers = animator
                .layers.Where(layer => !Layers.Contains(layer.name))
                .ToArray();
        }
    }
}
#endif

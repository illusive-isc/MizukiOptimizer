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
    internal class PenCtrl : Utils
    {
        HashSet<string> paramList = new();
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;
        private static readonly List<string> Layers = new() { "PenCtrl_R", "PenCtrl_L" };

        private static readonly List<string> Parameters = new()
        {
            "PenColor",
            "Pen1",
            "Pen1Grab",
            "Pen2",
            "Pen2Grab",
        };

        public PenCtrl Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator,
            MizukiOptimizer optimizer
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            return this;
        }

        public PenCtrl DeleteFx()
        {
            animator.layers = animator
                .layers.Where(layer => !Layers.Contains(layer.name))
                .ToArray();

            return this;
        }

        public PenCtrl DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !paramList.Contains(parameter.name))
                .ToArray();
            animator.parameters = animator
                .parameters.Where(parameter => !Parameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public PenCtrl DeleteFxBT()
        {
            foreach (var layer in animator.layers.Where(layer => layer.name == "MainCtrlTree"))
            {
                foreach (var state in layer.stateMachine.states)
                {
                    if (state.state.motion is BlendTree blendTree)
                    {
                        blendTree.children = blendTree
                            .children.Where(c =>
                                CheckBT(c.motion, paramList.Concat(Parameters).ToList())
                            )
                            .ToArray();
                    }
                }
            }
            return this;
        }

        public PenCtrl DeleteVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            param.parameters = param
                .parameters.Where(parameter =>
                    !paramList
                        .Where(obj =>
                            !(obj == "HeartGunCollider R") || !(obj == "HeartGunCollider L")
                        )
                        .Concat(Parameters)
                        .Contains(parameter.name)
                )
                .ToArray();

            foreach (var control in menu.controls)
            {
                if (control.name == "Particle")
                {
                    var expressionsSubMenu = control.subMenu;

                    foreach (var control2 in expressionsSubMenu.controls)
                    {
                        if (control2.name == "Pen")
                        {
                            expressionsSubMenu.controls.Remove(control2);
                            break;
                        }
                    }
                    control.subMenu = expressionsSubMenu;
                    break;
                }
            }
            return this;
        }

        public PenCtrl ChangeObj()
        {
            DestroyObj(descriptor.transform.Find("Advanced/Particle/7"));
            DestroyObj(descriptor.transform.Find("Advanced/Constraint/Index_R_Constraint"));
            DestroyObj(descriptor.transform.Find("Advanced/Constraint/Index_L_Constraint"));
            DestroyObj(descriptor.transform.Find("Advanced/Constraint/Hand_R_Constraint0"));
            DestroyObj(descriptor.transform.Find("Advanced/Constraint/Hand_L_Constraint0"));
            return this;
        }
    }
}
#endif

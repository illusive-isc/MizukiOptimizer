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
    internal class FreeParticle : Utils
    {
        HashSet<string> paramList = new();
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;

        private static readonly List<string> Parameters = new()
        {
            "Paricle8_1",
            "Paricle8_2",
            "Paricle8_3",
            "Paricle8_4",
            "Paricle8_5",
            "Paricle8_6",
            "Paricle8_7",
            "Paricle8_8",
        };

        public FreeParticle Initialize(VRCAvatarDescriptor descriptor, AnimatorController animator)
        {
            this.descriptor = descriptor;
            this.animator = animator;
            return this;
        }

        public FreeParticle DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !paramList.Contains(parameter.name))
                .ToArray();
            animator.parameters = animator
                .parameters.Where(parameter => !Parameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public FreeParticle DeleteFxBT()
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

        public FreeParticle DeleteVRCExpressions(VRCExpressionsMenu menu, VRCExpressionParameters param)
        {
            param.parameters = param
                .parameters.Where(parameter =>
                    !paramList.Concat(Parameters).Contains(parameter.name)
                )
                .ToArray();

            foreach (var control in menu.controls)
            {
                if (control.name == "Particle")
                {
                    var expressionsSubMenu = control.subMenu;

                    foreach (var control2 in expressionsSubMenu.controls)
                    {
                        if (control2.name == "Particle Free")
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

        public FreeParticle ChangeObj()
        {
            DestroyObj(descriptor.transform.Find("Particle"));
            return this;
        }
    }
}
#endif

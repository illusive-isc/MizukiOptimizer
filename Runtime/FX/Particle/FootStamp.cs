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
    internal class FootStamp : Utils
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;

        private static readonly List<string> Parameters = new() { "Particle4" };

        public FootStamp Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            return this;
        }

        public FootStamp DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !Parameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public FootStamp DeleteFxBT()
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
            return this;
        }

        public FootStamp DeleteVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            param.parameters = param
                .parameters.Where(parameter => !Parameters.Contains(parameter.name))
                .ToArray();

            foreach (var control in menu.controls)
            {
                if (control.name == "Particle")
                {
                    var expressionsSubMenu = control.subMenu;

                    foreach (var control2 in expressionsSubMenu.controls)
                    {
                        if (control2.name == "Foot_stamp")
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

        public FootStamp ChangeObj()
        {
            DestroyObj(descriptor.transform.Find("Advanced/Particle/4"));
            return this;
        }
    }
}
#endif

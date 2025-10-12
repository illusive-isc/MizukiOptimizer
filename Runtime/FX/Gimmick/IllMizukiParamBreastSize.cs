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
    internal class IllMizukiParamBreastSize : IllMizukiUtils
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;

        bool breastSizeFlg1,
            breastSizeFlg2,
            breastSizeFlg3;

        private static readonly List<string> MenuParameters = new() { "BreastSize" };

        public IllMizukiParamBreastSize Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator,
            IllMizukiOptimizer optimizer
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            breastSizeFlg1 = optimizer.BreastSizeFlg1;
            breastSizeFlg2 = optimizer.BreastSizeFlg2;
            breastSizeFlg3 = optimizer.BreastSizeFlg3;
            return this;
        }

        public IllMizukiParamBreastSize DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllMizukiParamBreastSize DeleteFxBT()
        {
            foreach (var layer in animator.layers.Where(layer => layer.name == "MainCtrlTree"))
            {
                foreach (var state in layer.stateMachine.states)
                {
                    if (state.state.motion is BlendTree blendTree)
                    {
                        blendTree.children = blendTree
                            .children.Where(c => CheckBT(c.motion, MenuParameters))
                            .ToArray();
                    }
                }
            }
            return this;
        }

        public IllMizukiParamBreastSize DeleteVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            param.parameters = param
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();

            foreach (var control in menu.controls)
            {
                if (control.name == "Gimmick")
                {
                    var expressionsSubMenu = control.subMenu;

                    foreach (var control2 in expressionsSubMenu.controls)
                    {
                        if (control2.name == "Breast_size")
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

        public IllMizukiParamBreastSize ChangeObj()
        {
            var Body_b = descriptor.transform.Find("Body_b");
            if (Body_b)
                if (Body_b.TryGetComponent<SkinnedMeshRenderer>(out var Body_bSMR))
                {
                    SetWeight(Body_bSMR, "Breast_small_____胸_小", breastSizeFlg1 ? 100 : 0);
                    SetWeight(Body_bSMR, "Breast_Big_____胸_大", breastSizeFlg2 ? 50 : 0);
                    SetWeight(Body_bSMR, "Breast_Big_____胸_大", breastSizeFlg3 ? 100 : 0);
                }
            var maid = descriptor.transform.Find("Maid");
            if (maid)
                if (maid.TryGetComponent<SkinnedMeshRenderer>(out var maidSMR))
                {
                    SetWeight(maidSMR, "Breast_small_____胸_小", breastSizeFlg1 ? 100 : 0);
                    SetWeight(
                        maidSMR,
                        "Breast_Big_____胸_大",
                        breastSizeFlg2 ? 50
                            : breastSizeFlg3 ? 100
                            : 0
                    );
                }
            var outer = descriptor.transform.Find("Outer");
            if (outer)
                if (outer.TryGetComponent<SkinnedMeshRenderer>(out var outerSMR))
                {
                    SetWeight(outerSMR, "Breast_small_____胸_小", breastSizeFlg1 ? 100 : 0);
                    SetWeight(
                        outerSMR,
                        "Breast_Big_____胸_大",
                        breastSizeFlg2 ? 50
                            : breastSizeFlg3 ? 100
                            : 0
                    );
                }
            var jacket = descriptor.transform.Find("jacket");
            if (jacket)
                if (jacket.TryGetComponent<SkinnedMeshRenderer>(out var jacketSMR))
                {
                    jacketSMR.SetBlendShapeWeight(
                        1,
                        breastSizeFlg2 ? 100
                            : breastSizeFlg3 ? 200
                            : 0
                    );
                }
            return this;
        }
    }
}
#endif

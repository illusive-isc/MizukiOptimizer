using System.Collections.Generic;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class BreastSize : MizukiBase
    {
        bool breastSizeFlg1,
            breastSizeFlg2,
            breastSizeFlg3;

        internal static new readonly List<string> Parameters = new() { "BreastSize" };
        internal static new readonly List<string> menuPath = new() { "Gimmick2", "Breast_size" };

        internal void Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController paryi_FX,
            MizukiOptimizer optimizer
        )
        {
            this.descriptor = descriptor;
            this.paryi_FX = paryi_FX;
            breastSizeFlg1 = optimizer.BreastSizeFlg1;
            breastSizeFlg2 = optimizer.BreastSizeFlg2;
            breastSizeFlg3 = optimizer.BreastSizeFlg3;
        }

        protected new void DeleteFx(List<string> Layers)
        {
            DeleteBarCtrlHandHit(Parameters, "BreastSize");
            DeleteBarCtrl("BarOff", "BarOpen", "BreastSize");
        }

        internal new void ChangeObj(List<string> delPath)
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
        }
    }
}
#endif

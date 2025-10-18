using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
#if UNITY_EDITOR


namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class Shoes : MizukiBase
    {
        internal static new readonly List<string> Parameters = new() { "Object6" };

        internal static new readonly List<string> menuPath = new() { "Object", "shoes" };

        bool ArmAcceFlg2;

        internal void Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController paryi_FX,
            MizukiOptimizer optimizer
        )
        {
            this.descriptor = descriptor;
            this.paryi_FX = paryi_FX;
            ArmAcceFlg2 = optimizer.ArmAcceFlg2;
        }

        internal new void ChangeObj(List<string> delPath)
        {
            var maid = descriptor.transform.Find("Maid");

            if (maid)
                if (maid.TryGetComponent<SkinnedMeshRenderer>(out var maidSMR))
                {
                    SetWeight(maidSMR, "Shoes_off", ArmAcceFlg2 ? 0 : 100);
                }
        }
    }
}
#endif

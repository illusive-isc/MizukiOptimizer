using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
#if UNITY_EDITOR


namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class Eyemask : MizukiBase
    {
        internal static new readonly List<string> Parameters = new() { "OBJ7_6" };

        internal static new readonly List<string> menuPath = new()
        {
            "Object",
            "Head add",
            "eyemask",
        };

        bool EyemaskFlg2;

        internal void Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController paryi_FX,
            MizukiOptimizer optimizer
        )
        {
            this.descriptor = descriptor;
            this.paryi_FX = paryi_FX;
            EyemaskFlg2 = optimizer.EyemaskFlg2;
        }

        internal new void ChangeObj(List<string> delPath)
        {
            var maid = descriptor.transform.Find("Maid");

            if (maid)
                if (maid.TryGetComponent<SkinnedMeshRenderer>(out var maidSMR))
                {
                    SetWeight(maidSMR, "Eye mask_on", EyemaskFlg2 ? 100 : 0);
                }
        }
    }
}
#endif

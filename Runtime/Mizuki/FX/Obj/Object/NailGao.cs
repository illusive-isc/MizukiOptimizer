using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
#if UNITY_EDITOR


namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class NailGao : MizukiBase
    {
        bool NailGaoFlg2;

        internal void Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator,
            MizukiOptimizer optimizer
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            NailGaoFlg2 = optimizer.NailGaoFlg2;
        }

        internal static new readonly List<string> Parameters = new() { "Object1" };

        internal static new readonly List<string> menuPath = new() { "Object", "nail gao~" };

        internal new void ChangeObj(List<string> delPath)
        {
            var body_b = descriptor.transform.Find("Body_b");

            if (body_b)
                if (body_b.TryGetComponent<SkinnedMeshRenderer>(out var body_bSMR))
                {
                    SetWeight(body_bSMR, "Extend", NailGaoFlg2 ? 100 : 0);
                }
        }
    }
}
#endif

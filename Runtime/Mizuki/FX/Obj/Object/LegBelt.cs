using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
#if UNITY_EDITOR


namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class LegBelt : MizukiBase
    {
        internal static new readonly List<string> Parameters = new() { "Object5" };

        internal static new readonly List<string> menuPath = new() { "Object", "leg belt" };

        bool LegBeltFlg2;

        internal void Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController paryi_FX,
            MizukiOptimizer optimizer
        )
        {
            this.descriptor = descriptor;
            this.paryi_FX = paryi_FX;
            LegBeltFlg2 = optimizer.LegBeltFlg2;
        }

        internal new void ChangeObj(List<string> delPath)
        {
            if (LegBeltFlg2)
                DestroyObj(descriptor.transform.Find("leg-garter"));
        }
    }
}
#endif

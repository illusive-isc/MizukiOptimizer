using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
#if UNITY_EDITOR


namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class EarTail : MizukiBase
    {
        internal static new readonly List<string> Parameters = new() { "OBJ7_1" };

        internal static new readonly List<string> menuPath = new()
        {
            "Object",
            "Head add",
            "ear tail",
        };

        bool EarTailFlg2;

        internal void Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController paryi_FX,
            MizukiOptimizer optimizer
        )
        {
            this.descriptor = descriptor;
            this.paryi_FX = paryi_FX;
            EarTailFlg2 = optimizer.EarTailFlg2;
        }

        internal new readonly List<string> delPath = new()
        {
            "Armature/Hips/tail",
            "Armature/Hips/Spine/Chest/Neck/Head/TigerEar",
        };

        internal new void ChangeObj(List<string> delPath)
        {
            base.ChangeObj(delPath);
            if (EarTailFlg2)
                DestroyObj(descriptor.transform.Find("eartail"));
        }
    }
}
#endif

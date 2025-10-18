using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
#if UNITY_EDITOR


namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class Outer : MizukiBase
    {
        internal static new readonly List<string> Parameters = new() { "Object3" };

        internal static new readonly List<string> menuPath = new() { "Object", "outer" };

        bool OuterFlg2;

        internal void Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController paryi_FX,
            MizukiOptimizer optimizer
        )
        {
            this.descriptor = descriptor;
            this.paryi_FX = paryi_FX;
            OuterFlg2 = optimizer.OuterFlg2;
        }

        internal new readonly List<string> delPath = new()
        {
            "Outer",
            "Armature/Hips/Spine/Chest/Shoulder_R/Upperarm_R/Lowerarm_R/Right Hand/coat_hand_root_R",
            "Armature/Hips/Spine/Chest/Shoulder_L/Upperarm_L/Lowerarm_L/Left Hand/coat_hand_root_L",
        };

        internal new void ChangeObj(List<string> delPath)
        {
            if (OuterFlg2)
                base.ChangeObj(delPath);
        }
    }
}
#endif

using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
#if UNITY_EDITOR


namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class ClothDel : MizukiBase
    {
        bool ClothDelFlg2;

        internal void Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController paryi_FX,
            MizukiOptimizer optimizer
        )
        {
            this.descriptor = descriptor;
            this.paryi_FX = paryi_FX;
            ClothDelFlg2 = optimizer.ClothDelFlg2;
        }

        internal new readonly List<string> delPath = new()
        {
            "Maid",
            "knee-socks",
            "Armature/Hips/Skirt_Root",
            "Armature/Hips/Spine/Chest/Shoulder_Ribbon_BackRoot_L",
            "Armature/Hips/Spine/Chest/Shoulder_Ribbon_BackRoot_R",
            "Armature/Hips/Spine/Chest/Shoulder_L/Shoulder_Ribbon_FrontRoot_L",
            "Armature/Hips/Spine/Chest/Shoulder_R/Shoulder_Ribbon_FrontRoot_R",
            "Armature/Hips/Spine/Chest/Neck/Head/headband_Root",
            "Armature/Hips/Upperleg_R/Lowerleg_R/Leg_frills_Root_R",
            "Armature/Hips/Upperleg_L/Lowerleg_L/Leg_frills_Loot_L",
            "Armature/Hips/Spine/Chest/Shoulder_R/Upperarm_R/Lowerarm_R/Right Hand/Hand_frills_R",
            "Armature/Hips/Spine/Chest/Shoulder_L/Upperarm_L/Lowerarm_L/Left Hand/Hand_frills_L",
        };

        internal new void ChangeObj(List<string> delPath)
        {
            base.ChangeObj(delPath);
            var Body_b = descriptor.transform.Find("Body_b");

            if (Body_b)
                if (Body_b.TryGetComponent<SkinnedMeshRenderer>(out var Body_bSMR))
                {
                    SetWeight(Body_bSMR, "Knee socks_____ニーソ専用", 0);
                    SetWeight(Body_bSMR, "bra_off_____ブラジャー_off", ClothDelFlg2 ? 0 : 100);
                    SetWeight(Body_bSMR, "pants_off_____パンツ_off", ClothDelFlg2 ? 0 : 100);
                }
        }
    }
}
#endif

using System.Collections.Generic;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class FaceContact : MizukiBase
    {
        public bool kamitukiFlg = false;
        public bool nadeFlg = false;

        public void Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController paryi_FX,
            MizukiOptimizer optimizer
        )
        {
            this.descriptor = descriptor;
            this.paryi_FX = paryi_FX;
            kamitukiFlg = optimizer.kamitukiFlg;
            nadeFlg = optimizer.nadeFlg;
        }

        public new void DeleteFx(List<string> Layers)
        {
            if (nadeFlg)
                DeleteMenuButtonCtrl(new() { "NadeNade" });
            if (kamitukiFlg)
                DeleteMenuButtonCtrl(new() { "Gimmick2_5" });
        }

        protected new void DeleteFxBT(List<string> Parameters)
        {
            if (nadeFlg)
                base.DeleteFxBT(new() { "NadeNade" });
            if (kamitukiFlg)
                base.DeleteFxBT(new() { "Gimmick2_5" });
        }

        public new void EditVRCExpressions(VRCExpressionsMenu menu, List<string> menuPath)
        {
            if (nadeFlg)
                RemoveMenuItemRecursivelyInternal(
                    menu,
                    new() { "Gimmick2", "Gesture_change", "NadeNade" },
                    0
                );
            if (kamitukiFlg)
                RemoveMenuItemRecursivelyInternal(menu, new() { "Gimmick2", "噛みつき禁止" }, 0);
        }

        protected new void ChangeObj(List<string> delPath)
        {
            if (nadeFlg)
                descriptor.transform.Find("Advanced/Gimmick2/Face2").gameObject.SetActive(true);

            if (kamitukiFlg)
                descriptor.transform.Find("Advanced/Gimmick2/3").gameObject.SetActive(true);
        }
    }
}
#endif

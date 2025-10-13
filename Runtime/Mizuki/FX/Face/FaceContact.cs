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
            AnimatorController animator,
            MizukiOptimizer optimizer
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
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

        public new void EditVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param,
            List<string> Parameters,
            List<string> menuPath
        )
        {
            foreach (var parameter in param.parameters)
            {
                if (parameter.name is "NadeNade" && nadeFlg)
                {
                    parameter.defaultValue = 1;
                    parameter.networkSynced = false;
                    RemoveMenuItemRecursivelyInternal(
                        menu,
                        new() { "Gimmick2", "Gesture_change", "NadeNade" },
                        0
                    );
                }
                if (parameter.name is "Gimmick2_5" && kamitukiFlg)
                {
                    parameter.defaultValue = 1;
                    parameter.networkSynced = false;
                    RemoveMenuItemRecursivelyInternal(
                        menu,
                        new() { "Gimmick2", "噛みつき禁止" },
                        0
                    );
                }
            }
        }
    }
}
#endif

using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
#if UNITY_EDITOR


namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class Accesary : MizukiBase
    {
        internal static new readonly List<string> Parameters = new() { "OBJ7_8" };

        internal static new readonly List<string> menuPath = new()
        {
            "Object",
            "Head add",
            "accesary",
        };

        bool AccesaryFlg2;

        internal void Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController paryi_FX,
            MizukiOptimizer optimizer
        )
        {
            this.descriptor = descriptor;
            this.paryi_FX = paryi_FX;
            AccesaryFlg2 = optimizer.AccesaryFlg2;
        }

        internal new void ChangeObj(List<string> delPath)
        {
            if (AccesaryFlg2)
                DestroyObj(descriptor.transform.Find("Add-Ribbon"));
        }
    }
}
#endif

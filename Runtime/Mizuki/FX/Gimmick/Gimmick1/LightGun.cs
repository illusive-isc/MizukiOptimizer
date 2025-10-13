using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
#if UNITY_EDITOR


namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class LightGun : MizukiBase
    {
        internal static new readonly List<string> Layers = new() { "butterfly" };

        internal static new readonly List<string> Parameters = new()
        {
            "LightGun",
            "LightColor",
            "LightStrength",
            "butterfly_Set",
            "butterfly_Shot",
            "butterfly_stand",
            "butterfly_FingerIndexL",
            "butterfly_Gesture_Set",
        };
        internal static new readonly List<string> menuPath = new() { "Gimmick", "Light_Gun" };
        internal static new readonly List<string> delPath = new() { "Advanced/butterfly" };

        protected new void DeleteFx(List<string> Layers)
        {
            base.DeleteFx(Layers);
            DeleteBarCtrlHandHit(Parameters, "LightRange", "LightStrength");
        }
    }
}
#endif

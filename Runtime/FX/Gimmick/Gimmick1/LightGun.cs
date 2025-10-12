using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.MizukiOptimizer
{
    [AddComponentMenu("")]
    internal class LightGun : MizukiOptimizerBase
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
    }
}
#endif

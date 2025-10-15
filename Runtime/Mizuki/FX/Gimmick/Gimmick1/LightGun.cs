using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
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
        internal static new readonly List<string> delPath = new() { "Advanced/butterfly" };
        internal static readonly List<List<string>> menuPathList = new()
        {
            new() { "Gimmick", "Light_Gun", "Light_Gun_On" },
            new() { "Gimmick", "Light_Gun", "Light_Gun_Option" },
            new() { "Gimmick", "Light_Gun", "Light_color" },
            new() { "Gimmick", "Light_Gun", "Light strength" },
            new() { "Gimmick", "Light_Gun" },
        };

        public new void EditVRCExpressions(VRCExpressionsMenu menu, List<string> menuPath)
        {
            foreach (var item in menuPathList)
                base.EditVRCExpressions(menu, item);
        }

        protected new void DeleteFx(List<string> Layers)
        {
            base.DeleteFx(Layers);
            DeleteBarCtrlHandHit(Parameters, "LightRange", "LightStrength");
            DeleteBarCtrl(
                "BarOff 0 0 0 0 0 0",
                "BarOpen 0 0 0 0 0 0",
                "LightColor",
                "BarOpen 0 0 0 0 0 0 1",
                "BarOff 0 0 0 0 0 0 0",
                "BarOpen 0 0 0 0 0 0 0",
                "LightStrength"
            );
        }
    }
}
#endif

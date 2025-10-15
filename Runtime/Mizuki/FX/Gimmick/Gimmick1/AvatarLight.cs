using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR


namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class AvatarLight : MizukiBase
    {
        internal static new readonly List<string> Parameters = new() { "AvatarLightStrength" };
        internal static new readonly List<string> menuPath = new() { "Gimmick", "Avatar_Light" };

        protected new void DeleteFx(List<string> Layers)
        {
            DeleteBarCtrlHandHit(Parameters, "AvatarLightStrength");
            DeleteBarCtrl("BarOff 0 0", "BarOpen 0 0", "AvatarLightStrength", "Color 0");
        }
    }
}
#endif

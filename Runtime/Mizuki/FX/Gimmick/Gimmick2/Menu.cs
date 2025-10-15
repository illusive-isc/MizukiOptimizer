using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class Menu : MizukiBase
    {
        internal static new readonly List<string> Layers = new()
        {
            "MainKeyCtrl",
            "MainIntCtrl",
            "MainCtrl",
            "MenuButtonCtrl",
            "MenuOpenGestureCtrl",
            "BarCtrl",
            "BarCtrlHandHit",
        };
        internal static new readonly List<string> Parameters = new()
        {
            "Paryi_MenuMainInt",
            "MenuButtonGlobalToggle",
            "MenuStart",
            "BarType",
            "Voice",
            "Viseme",
        };
        internal static new readonly List<string> menuPath = new() { "Gimmick2", "MenuDesktop" };
        internal static new readonly List<string> delPath = new() { "MenuGrabWorld", "Menu" };
    }
}
#endif

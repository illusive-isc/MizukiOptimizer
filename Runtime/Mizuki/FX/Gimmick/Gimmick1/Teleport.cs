using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR


namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class Teleport : MizukiBase
    {
        internal static new readonly List<string> Parameters = new() { "warpA", "warpB" };
        internal static new readonly List<string> delPath = new() { "Advanced/Teleport_World" };

        protected new void EditVRCExpressions(VRCExpressionsMenu menu, List<string> menuPath)
        {
            if (menu == null || menuPath == null)
                return;
            RemoveMenuItemRecursivelyInternal(menu, new() { "Gimmick", "WarpA" }, 0);
            RemoveMenuItemRecursivelyInternal(menu, new() { "Gimmick", "WarpB" }, 0);
        }
    }
}
#endif

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR


namespace jp.illusive_isc.MizukiOptimizer
{
    [AddComponentMenu("")]
    internal class Teleport : MizukiOptimizerBase
    {
        internal static new readonly List<string> Parameters = new() { "warpA", "warpB" };
        internal static new readonly List<string> delPath = new() { "Advanced/Teleport_World" };

        protected new void DeleteVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param,
            List<string> Parameters,
            List<string> menuPath
        )
        {
            if (menu == null || menuPath == null)
                return;
            // パラメーターの削除
            param.parameters = param
                .parameters.Where(parameter => !Parameters.Contains(parameter.name))
                .ToArray();
            RemoveMenuItemRecursivelyInternal(menu, new() { "Gimmick", "WarpA" }, 0);
            RemoveMenuItemRecursivelyInternal(menu, new() { "Gimmick", "WarpB" }, 0);
        }
    }
}
#endif

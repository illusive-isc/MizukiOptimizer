using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.MizukiOptimizer
{
    [AddComponentMenu("")]
    internal class Collider : MizukiOptimizerBase
    {
        internal static new readonly List<string> Layers = new() { "ColliderCtrl" };

        internal static new readonly List<string> Parameters = new()
        {
            "ColliderON",
            "SpeedCollider",
            "JumpCollider",
        };

        internal static new readonly List<string> menuPath = new() { "Jump&Dash" };
        internal static new readonly List<string> delPath = new()
        {
            "Advanced/Gimmick1/JUMP",
            "Advanced/Gimmick1/SPEED",
            "Advanced/Gimmick1/JUMP",
            "Advanced/Gimmick1/SPEED",
        };
    }
}
#endif

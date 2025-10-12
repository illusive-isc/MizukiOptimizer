using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.MizukiOptimizer
{
    [AddComponentMenu("")]
    internal class HandTrail : MizukiOptimizerBase
    {
        internal static new readonly List<string> Parameters = new() { "Paricle2" };

        internal static new readonly List<string> menuPath = new() { "Particle", "Hand trail" };
        internal static new readonly List<string> delPath = new() { "Advanced/Particle/2" };
    }
}
#endif

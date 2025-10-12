using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.MizukiOptimizer
{
    [AddComponentMenu("")]
    internal class FootStamp : MizukiOptimizerBase
    {
        internal static new readonly List<string> Parameters = new() { "Particle4" };
        internal static new readonly List<string> menuPath = new() { "Particle", "Foot_stamp" };
        internal static new readonly List<string> delPath = new() { "Advanced/Particle/4" };
    }
}
#endif

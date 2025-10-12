using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.MizukiOptimizer
{
    [AddComponentMenu("")]
    internal class Status : MizukiOptimizerBase
    {
        internal static new readonly List<string> Parameters = new() { "ParticleStatus" };
        internal static new readonly List<string> menuPath = new() { "Particle", "Status" };
        internal static new readonly List<string> delPath = new() { "Advanced/Particle/6" };
    }
}
#endif

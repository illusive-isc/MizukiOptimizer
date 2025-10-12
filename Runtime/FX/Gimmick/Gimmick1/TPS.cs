using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.MizukiOptimizer
{
    [AddComponentMenu("")]
    internal class TPS : MizukiOptimizerBase
    {
        internal static new readonly List<string> Parameters = new() { "TPS" };
        internal static new readonly List<string> menuPath = new() { "Gimmick", "TPS" };
        internal static new readonly List<string> delPath = new() { "Advanced/TPS" };
    }
}
#endif

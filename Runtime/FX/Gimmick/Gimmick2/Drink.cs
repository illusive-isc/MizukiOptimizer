using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.MizukiOptimizer
{
    [AddComponentMenu("")]
    internal class Drink : MizukiOptimizerBase
    {
        internal static new readonly List<string> Parameters = new() { "Gimmick2_6", "Drinkhight" };

        internal static new readonly List<string> menuPath = new() { "Gimmick2", "Gimmick6" };
        internal static new readonly List<string> delPath = new() { "Advanced/Gimmick2/6" };
    }
}
#endif

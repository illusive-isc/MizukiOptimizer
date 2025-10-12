using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.MizukiOptimizer
{
    [AddComponentMenu("")]
    internal class Clairvoyance : MizukiOptimizerBase
    {
        internal static new readonly List<string> Parameters = new() { "clairvoyance" };

        internal static new readonly List<string> menuPath = new() { "Gimmick", "Clairvoyance" };
        internal static new readonly List<string> delPath = new() { "Advanced/clairvoyance" };
    }
}
#endif

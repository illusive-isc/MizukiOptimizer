using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class EightBit : MizukiBase
    {
        internal static new readonly List<string> Parameters = new() { "Particle5" };
        internal static new readonly List<string> menuPath = new() { "Particle", "EightBit" };
        internal static new readonly List<string> delPath = new() { "Advanced/Particle/5" };
    }
}
#endif

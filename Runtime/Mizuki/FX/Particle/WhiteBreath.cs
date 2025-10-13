using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class WhiteBreath : MizukiBase
    {
        internal static new readonly List<string> Parameters = new() { "Particle1" };
        internal static new readonly List<string> menuPath = new() { "Particle", "White_breath" };
        internal static new readonly List<string> delPath = new() { "Advanced/Particle/1" };
    }
}
#endif

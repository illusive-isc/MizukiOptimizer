using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class NailTrail : MizukiBase
    {
        internal static new readonly List<string> Parameters = new() { "Particle3" };

        internal static new readonly List<string> menuPath = new() { "Particle", "Nail_trail" };
        internal static new readonly List<string> delPath = new() { "Advanced/Particle/3" };
    }
}
#endif

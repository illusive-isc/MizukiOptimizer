using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class HandTrail : MizukiBase
    {
        internal static new readonly List<string> Parameters = new() { "Particle2" };

        internal static new readonly List<string> menuPath = new() { "Particle", "Hand trail" };
        internal static new readonly List<string> delPath = new() { "Advanced/Particle/2" };
    }
}
#endif

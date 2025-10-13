using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class Status : MizukiBase
    {
        internal static new readonly List<string> Parameters = new() { "ParticleStatus" };
        internal static new readonly List<string> menuPath = new() { "Particle", "Status" };
        internal static new readonly List<string> delPath = new() { "Advanced/Particle/6" };
    }
}
#endif

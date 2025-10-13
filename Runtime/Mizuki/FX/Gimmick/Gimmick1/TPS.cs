using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class TPS : MizukiBase
    {
        internal static new readonly List<string> Parameters = new() { "TPS" };
        internal static new readonly List<string> menuPath = new() { "Gimmick", "TPS" };
        internal static new readonly List<string> delPath = new() { "Advanced/TPS" };
    }
}
#endif

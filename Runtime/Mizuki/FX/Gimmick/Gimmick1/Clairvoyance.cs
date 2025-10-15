#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class Clairvoyance : MizukiBase
    {
        internal static new readonly List<string> Parameters = new() { "clairvoyance" };

        internal static new readonly List<string> menuPath = new() { "Gimmick", "Clairvoyance" };
        internal static new readonly List<string> delPath = new() { "Advanced/clairvoyance" };
    }
}
#endif

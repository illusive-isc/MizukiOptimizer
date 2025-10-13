using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class FaceEffect : MizukiBase
    {
        internal static new readonly List<string> Parameters = new() { "faceeffect" };

        internal static new readonly List<string> menuPath = new() { "Gimmick2", "Gesture_change", "faceeffect" };
        internal static new readonly List<string> delPath = new() { "Advanced/FaceEffect" };
    }
}
#endif

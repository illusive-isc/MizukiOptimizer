using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class CameraPicture : MizukiBase
    {
        internal static new readonly List<string> Layers = new() { "CameraPicture" };

        internal static new readonly List<string> Parameters = new() { "Gimmick1_7", "CameraPictureDistance" };
        internal static new readonly List<string> menuPath = new() { "Gimmick", "Camera" };
        internal static new readonly List<string> delPath = new() { "Advanced/CameraPictureWorld" };
    }
}
#endif

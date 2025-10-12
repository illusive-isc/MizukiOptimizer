using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR


namespace jp.illusive_isc.MizukiOptimizer
{
    [AddComponentMenu("")]
    internal class AvatarLight : MizukiOptimizerBase
    {
        internal static new readonly List<string> Parameters = new() { "AvatarLightStrength" };
        internal static new readonly List<string> menuPath = new() { "Gimmick", "Avatar_Light" };
    }
}
#endif

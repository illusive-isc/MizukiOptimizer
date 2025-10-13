using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class FreeParticle : MizukiBase
    {
        internal static new readonly List<string> Parameters = new()
        {
            "Paricle8_1",
            "Paricle8_2",
            "Paricle8_3",
            "Paricle8_4",
            "Paricle8_5",
            "Paricle8_6",
            "Paricle8_7",
            "Paricle8_8",
        };

        internal static new readonly List<string> menuPath = new() { "Particle", "Particle Free" };
        internal static new readonly List<string> delPath = new() { "Particle" };
    }
}
#endif

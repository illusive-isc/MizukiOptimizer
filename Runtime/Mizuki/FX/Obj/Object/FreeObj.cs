using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class FreeObj : MizukiBase
    {
        internal static new readonly List<string> Parameters = new()
        {
            "OBJ8_1",
            "OBJ8_2",
            "OBJ8_3",
            "OBJ8_4",
            "OBJ8_5",
            "OBJ8_6",
            "OBJ8_7",
            "OBJ8_8",
        };

        internal static new readonly List<string> menuPath = new() { "Object", "Object Free" };
        internal static new readonly List<string> delPath = new() { "Object" };
    }
}
#endif

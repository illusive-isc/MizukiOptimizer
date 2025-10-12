using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.MizukiOptimizer
{
    [AddComponentMenu("")]
    internal class FreeGimmick : MizukiOptimizerBase
    {
        internal static new readonly List<string> Parameters = new()
        {
            "Gimmick2_8_1",
            "Gimmick2_8_2",
            "Gimmick2_8_3",
            "Gimmick2_8_4",
            "Gimmick2_8_5",
            "Gimmick2_8_6",
            "Gimmick2_8_7",
            "Gimmick2_8_8",
        };

        internal static new readonly List<string> menuPath = new() { "Gimmick2", "Free" };
        internal static new readonly List<string> delPath = new() { "Gimmick2" };
    }
}
#endif

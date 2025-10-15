using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class Drink : MizukiBase
    {
        internal static new readonly List<string> Parameters = new()
        {
            "Gimmick2_6",
            "Drinkhight",
            "Drinkmouth",
            "DrinkReset",
        };

        internal static new readonly List<string> menuPath = new() { "Gimmick2", "Gimmick6" };
        internal static new readonly List<string> delPath = new() { "Advanced/Gimmick2/6" };

        public new void DeleteFx(List<string> Layers)
        {
            paryi_FX
                .layers.Where(layer => layer.name == "LipSynk")
                .ToList()
                .ForEach(layer =>
                {
                    var delList = new List<string>()
                    {
                        "mouse0 0 0 0",
                        "Drinkhight1",
                        "mouse0 0 1",
                    };

                    RemoveStatesAndTransitions(
                        layer.stateMachine,
                        layer
                            .stateMachine.states.Where(state => delList.Contains(state.state.name))
                            .Select(state => state.state)
                            .ToArray()
                    );
                });
        }
    }
}
#endif

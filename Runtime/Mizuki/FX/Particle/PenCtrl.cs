using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class PenCtrl : MizukiBase
    {
        internal static new readonly List<string> Layers = new() { "PenCtrl_R", "PenCtrl_L" };

        internal static new readonly List<string> Parameters = new()
        {
            "PenColor",
            "Pen1",
            "Pen1Grab",
            "Pen2",
            "Pen2Grab",
        };

        internal static new readonly List<string> menuPath = new() { "Particle", "Pen" };
        internal static new readonly List<string> delPath = new()
        {
            "Advanced/Particle/7",
            "Advanced/Constraint/Index_R_Constraint",
            "Advanced/Constraint/Index_L_Constraint",
            "Advanced/Constraint/Hand_R_Constraint0",
            "Advanced/Constraint/Hand_L_Constraint0",
        };

        protected new void DeleteFx(List<string> Layers)
        {
            base.DeleteFx(Layers);
            DeleteBarCtrlHandHit(Parameters, "PenColor");
        }
    }
}
#endif

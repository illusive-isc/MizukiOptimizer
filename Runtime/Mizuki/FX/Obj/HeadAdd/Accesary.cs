using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class Accesary : MizukiBase
    {
        internal static new readonly List<string> Parameters = new() { "OBJ7_8" };

        internal static new readonly List<string> menuPath = new()
        {
            "Object",
            "Head add",
            "accesary",
        };

        // internal new void ChangeObj(List<string> delPath)
        // {
        //     var body_b = descriptor.transform.Find("Body_b");
        //     if (body_b)
        //         if (body_b.TryGetComponent<SkinnedMeshRenderer>(out var body_bSMR))
        //         {
        //             SetWeight(
        //                 body_bSMR,
        //                 "Foot_heel_OFF_____足_ヒールオフ",
        //                 heelFlg1 || heelFlg2 ? 0 : 100
        //             );
        //         }
        // }
    }
}
#endif

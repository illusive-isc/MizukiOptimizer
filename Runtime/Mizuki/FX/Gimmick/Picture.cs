using System.Collections.Generic;
using UnityEngine;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR


namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class Picture : MizukiBase
    {
        internal static new readonly List<string> Parameters = new()
        {
            "cameraLight&eyeLookHide",
            "LightCamera",
            "eyeLook",
        };
        internal static new readonly List<string> delPath = new()
        {
            "Advanced/LookOBJHead",
            "Advanced/CametaLightOBJ_World",
        };

        internal static readonly List<List<string>> menuPathList = new()
        {
            new() { "Gimmick", "Light_Gun", "Light_camera_on" },
            new() { "Gimmick", "Light_Gun", "eyeLook" },
            new() { "Gimmick", "Light_Gun", "cameraLight&eyeLookHide" },
        };

        public new void EditVRCExpressions(VRCExpressionsMenu menu, List<string> menuPath)
        {
            foreach (var item in menuPathList)
                base.EditVRCExpressions(menu, item);
        }
    }
}
#endif

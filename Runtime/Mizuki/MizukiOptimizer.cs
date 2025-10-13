using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("MizukiOptimizer")]
    public partial class MizukiOptimizer : IKUSIAOverrideAbstract
    {
        string pathDirPrefix = "Assets/MizukiOptimizer/";

        protected override long InitializeAssets(VRCAvatarDescriptor descriptor)
        {
            var step1 = Stopwatch.StartNew();
            pathDir = pathDirPrefix + descriptor.gameObject.name + pathDirSuffix;
            if (AssetDatabase.LoadAssetAtPath<AnimatorController>(pathDir + pathName) != null)
            {
                AssetDatabase.DeleteAsset(pathDir + pathName);
                AssetDatabase.DeleteAsset(pathDir + "Menu");
                AssetDatabase.DeleteAsset(pathDir + "paryi_paraments.asset");
            }
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }

            if (!controllerDef)
            {
                if (!descriptor.baseAnimationLayers[4].animatorController)
                    descriptor.baseAnimationLayers[4].animatorController =
                        AssetDatabase.LoadAssetAtPath<AnimatorController>(
                            AssetDatabase.GUIDToAssetPath("eabec4db12bc4574c996310914852639")
                        );
                controllerDef =
                    descriptor.baseAnimationLayers[4].animatorController as AnimatorController;
            }
            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(controllerDef), pathDir + pathName);

            controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(pathDir + pathName);

            if (!menuDef)
            {
                if (!descriptor.expressionsMenu)
                    descriptor.expressionsMenu = AssetDatabase.LoadAssetAtPath<VRCExpressionsMenu>(
                        AssetDatabase.GUIDToAssetPath("2e95f28830e406047b35e7e58b3c0e79")
                    );
                menuDef = descriptor.expressionsMenu;
            }

            var iconPath = pathDir + "/icon";
            if (!Directory.Exists(iconPath))
            {
                Directory.CreateDirectory(iconPath);
            }
            menu = DuplicateExpressionMenu(menuDef, pathDir, iconPath, questFlg1, textureResize);

            if (!paramDef)
            {
                if (!descriptor.expressionParameters)
                    descriptor.expressionParameters =
                        AssetDatabase.LoadAssetAtPath<VRCExpressionParameters>(
                            AssetDatabase.GUIDToAssetPath("ca37a7e2249e6404ea1893c197866705")
                        );
                paramDef = descriptor.expressionParameters;
                paramDef.name = descriptor.expressionParameters.name;
            }
            param = ScriptableObject.CreateInstance<VRCExpressionParameters>();
            EditorUtility.CopySerialized(paramDef, param);
            param.name = paramDef.name;
            EditorUtility.SetDirty(param);
            AssetDatabase.CreateAsset(param, pathDir + param.name + ".asset");
            step1.Stop();
            return step1.ElapsedMilliseconds;
        }

        protected override long Edit(VRCAvatarDescriptor descriptor)
        {
            var step2 = Stopwatch.StartNew();
            var body_b = descriptor.transform.Find("Body_b");
            if (body_b)
                if (body_b.TryGetComponent<SkinnedMeshRenderer>(out var body_bSMR))
                {
                    IKUSIAOverrideCore.SetWeight(
                        body_bSMR,
                        "Foot_heel_OFF_____足_ヒールオフ",
                        heelFlg1 || heelFlg2 ? 0 : 100
                    );
                    IKUSIAOverrideCore.SetWeight(
                        body_bSMR,
                        "Foot_Hiheel_____足_ハイヒール",
                        heelFlg2 ? 100 : 0
                    );
                }

            foreach (var config in GetParamConfigs(descriptor))
            {
                if (config.condition())
                    config.processAction();
            }

            if (IKUSIA_emote)
                foreach (var control in menu.controls)
                    if (control.name == "IKUSIA_emote")
                    {
                        menu.controls.Remove(control);
                        break;
                    }

            Edit4Quest(descriptor, this);

            step2.Stop();
            return step2.ElapsedMilliseconds;
        }
    }
}
#endif

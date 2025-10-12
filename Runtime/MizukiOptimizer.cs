using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
using VRC.SDKBase;
using Debug = UnityEngine.Debug;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.MizukiOptimizer
{
    [AddComponentMenu("MizukiOptimizer")]
    public partial class MizukiOptimizer : MonoBehaviour, IEditorOnly
    {
        string pathDirPrefix = "Assets/MizukiOptimizer/";
        string pathDirSuffix = "/FX/";
        string pathName = "paryi_FX.controller";

        public bool questFlg1 = false;

        public bool Skirt_Root;

        public bool Breast;

        public bool backhair;

        public bool back_side_root;

        public bool Head_002;

        public bool Front_hair2_root;

        public bool side_1_root;

        public bool hair_2;

        public bool sidehair;

        public bool side_3_root;

        public bool Side_root;

        public bool tail_044;

        public bool tail_022;

        public bool tail_024;

        public bool chest_collider1;
        public bool chest_collider2;

        public bool upperleg_collider1;
        public bool upperleg_collider2;
        public bool upperleg_collider3;

        public bool upperArm_collider;

        public bool head_collider1;
        public bool head_collider2;

        public bool Breast_collider;

        public bool plane_collider;
        public bool AAORemoveFlg;

        public bool plane_tail_collider;
        public AnimatorController controllerDef;
        public VRCExpressionsMenu menuDef;
        public VRCExpressionParameters paramDef;

        public AnimatorController controller;
        public VRCExpressionsMenu menu;
        public VRCExpressionParameters param;

        private string pathDir;

        public enum TextureResizeOption
        {
            LowerResolution,
            Delete,
        }

        public TextureResizeOption textureResize = TextureResizeOption.LowerResolution;

        public void Execute(VRCAvatarDescriptor descriptor)
        {
            var stopwatch = Stopwatch.StartNew();
            var stepTimes = new Dictionary<string, long>
            {
                ["InitializeAssets"] = InitializeAssets(descriptor),
                ["editProcessing"] = Edit(descriptor),
                ["FinalizeAssets"] = FinalizeAssets(descriptor),
            };
            stopwatch.Stop();
            Debug.Log(
                $"最適化を実行しました！総処理時間: {stopwatch.ElapsedMilliseconds}ms ({stopwatch.Elapsed.TotalSeconds:F2}秒)"
            );

            foreach (var kvp in stepTimes)
            {
                Debug.Log($"[Performance] {kvp.Key}: {kvp.Value}ms");
            }
        }

        private long Edit(VRCAvatarDescriptor descriptor)
        {
            var step2 = Stopwatch.StartNew();
            var body_b = descriptor.transform.Find("Body_b");
            if (body_b)
                if (body_b.TryGetComponent<SkinnedMeshRenderer>(out var body_bSMR))
                {
                    MizukiOptimizerBase.SetWeight(
                        body_bSMR,
                        "Foot_heel_OFF_____足_ヒールオフ",
                        heelFlg1 || heelFlg2 ? 0 : 100
                    );
                    MizukiOptimizerBase.SetWeight(
                        body_bSMR,
                        "Foot_Hiheel_____足_ハイヒール",
                        heelFlg2 ? 100 : 0
                    );
                }
            foreach (var config in GetParamConfigs(descriptor))
            {
                if (config.condition())
                    config.processAction();
                config.afterAction?.Invoke();
            }

            if (IKUSIA_emote)
                foreach (var control in menu.controls)
                    if (control.name == "IKUSIA_emote")
                    {
                        menu.controls.Remove(control);
                        break;
                    }

            IllMizukiDel4Quest.Edit4Quest(descriptor, this);

            step2.Stop();
            return step2.ElapsedMilliseconds;
        }

        private long FinalizeAssets(VRCAvatarDescriptor descriptor)
        {
            var step4 = Stopwatch.StartNew();
            RemoveUnusedMenuControls(menu, param);
            EditorUtility.SetDirty(controller);
            MarkAllMenusDirty(menu);
            EditorUtility.SetDirty(param);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            descriptor.baseAnimationLayers[4].animatorController = controller;
            descriptor.expressionsMenu = menu;
            descriptor.expressionParameters = param;
            EditorUtility.SetDirty(descriptor);
            step4.Stop();
            return step4.ElapsedMilliseconds;
        }

        private long InitializeAssets(VRCAvatarDescriptor descriptor)
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
                            AssetDatabase.GUIDToAssetPath("3eece7cfaddb2fe4fb361c09935d2231")
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
                        AssetDatabase.GUIDToAssetPath("78032fce499b8cd4c9590b79ccdf3166")
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
                            AssetDatabase.GUIDToAssetPath("ab33368960825474eb83487d302f6743")
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

        private static void RemoveUnusedMenuControls(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            if (menu == null)
                return;

            for (int i = menu.controls.Count - 1; i >= 0; i--)
            {
                var control = menu.controls[i];
                bool shouldRemove = true;

                if (string.IsNullOrEmpty(control.parameter.name))
                {
                    shouldRemove = false;
                }
                else
                {
                    if (param.parameters.Any(p => p.name == control.parameter.name))
                    {
                        shouldRemove = false;
                    }
                }

                if (control.type == VRCExpressionsMenu.Control.ControlType.SubMenu)
                {
                    RemoveUnusedMenuControls(control.subMenu, param);
                }

                if (
                    shouldRemove
                    || (
                        control.type == VRCExpressionsMenu.Control.ControlType.SubMenu
                        && control.subMenu.controls.Count == 0
                    )
                )
                {
                    menu.controls.RemoveAt(i);
                }
            }
        }

        private static void MarkAllMenusDirty(VRCExpressionsMenu menu)
        {
            if (menu == null)
                return;

            EditorUtility.SetDirty(menu);

            foreach (var control in menu.controls)
            {
                if (control.subMenu != null)
                {
                    MarkAllMenusDirty(control.subMenu);
                }
            }
        }

        public static VRCExpressionsMenu DuplicateExpressionMenu(
            VRCExpressionsMenu originalMenu,
            string parentPath,
            string iconPath,
            bool questFlg1,
            TextureResizeOption textureResize
        )
        {
            return DuplicateExpressionMenu(
                originalMenu,
                parentPath,
                iconPath,
                questFlg1,
                textureResize,
                null,
                null,
                null
            );
        }

        private static VRCExpressionsMenu DuplicateExpressionMenu(
            VRCExpressionsMenu originalMenu,
            string parentPath,
            string iconPath,
            bool questFlg1,
            TextureResizeOption textureResize,
            VRCExpressionsMenu rootMenuAsset = null,
            Dictionary<VRCExpressionsMenu, VRCExpressionsMenu> processedMenus = null,
            Dictionary<string, Texture2D> processedIcons = null
        )
        {
            if (originalMenu == null)
            {
                Debug.LogError("元のExpression Menuがありません");
                return null;
            }

            bool isRootCall = processedMenus == null;
            if (isRootCall)
            {
                processedMenus = new Dictionary<VRCExpressionsMenu, VRCExpressionsMenu>();
                processedIcons = new Dictionary<string, Texture2D>();
            }

            if (processedMenus.ContainsKey(originalMenu))
            {
                return processedMenus[originalMenu];
            }

            VRCExpressionsMenu newMenu = Instantiate(originalMenu);
            newMenu.name = originalMenu.name;

            processedMenus[originalMenu] = newMenu;

            if (isRootCall)
            {
                string menuAssetPath = Path.Combine(parentPath, originalMenu.name + ".asset");
                AssetDatabase.CreateAsset(newMenu, menuAssetPath);
                rootMenuAsset = newMenu;
            }
            else if (rootMenuAsset != null)
            {
                AssetDatabase.AddObjectToAsset(newMenu, rootMenuAsset);
            }

            for (int i = 0; i < newMenu.controls.Count; i++)
            {
                var control = newMenu.controls[i];
                if (questFlg1)
                {
                    if (textureResize == TextureResizeOption.LowerResolution)
                    {
                        var originalControl = originalMenu.controls[i];

                        if (originalControl.icon != null)
                        {
                            string iconAssetPath = AssetDatabase.GetAssetPath(originalControl.icon);
                            if (!string.IsNullOrEmpty(iconAssetPath))
                            {
                                string iconFileName = Path.GetFileName(iconAssetPath);
                                string destPath = Path.Combine(iconPath, iconFileName);

                                if (processedIcons.ContainsKey(iconAssetPath))
                                {
                                    control.icon = processedIcons[iconAssetPath];
                                }
                                else
                                {
                                    if (!File.Exists(destPath))
                                    {
                                        File.Copy(iconAssetPath, destPath, true);
                                        AssetDatabase.ImportAsset(destPath);
                                    }

                                    var copiedIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(
                                        destPath
                                    );
                                    if (copiedIcon != null)
                                    {
                                        var importer =
                                            AssetImporter.GetAtPath(destPath) as TextureImporter;
                                        if (importer != null)
                                        {
                                            importer.maxTextureSize = 32;
                                            importer.SaveAndReimport();
                                        }

                                        processedIcons[iconAssetPath] = copiedIcon;
                                        control.icon = copiedIcon;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        control.icon = null;
                    }
                }
                if (control.subMenu != null)
                {
                    control.subMenu = DuplicateExpressionMenu(
                        control.subMenu,
                        parentPath,
                        iconPath,
                        questFlg1,
                        textureResize,
                        rootMenuAsset,
                        processedMenus,
                        processedIcons
                    );
                }
            }
            EditorUtility.SetDirty(newMenu);
            if (isRootCall)
            {
                AssetDatabase.SaveAssets();
            }
            return newMenu;
        }
    }
}
#endif

#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using VRC.Dynamics;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
using VRC.SDKBase;
using Debug = UnityEngine.Debug;

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    public abstract class IKUSIAOverrideAbstract : MonoBehaviour, IEditorOnly
    {
        protected string pathDirSuffix = "/FX/";
        protected string pathName = "paryi_FX.controller";
        public AnimatorController controllerDef;
        public VRCExpressionsMenu menuDef;
        public VRCExpressionParameters paramDef;

        public AnimatorController controller;
        public VRCExpressionsMenu menu;
        public VRCExpressionParameters param;

        protected string pathDir;

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
                ["EditProcessing"] = Edit(descriptor),
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

        protected abstract long InitializeAssets(VRCAvatarDescriptor descriptor);

        protected abstract long Edit(VRCAvatarDescriptor descriptor);

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

        protected static readonly Dictionary<Type, MethodInfo[]> methodCache = new();

        protected struct ParamProcessConfig
        {
            public Func<bool> condition;
            public Action processAction;
            public Action afterAction;
        }

        protected void ProcessParam<T>(VRCAvatarDescriptor descriptor)
            where T : IKUSIAOverrideCore, new()
        {
            var type = typeof(T);
            const BindingFlags bindingFlags =
                BindingFlags.Public
                | BindingFlags.NonPublic
                | BindingFlags.Instance
                | BindingFlags.Static;

            if (!methodCache.TryGetValue(type, out var methods))
            {
                var initializeMethods = type.GetMethods(bindingFlags)
                    .Where(m => m.Name == "Initialize")
                    .ToArray();
                var Method =
                    initializeMethods.FirstOrDefault(m => m.GetParameters().Length == 3)
                    ?? initializeMethods.FirstOrDefault();
                methods = new[]
                {
                    Method,
                    type.GetMethod("DeleteFx", bindingFlags),
                    type.GetMethod("DeleteFxBT", bindingFlags),
                    type.GetMethod("DeleteParam", bindingFlags),
                    type.GetMethod("EditVRCExpressions", bindingFlags),
                    type.GetMethod("ParticleOptimize", bindingFlags),
                    type.GetMethod("ChangeObj", bindingFlags),
                    type.GetMethod("DeleteMenuButtonCtrl", bindingFlags),
                };
                methodCache[type] = methods;
            }

            var parametersField = type.GetField("Parameters", bindingFlags);
            var menuPathField = type.GetField("menuPath", bindingFlags);
            var delPathField = type.GetField("delPath", bindingFlags);
            var layersField = type.GetField("Layers", bindingFlags);

            var instance = ScriptableObject.CreateInstance<T>();
            try
            {
                var parameters =
                    GetFieldValue<List<string>>(parametersField, instance) ?? new List<string>();
                var menuPath =
                    GetFieldValue<List<string>>(menuPathField, instance) ?? new List<string>();
                var delPath =
                    GetFieldValue<List<string>>(delPathField, instance) ?? new List<string>();
                var layers =
                    GetFieldValue<List<string>>(layersField, instance) ?? new List<string>();

                var initializeMethod = methods[0];

                if (initializeMethod != null)
                {
                    var paramCount = initializeMethod.GetParameters().Length;
                    var args =
                        paramCount == 3
                            ? new object[] { descriptor, controller, this }
                            : new object[] { descriptor, controller };
                    initializeMethod.Invoke(instance, args);
                }

                methods[1]?.Invoke(instance, new object[] { layers });
                methods[2]?.Invoke(instance, new object[] { parameters });
                methods[3]?.Invoke(instance, new object[] { parameters });
                methods[4]?.Invoke(instance, new object[] { menu, param, parameters, menuPath });
                methods[5]?.Invoke(instance, null);
                methods[6]?.Invoke(instance, new object[] { delPath });
                methods[7]?.Invoke(instance, new object[] { parameters });
            }
            finally
            {
                if (instance != null)
                    DestroyImmediate(instance);
            }
        }

        private static TFieldType GetFieldValue<TFieldType>(FieldInfo field, object instance)
            where TFieldType : class
        {
            if (field == null)
                return null;

            if (field.IsStatic)
            {
                return field.GetValue(null) as TFieldType;
            }
            else
            {
                return field.GetValue(instance) as TFieldType;
            }
        }

        protected abstract ParamProcessConfig[] GetParamConfigs(VRCAvatarDescriptor descriptor);
        protected abstract void Edit4Quest(
            VRCAvatarDescriptor descriptor,
            MizukiOptimizer optimizer
        );

        protected static void DelPBByPathArray(
            VRCAvatarDescriptor descriptor,
            params string[] paths
        )
        {
            foreach (var path in paths)
            {
                IKUSIAOverrideCore.DestroyComponent<VRCPhysBoneBase>(
                    descriptor.transform.Find(path)
                );
            }
        }

        protected static void DelPBColliderByPathArray(
            VRCAvatarDescriptor descriptor,
            params string[] paths
        )
        {
            foreach (var path in paths)
            {
                IKUSIAOverrideCore.DestroyComponent<VRCPhysBoneColliderBase>(
                    descriptor.transform.Find(path)
                );
            }
        }

        protected static void DelColliderSettingByPathArray(
            VRCAvatarDescriptor descriptor,
            string[] colliderNames,
            string[] pbPaths
        )
        {
            foreach (var pbPath in pbPaths)
            {
                if (descriptor.transform.Find(pbPath))
                {
                    var physBone = descriptor
                        .transform.Find(pbPath)
                        .GetComponent<VRCPhysBoneBase>();
                    if (physBone != null && physBone.colliders != null)
                    {
                        foreach (var colliderName in colliderNames)
                        {
                            for (int i = physBone.colliders.Count - 1; i >= 0; i--)
                            {
                                var collider = physBone.colliders[i];
                                if (collider != null && collider.name.Contains(colliderName))
                                {
                                    physBone.colliders.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        // 安全にアセンブリから Type[] を取得するヘルパー
        private static Type[] SafeGetTypes(Assembly asm)
        {
            try
            {
                return asm.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(t => t != null).ToArray();
            }
            catch
            {
                return Array.Empty<Type>();
            }
        }

        // MizukiBase を継承する concrete クラス一覧を返す
        protected static Type[] GetMizukiBaseDerivedTypes(string TargetNamespace)
        {
            var baseType = typeof(MizukiBase);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies
                .SelectMany(a => SafeGetTypes(a))
                .Where(t =>
                    t != null
                    && t.IsClass
                    && !t.IsAbstract
                    && t.Namespace == TargetNamespace
                    && baseType.IsAssignableFrom(t)
                )
                .OrderBy(t => t.Name)
                .ToArray();
            return types;
        }

        // 型からインスタンスを作る（ScriptableObject 派生なら CreateInstance を使う）
        protected static IKUSIAOverrideCore CreateInstanceForType(Type t)
        {
            if (t == null)
                return null;
            if (!typeof(IKUSIAOverrideCore).IsAssignableFrom(t))
                return null;

            // ScriptableObject 派生なら CreateInstance を使う（エディタ専用）
            if (typeof(ScriptableObject).IsAssignableFrom(t))
            {
#if UNITY_EDITOR
                return ScriptableObject.CreateInstance(t) as IKUSIAOverrideCore;
#else
                return null;
#endif
            }

            try
            {
                return Activator.CreateInstance(t) as IKUSIAOverrideCore;
            }
            catch
            {
                return null;
            }
        }

        // 例: 非公開ジェネリックメソッド ProcessParam<T>(VRCAvatarDescriptor) を runtime の Type で呼ぶ方法
        // thisObj は ProcessParam を持つインスタンス（たとえば MizukiOptimizer の this）
        protected static void InvokeProcessParamByType(
            object thisObj,
            Type genericParamType,
            object descriptor
        )
        {
            if (thisObj == null || genericParamType == null)
                return;

            var mi = thisObj
                .GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .FirstOrDefault(m => m.Name == "ProcessParam" && m.IsGenericMethodDefinition);
            if (mi == null)
                return;

            var gm = mi.MakeGenericMethod(genericParamType);
            // ProcessParam<T> が戻り値を持つ場合は取得できる（ここでは戻り値を無視）
            gm.Invoke(thisObj, new object[] { descriptor });
        }

        // bool フィールドを読み取る／設定するヘルパー（static/instance 対応）
        protected bool GetBoolField(FieldInfo field)
        {
            if (field == null)
                return false;
            try
            {
                if (field.IsStatic)
                    return field.GetValue(null) is bool b && b;
                else
                    return field.GetValue(this) is bool b2 && b2;
            }
            catch
            {
                return false;
            }
        }

        protected void SetBoolField(FieldInfo field, bool value)
        {
            if (field == null)
                return;
            try
            {
                if (field.IsStatic)
                    field.SetValue(null, value);
                else
                    field.SetValue(this, value);
            }
            catch
            {
                // ignore
            }
        }

        // thisObj の ProcessParam<T>(VRCAvatarDescriptor) をリフレクションで呼ぶ
        protected static void InvokeProcessParamByType(
            object thisObj,
            Type genericParamType,
            VRCAvatarDescriptor descriptor
        )
        {
            if (thisObj == null || genericParamType == null)
                return;
            var mi = thisObj
                .GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .FirstOrDefault(m =>
                    m.Name == "ProcessParam"
                    && m.IsGenericMethodDefinition
                    && m.GetGenericArguments().Length == 1
                );
            if (mi == null)
                return;
            try
            {
                var gm = mi.MakeGenericMethod(genericParamType);
                gm.Invoke(thisObj, new object[] { descriptor });
            }
            catch
            {
                // ignore invocation errors
            }
        }
    }
}
#endif

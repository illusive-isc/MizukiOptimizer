#if UNITY_EDITOR
using System;
using System.Linq;
using System.Reflection;
using jp.illusive_isc.IKUSIAOverride.Mizuki;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace jp.illusive_isc.IKUSIAOverride
{
    public class IKUSIAOverrideEditor : Editor
    {
        static System.Collections.Generic.Dictionary<Type, FieldInfo[]> _propertyFieldCache = new();
        protected void AutoInitializeSerializedProperties(IKUSIAOverrideEditor editorInstance)
        {
            // キャッシュ用の Dictionary を static フィールドとして保持

            var editorType = editorInstance.GetType();
            if (!_propertyFieldCache.TryGetValue(editorType, out var serializedPropertyFields))
            {
                serializedPropertyFields = editorType
                    .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(f => f.FieldType == typeof(SerializedProperty))
                    .ToArray();
                _propertyFieldCache[editorType] = serializedPropertyFields;
            }

            foreach (var field in serializedPropertyFields)
            {
                try
                {
                    field.SetValue(editorInstance, serializedObject.FindProperty(field.Name));
                }
                catch (Exception) { }
            }
        }

        protected static bool ValidateCreateObj(MenuCommand menuCommand, string name)
        {
            if (menuCommand.context is not GameObject contextGO)
                contextGO = Selection.activeGameObject;
            if (contextGO == null)
                return false;

            if (contextGO.GetComponent<VRCAvatarDescriptor>() == null)
                return false;
            if (
                contextGO.transform.parent != null
                && contextGO.transform.parent.GetComponent<VRCAvatarDescriptor>() != null
            )
                return false;
            Animator animator = contextGO.GetComponent<Animator>();
            if (animator == null)
                return false;
            if (animator.avatar == null)
                return false;
            if (animator.avatar.name != name)
                return false;
            return true;
        }

        protected static void CreateObj(MenuCommand menuCommand, string name)
        {
            // 新しい GameObject を作成し、MizukiOptimizer コンポーネントを追加
            GameObject go = new(name);
            go.AddComponent<MizukiOptimizer>();

            // 右クリックで選択されたオブジェクト（VRCアバターのルート）の子として配置
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
    }
}
#endif

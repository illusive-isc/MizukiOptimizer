#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace jp.illusive_isc.MizukiOptimizer
{
    public partial class MizukiOptimizer
    {

        [SerializeField]
        private bool statusFlg = false;

        public bool heelFlg1 = true;

        public bool heelFlg2 = false;

        [SerializeField]
        private bool FreeClothFlg = false;

        [SerializeField]
        private bool ClothFlg = false;

        public bool ClothFlg1 = false;

        public bool ClothFlg2 = true;

        public bool ClothFlg3 = false;

        public bool ClothFlg4 = false;

        public bool ClothFlg5 = false;

        public bool ClothFlg6 = false;

        public bool ClothFlg7 = false;

        public bool ClothFlg8 = false;

        [SerializeField]
        private bool HairFlg = false;

        public bool HairFlg1 = false;

        public bool HairFlg11 = false;

        public bool HairFlg12 = false;

        public bool HairFlg2 = false;

        public bool HairFlg22 = false;

        public bool HairFlg3 = false;

        public bool HairFlg4 = false;

        public bool HairFlg5 = false;

        public bool HairFlg51 = false;

        public bool HairFlg6 = false;

        public bool TailFlg = false;

        [SerializeField]
        private bool TailFlg1 = false;

        [SerializeField]
        private bool TPSFlg = false;

        [SerializeField]
        private bool ClairvoyanceFlg = false;

        [SerializeField]
        private bool JointBallFlg = false;

        [SerializeField]
        private bool TeleportFlg = false;

        [SerializeField]
        private bool AvatarLightFlg = false;

        [SerializeField]
        private bool colliderJumpFlg = false;

        [SerializeField]
        private bool pictureFlg = false;

        [SerializeField]
        private bool BreastSizeFlg = false;
        public bool BreastSizeFlg1 = false;
        public bool BreastSizeFlg2 = false;
        public bool BreastSizeFlg3 = true;

        [SerializeField]
        private bool LightGunFlg = false;

        [SerializeField]
        private bool WhiteBreathFlg = false;

        [SerializeField]
        private bool FreeGimmickFlg = false;

        [SerializeField]
        private bool drinkFlg = false;

        [SerializeField]
        private bool issyouFlg = false;

        [SerializeField]
        private bool helpFlg = false;

        [SerializeField]
        private bool FootStampFlg = false;

        [SerializeField]
        private bool eightBitFlg = false;

        [SerializeField]
        private bool PenCtrlFlg = false;

        public bool FreeParticleFlg = false;
        public bool HandTrailFlg = false;
        public bool NailTrailFlg = false;

        public bool FaceGestureFlg = false;

        public bool FaceLockFlg = false;

        public bool FaceValFlg = false;

        public bool kamitukiFlg = false;

        public bool nadeFlg = false;

        public bool blinkFlg = false;

        [SerializeField]
        private bool IKUSIA_emote = false;

        [SerializeField]
        private bool IKUSIA_emote1 = true;
        private static readonly Dictionary<Type, System.Reflection.MethodInfo[]> methodCache =
            new();

        private struct ParamProcessConfig
        {
            public Func<bool> condition;
            public Action processAction;
            public Action afterAction;
        }

        private void ProcessParam<T>(VRCAvatarDescriptor descriptor)
            where T : MizukiOptimizerBase, new()
        {
            var type = typeof(T);
            const System.Reflection.BindingFlags bindingFlags =
                System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.Static;

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
                    type.GetMethod("DeleteVRCExpressions", bindingFlags),
                    type.GetMethod("ParticleOptimize", bindingFlags),
                    type.GetMethod("ChangeObj", bindingFlags),
                };
                methodCache[type] = methods;
            }

            // 静的フィールドから値を一括取得（static/instanceを適切に判定）
            var parametersField = type.GetField("Parameters", bindingFlags);
            var menuPathField = type.GetField("menuPath", bindingFlags);
            var delPathField = type.GetField("delPath", bindingFlags);
            var layersField = type.GetField("Layers", bindingFlags);

            var instance = new T();

            // フィールドがstaticかどうかを判定してアクセス
            var parameters =
                GetFieldValue<List<string>>(parametersField, instance) ?? new List<string>();
            var menuPath =
                GetFieldValue<List<string>>(menuPathField, instance) ?? new List<string>();
            var delPath = GetFieldValue<List<string>>(delPathField, instance) ?? new List<string>();
            var layers = GetFieldValue<List<string>>(layersField, instance) ?? new List<string>();

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
        }

        // フィールドがstaticかinstanceかを判定してアクセスするヘルパーメソッド
        private static TFieldType GetFieldValue<TFieldType>(
            System.Reflection.FieldInfo field,
            object instance
        )
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

        private ParamProcessConfig[] GetParamConfigs(VRCAvatarDescriptor descriptor)
        {
            return new ParamProcessConfig[]
            {
                new()
                {
                    condition = () => true,
                    processAction = () => ProcessParam<Core>(descriptor),
                },
                new()
                {
                    condition = () => statusFlg,
                    processAction = () => ProcessParam<Status>(descriptor),
                },
                new()
                {
                    condition = () => TailFlg,
                    processAction = () => ProcessParam<Tail>(descriptor),
                    afterAction = () =>
                    {
                        if (TailFlg1)
                            MizukiOptimizerBase.DestroyObj(
                                descriptor.transform.Find("tail_ribbon")
                            );
                    },
                },
                new()
                {
                    condition = () => FreeClothFlg,
                    processAction = () => ProcessParam<FreeCloth>(descriptor),
                },
                new()
                {
                    condition = () => ClothFlg,
                    processAction = () => ProcessParam<Cloth>(descriptor),
                },
                new()
                {
                    condition = () => HairFlg,
                    processAction = () => ProcessParam<Hair>(descriptor),
                    afterAction = () =>
                    {
                        if (descriptor.transform.Find("Advanced/Particle/4"))
                            if (questFlg1)
                            {
                                MizukiOptimizerBase.DestroyObj(
                                    descriptor.transform.Find(
                                        "Armature/Hips/Spine/Chest/Neck/Head/headphone_particle"
                                    )
                                );
                                MizukiOptimizerBase.DestroyObj(
                                    descriptor.transform.Find("Advanced/Particle/4")
                                );
                            }
                    },
                },
                new()
                {
                    condition = () => TPSFlg,
                    processAction = () => ProcessParam<TPS>(descriptor),
                },
                new()
                {
                    condition = () => ClairvoyanceFlg,
                    processAction = () => ProcessParam<Clairvoyance>(descriptor),
                },
                new()
                {
                    condition = () => JointBallFlg,
                    processAction = () => ProcessParam<JointBall>(descriptor),
                },
                new()
                {
                    condition = () => TeleportFlg,
                    processAction = () => ProcessParam<Teleport>(descriptor),
                },
                new()
                {
                    condition = () => AvatarLightFlg,
                    processAction = () => ProcessParam<AvatarLight>(descriptor),
                },
                new()
                {
                    condition = () => colliderJumpFlg,
                    processAction = () => ProcessParam<Collider>(descriptor),
                },
                new()
                {
                    condition = () => pictureFlg,
                    processAction = () => ProcessParam<Picture>(descriptor),
                },
                new()
                {
                    condition = () => BreastSizeFlg,
                    processAction = () => ProcessParam<BreastSize>(descriptor),
                },
                new()
                {
                    condition = () => LightGunFlg,
                    processAction = () => ProcessParam<LightGun>(descriptor),
                },
                new()
                {
                    condition = () => WhiteBreathFlg,
                    processAction = () => ProcessParam<WhiteBreath>(descriptor),
                },
                new()
                {
                    condition = () => FootStampFlg,
                    processAction = () => ProcessParam<FootStamp>(descriptor),
                },
                new()
                {
                    condition = () => eightBitFlg,
                    processAction = () => ProcessParam<EightBit>(descriptor),
                },
                new()
                {
                    condition = () => FreeParticleFlg,
                    processAction = () => ProcessParam<FreeParticle>(descriptor),
                },
                new()
                {
                    condition = () => FreeGimmickFlg,
                    processAction = () => ProcessParam<FreeGimmick>(descriptor),
                },
                new()
                {
                    condition = () => drinkFlg,
                    processAction = () => ProcessParam<Drink>(descriptor),
                },
                new()
                {
                    condition = () => issyouFlg,
                    processAction = () => ProcessParam<Issyou>(descriptor),
                },
                new()
                {
                    condition = () => helpFlg,
                    processAction = () => ProcessParam<Help>(descriptor),
                },
                new()
                {
                    condition = () => HandTrailFlg,
                    processAction = () => ProcessParam<HandTrail>(descriptor),
                },
                new()
                {
                    condition = () => NailTrailFlg,
                    processAction = () => ProcessParam<NailTrail>(descriptor),
                },
                new()
                {
                    condition = () => PenCtrlFlg,
                    processAction = () => ProcessParam<PenCtrl>(descriptor),
                },
                new()
                {
                    condition = () => FaceGestureFlg || FaceLockFlg || FaceValFlg,
                    processAction = () => ProcessParam<FaceGesture>(descriptor),
                },
                new()
                {
                    condition = () => kamitukiFlg || nadeFlg || blinkFlg,
                    processAction = () => ProcessParam<FaceContact>(descriptor),
                },
            };
        }
    }
}
#endif

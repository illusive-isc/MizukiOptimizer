#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    public partial class MizukiOptimizer : IKUSIAOverrideAbstract
    {
        [SerializeField]
        private bool StatusFlg = false;

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
        private bool FaceEffectFlg = false;

        [SerializeField]
        private bool FreeObjFlg = false;

        [SerializeField]
        private bool NailGaoFlg = false;

        public bool NailGaoFlg2 = false;

        [SerializeField]
        private bool ArmAcceFlg = false;

        [SerializeField]
        private bool BeltFlg = false;

        [SerializeField]
        private bool LegBeltFlg = false;

        [SerializeField]
        private bool OuterFlg = false;

        [SerializeField]
        private bool ShoesFlg = false;

        [SerializeField]
        private bool AvatarLightFlg = false;

        [SerializeField]
        private bool ColliderFlg = false;

        [SerializeField]
        private bool PictureFlg = false;

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
        private bool DrinkFlg = false;

        [SerializeField]
        private bool IssyouFlg = false;

        [SerializeField]
        private bool HelpFlg = false;

        [SerializeField]
        private bool FootStampFlg = false;

        [SerializeField]
        private bool EightBitFlg = false;

        [SerializeField]
        private bool PenCtrlFlg = false;

        public bool FreeParticleFlg = false;
        public bool HandTrailFlg = false;
        public bool NailTrailFlg = false;

        public bool FaceGestureFlg = false;
        public bool FaceGestureFlg2 = false;

        public bool FaceLockFlg = false;

        public bool FaceValFlg = false;

        public bool kamitukiFlg = false;
        public bool FaceContactFlg = false;

        public bool nadeFlg = false;

        public bool blinkFlg = false;

        public bool AccesaryFlg = false;

        public bool EarAngryFlg = false;

        public bool EarTailFlg = false;

        public bool ElfEarFlg = false;

        public bool EyemaskFlg = false;

        public bool FronthairLeftFlg = false;

        public bool FronthairRightFlg = false;

        public bool HeadDressFlg = false;
        public bool MenuFlg = false;

        [SerializeField]
        private bool IKUSIA_emote = false;
        private readonly bool CoreFlg = true;

        // フィールドがstaticかinstanceかを判定してアクセスするヘルパーメソッド
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

        protected override ParamProcessConfig[] GetParamConfigs(VRCAvatarDescriptor descriptor)
        {
            var types = GetMizukiBaseDerivedTypes("jp.illusive_isc.IKUSIAOverride.Mizuki");
            var myType = GetType();
            return types
                .Select(t =>
                {
                    // フィールド名は {TypeName}Flg を期待
                    var flagField = myType.GetField(
                        t.Name + "Flg",
                        BindingFlags.Instance
                            | BindingFlags.Static
                            | BindingFlags.Public
                            | BindingFlags.NonPublic
                    );

                    bool condition() => GetBoolField(flagField);
                    void processAction() => InvokeProcessParamByType(this, t, descriptor);

                    return new ParamProcessConfig
                    {
                        condition = condition,
                        processAction = processAction,
                    };
                })
                .ToArray();
        }
    }
}
#endif

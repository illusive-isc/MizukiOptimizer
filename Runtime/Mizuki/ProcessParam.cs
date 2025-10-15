#if UNITY_EDITOR
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

        public bool TPSFlg = false;

        public bool ClairvoyanceFlg = false;

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
        private bool CameraPictureFlg = false;

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

        protected ParamProcessConfig[] GetParamConfigs(VRCAvatarDescriptor descriptor)
        {
            return GetParamConfigs(descriptor, "jp.illusive_isc.IKUSIAOverride.Mizuki");
        }
    }
}
#endif

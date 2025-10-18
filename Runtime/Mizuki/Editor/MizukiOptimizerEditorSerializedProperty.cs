using UnityEditor;

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    internal partial class MizukiOptimizerEditor : IKUSIAOverrideEditor
    {
        // 多重実行防止用フラグ
        static bool isExecuting = false;
        SerializedProperty ClothFlg;
        SerializedProperty ClothDelFlg;
        SerializedProperty ClothDelFlg2;
        SerializedProperty StatusFlg;
        SerializedProperty NailGaoFlg;
        SerializedProperty NailGaoFlg2;

        SerializedProperty ArmAcceFlg;
        SerializedProperty ArmAcceFlg2;

        SerializedProperty BeltFlg;
        SerializedProperty BeltFlg2;

        SerializedProperty LegBeltFlg;
        SerializedProperty LegBeltFlg2;

        SerializedProperty OuterFlg;
        SerializedProperty OuterFlg2;

        SerializedProperty ShoesFlg;
        SerializedProperty ShoesFlg2;
        SerializedProperty TPSFlg;
        SerializedProperty ClairvoyanceFlg;
        SerializedProperty JointBallFlg;
        SerializedProperty TeleportFlg;
        SerializedProperty AvatarLightFlg;
        SerializedProperty ColliderFlg;
        SerializedProperty PictureFlg;
        SerializedProperty CameraPictureFlg;
        SerializedProperty BreastSizeFlg;
        SerializedProperty LightGunFlg;
        SerializedProperty WhiteBreathFlg;
        SerializedProperty FreeGimmickFlg;
        SerializedProperty DrinkFlg;
        SerializedProperty IssyouFlg;
        SerializedProperty HelpFlg;
        SerializedProperty FaceEffectFlg;
        SerializedProperty FreeObjFlg;
        SerializedProperty FootStampFlg;
        SerializedProperty EightBitFlg;
        SerializedProperty PenCtrlFlg;
        SerializedProperty FreeParticleFlg;
        SerializedProperty HandTrailFlg;
        SerializedProperty NailTrailFlg;
        SerializedProperty FaceGestureFlg;
        SerializedProperty FaceGestureFlg2;
        SerializedProperty FaceLockFlg;
        SerializedProperty FaceValFlg;
        SerializedProperty kamitukiFlg;
        SerializedProperty nadeFlg;
        SerializedProperty paryi_FX;
        SerializedProperty menu;
        SerializedProperty param;
        SerializedProperty paryi_FXDef;
        SerializedProperty menuDef;
        SerializedProperty paramDef;
        SerializedProperty IKUSIA_emote;
        SerializedProperty heelFlg1;
        SerializedProperty heelFlg2;
        SerializedProperty FreeClothFlg;
        SerializedProperty BreastSizeFlg1;
        SerializedProperty BreastSizeFlg2;
        SerializedProperty BreastSizeFlg3;

        SerializedProperty AccesaryFlg;
        SerializedProperty AccesaryFlg2;

        SerializedProperty EarAngryFlg;

        SerializedProperty EarTailFlg;
        SerializedProperty EarTailFlg2;

        SerializedProperty ElfEarFlg;
        SerializedProperty ElfEarFlg2;

        SerializedProperty EyemaskFlg;

        SerializedProperty FronthairLeftFlg;

        SerializedProperty FronthairRightFlg;

        SerializedProperty HeadDressFlg;
        SerializedProperty MenuFlg;
        SerializedProperty FaceContactFlg;
        SerializedProperty EyemaskFlg2;
        SerializedProperty HeadDressFlg2;
        SerializedProperty HairFlg;
        SerializedProperty FreeMenuFlg;

        private void OnEnable()
        {
            AutoInitializeSerializedProperties(this);
        }
    }
}

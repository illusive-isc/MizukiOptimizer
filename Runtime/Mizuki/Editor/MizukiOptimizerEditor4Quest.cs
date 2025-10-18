using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRC.Dynamics;

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    internal partial class MizukiOptimizerEditor : IKUSIAOverrideEditor
    {
        bool questArea;

        SerializedProperty questFlg1;

        SerializedProperty Butt;
        SerializedProperty Skirt_Root;
        SerializedProperty Breast;
        SerializedProperty Cheek;
        SerializedProperty ahoge;
        SerializedProperty Backhair;
        SerializedProperty Front;
        SerializedProperty Frontside;
        SerializedProperty side;
        SerializedProperty headband_Root;
        SerializedProperty tang;
        SerializedProperty TigerEar;
        SerializedProperty Shoulder_Ribbon;
        SerializedProperty coat_hand;
        SerializedProperty Hand_frills;
        SerializedProperty tail;
        SerializedProperty Leg_frills;

        SerializedProperty Upperleg_collider1;
        SerializedProperty Upperleg_collider2;
        SerializedProperty Chest_collider;
        SerializedProperty Butt_collider;
        SerializedProperty UpperArm_collider1;
        SerializedProperty UpperArm_collider2;
        SerializedProperty Shoulder_collider;
        SerializedProperty textureResize;
        SerializedProperty AAORemoveFlg;
        private int pbTCount = 373;
        private int pbCCount = 252;
        private int pbCount = 55;

        protected static readonly List<PhysBoneInfo> PhysBoneInfoList = new()
        {
            new()
            {
                name = "ヒップ",
                flgName = "Butt",
                AffectedCount = 4,
                ColliderCount = 0,
                PBCount = 2,
            },
            new()
            {
                name = "スカート",
                autodeletePropName = "ClothDelFlg",
                flgName = "Skirt_Root",
                AffectedCount = 79,
                ColliderCount = 132,
                titlesAndNames = new[] { ("脚コライダー", "Upperleg_collider1", 1f) },
                PBCount = 3,
            },
            new()
            {
                name = "バスト",
                flgName = "Breast",
                AffectedCount = 6,
                ColliderCount = 12,
                titlesAndNames = new[]
                {
                    ("肩コライダー", "Shoulder_collider", 1f),
                    ("上腕コライダー", "UpperArm_collider1", 1),
                },
                PBCount = 2,
            },
            new()
            {
                name = "頬",
                flgName = "Cheek",
                AffectedCount = 4,
                ColliderCount = 0,
                PBCount = 2,
            },
            new()
            {
                name = "あほげ",
                autodeletePropName = "HairFlg",
                flgName = "ahoge",
                AffectedCount = 5,
                ColliderCount = 0,
                PBCount = 1,
            },
            new()
            {
                name = "後ろ髪",
                autodeletePropName = "HairFlg",
                flgName = "Backhair",
                AffectedCount = 63,
                ColliderCount = 30,
                titlesAndNames = new[]
                {
                    ("チェストコライダー", "Chest_collider", 1f),
                    ("ヒップコライダー", "Butt_collider", 1f),
                },
                PBCount = 11,
            },
            new()
            {
                name = "前髪",
                autodeletePropName = "HairFlg",
                flgName = "Front",
                AffectedCount = 19,
                ColliderCount = 0,
                PBCount = 4,
            },
            new()
            {
                name = "前髪サイド",
                autodeletePropName = "HairFlg",
                flgName = "Frontside",
                AffectedCount = 16,
                ColliderCount = 0,
                PBCount = 4,
            },
            new()
            {
                name = "サイド",
                autodeletePropName = "HairFlg",
                flgName = "side",
                AffectedCount = 92,
                ColliderCount = 0,
                PBCount = 12,
            },
            new()
            {
                name = "ヘッドバンド",
                autodeletePropName = "ClothDelFlg",
                flgName = "headband_Root",
                AffectedCount = 17,
                ColliderCount = 0,
                PBCount = 1,
            },
            new()
            {
                name = "舌",
                flgName = "tang",
                AffectedCount = 3,
                ColliderCount = 0,
                PBCount = 1,
            },
            new()
            {
                name = "虎耳",
                autodeletePropName = "EarTailFlg2",
                flgName = "TigerEar",
                AffectedCount = 8,
                ColliderCount = 13,
                PBCount = 2,
            },
            new()
            {
                name = "肩リボン",
                autodeletePropName = "ClothDelFlg",
                flgName = "Shoulder_Ribbon",
                AffectedCount = 24,
                ColliderCount = 10,
                titlesAndNames = new[] { ("上腕コライダー", "UpperArm_collider2", 1f) },
                PBCount = 4,
            },
            new()
            {
                name = "萌え袖",
                autodeletePropName = "OuterFlg2",
                flgName = "coat_hand",
                AffectedCount = 10,
                ColliderCount = 18,
                PBCount = 2,
            },
            new()
            {
                name = "手フリル",
                autodeletePropName = "ClothDelFlg",
                flgName = "Hand_frills",
                AffectedCount = 4,
                ColliderCount = 18,
                PBCount = 2,
            },
            new()
            {
                name = "尻尾",
                autodeletePropName = "EarTailFlg2",
                flgName = "tail",
                AffectedCount = 14,
                ColliderCount = 26,
                titlesAndNames = new[] { ("脚コライダー", "Upperleg_collider2", 1f) },
                PBCount = 1,
            },
            new()
            {
                name = "脚フリル",
                autodeletePropName = "ClothDelFlg",
                flgName = "Leg_frills",
                AffectedCount = 5,
                ColliderCount = 18,
                PBCount = 1,
            },
        };

        private void Quest()
        {
            questArea = EditorGUILayout.Foldout(questArea, "Quest用調整項目(素体のみ)", true);

            if (questArea)
            {
                QuestDialog(
                    target as IKUSIAOverrideAbstract,
                    questFlg1,
                    "Quest化に対応してないコンポーネントやシェーダーを使っているためTPS、透視、コライダー・ジャンプ、撮影ギミック、ライトガン、ホワイトブレス、8bit、ペン操作、ハートガンなどを削除します。\n"
                );

                if (questFlg1.boolValue)
                {
                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
                    StatusFlg.boolValue = true;
                    TPSFlg.boolValue = true;
                    ClairvoyanceFlg.boolValue = true;
                    ColliderFlg.boolValue = true;
                    PictureFlg.boolValue = true;
                    LightGunFlg.boolValue = true;
                    WhiteBreathFlg.boolValue = true;
                    FreeGimmickFlg.boolValue = true;
                    FootStampFlg.boolValue = true;
                    EightBitFlg.boolValue = true;
                    PenCtrlFlg.boolValue = true;
                    FreeParticleFlg.boolValue = true;
                    CameraPictureFlg.boolValue = true;
                    JointBallFlg.boolValue = true;
                    HandTrailFlg.boolValue = true;
                    IssyouFlg.boolValue = true;
                    DrinkFlg.boolValue = true;
                    FaceEffectFlg.boolValue = true;
                    NailTrailFlg.boolValue = true;
                    TeleportFlg.boolValue = true;
                    MenuFlg.boolValue = true;
                    HelpFlg.boolValue = true;

                    serializedObject.ApplyModifiedProperties();
                }
                if (GUILayout.Button("おすすめ設定にする"))
                {
                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
                    Butt.boolValue = true;
                    Skirt_Root.boolValue = true;
                    Breast.boolValue = false;
                    Cheek.boolValue = true;
                    ahoge.boolValue = true;
                    Backhair.boolValue = true;
                    Front.boolValue = true;
                    Frontside.boolValue = true;
                    side.boolValue = true;
                    headband_Root.boolValue = true;
                    tang.boolValue = true;
                    TigerEar.boolValue = false;
                    Shoulder_Ribbon.boolValue = true;
                    coat_hand.boolValue = true;
                    Hand_frills.boolValue = true;
                    tail.boolValue = false;
                    Leg_frills.boolValue = true;
                    UpperArm_collider1.boolValue = true;
                    Shoulder_collider.boolValue = true;
                    Upperleg_collider2.boolValue = false;

                    serializedObject.ApplyModifiedProperties();
                }

                RenderProperty(PhysBoneInfoList);

                Count(PhysBoneInfoList, pbCount, pbTCount, pbCCount);
                DelMenu(textureResize, AAORemoveFlg);
            }
        }
    }
}

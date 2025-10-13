using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
#if AVATAR_OPTIMIZER_FOUND
using Anatawa12.AvatarOptimizer;
#endif

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [CustomEditor(typeof(MizukiOptimizer))]
    [AddComponentMenu("")]
    internal class MizukiOptimizerEditor : IKUSIAOverrideEditor
    {
        SerializedProperty StatusFlg;
        SerializedProperty NailGaoFlg;
        SerializedProperty NailGaoFlg2;

        SerializedProperty ArmAcceFlg;

        SerializedProperty BeltFlg;

        SerializedProperty LegBeltFlg;

        SerializedProperty OuterFlg;

        SerializedProperty ShoesFlg;
        SerializedProperty TPSFlg;
        SerializedProperty ClairvoyanceFlg;
        SerializedProperty JointBallFlg;
        SerializedProperty TeleportFlg;
        SerializedProperty AvatarLightFlg;
        SerializedProperty ColliderFlg;
        SerializedProperty PictureFlg;
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
        SerializedProperty controller;
        SerializedProperty menu;
        SerializedProperty param;
        SerializedProperty controllerDef;
        SerializedProperty menuDef;
        SerializedProperty paramDef;
        SerializedProperty IKUSIA_emote;
        SerializedProperty heelFlg1;
        SerializedProperty heelFlg2;
        SerializedProperty FreeClothFlg;
        SerializedProperty BreastSizeFlg1;
        SerializedProperty BreastSizeFlg2;
        SerializedProperty BreastSizeFlg3;
        SerializedProperty questFlg1;

        SerializedProperty Skirt_Root;
        SerializedProperty Breast;
        SerializedProperty backhair;
        SerializedProperty back_side_root;
        SerializedProperty Head_002;
        SerializedProperty Front_hair2_root;
        SerializedProperty side_1_root;
        SerializedProperty hair_2;
        SerializedProperty sidehair;
        SerializedProperty side_3_root;
        SerializedProperty Side_root;
        SerializedProperty tail_044;
        SerializedProperty tail_022;

        SerializedProperty chest_collider1;
        SerializedProperty chest_collider2;
        SerializedProperty upperleg_collider1;
        SerializedProperty upperleg_collider2;
        SerializedProperty upperleg_collider3;
        SerializedProperty upperArm_collider;
        SerializedProperty plane_collider;
        SerializedProperty head_collider1;
        SerializedProperty head_collider2;
        SerializedProperty Breast_collider;
        SerializedProperty plane_tail_collider;
        SerializedProperty textureResize;
        SerializedProperty AAORemoveFlg;

        SerializedProperty AccesaryFlg;

        SerializedProperty EarAngryFlg;

        SerializedProperty EarTailFlg;

        SerializedProperty ElfEarFlg;

        SerializedProperty EyemaskFlg;

        SerializedProperty FronthairLeftFlg;

        SerializedProperty FronthairRightFlg;

        SerializedProperty HeadDressFlg;
        SerializedProperty MenuFlg;
        SerializedProperty FaceContactFlg;
        bool questArea;

        // PB情報とコライダー情報のクラス定義（namespace内、Editorクラス外に移動）
        public class PhysBoneInfo
        {
            public int AffectedCount; //:Transform数
            public int Count; //:Transform数
            public int ColliderCount; //:Collider数
        }

        public static readonly Dictionary<string, PhysBoneInfo> physBoneList = new()
        {
            {
                "Breast",
                new PhysBoneInfo { AffectedCount = 6, ColliderCount = 0 }
            },
            {
                "back_side_root",
                new PhysBoneInfo { AffectedCount = 9, ColliderCount = 0 }
            },
            {
                "Head_002",
                new PhysBoneInfo { AffectedCount = 4, ColliderCount = 0 }
            },
            {
                "Front_hair2_root",
                new PhysBoneInfo { AffectedCount = 10, ColliderCount = 0 }
            },
            {
                "side_1_root",
                new PhysBoneInfo { AffectedCount = 15, ColliderCount = 0 }
            },
            {
                "hair_2",
                new PhysBoneInfo { AffectedCount = 10, ColliderCount = 0 }
            },
            {
                "sidehair",
                new PhysBoneInfo { AffectedCount = 6, ColliderCount = 0 }
            },
            {
                "side_3_root",
                new PhysBoneInfo { AffectedCount = 37, ColliderCount = 0 }
            },
            {
                "tail_022",
                new PhysBoneInfo { AffectedCount = 10, ColliderCount = 0 }
            },
            {
                "tail_044",
                new PhysBoneInfo { AffectedCount = 18, ColliderCount = 13 }
            },
            {
                "Side_root",
                new PhysBoneInfo { AffectedCount = 13, ColliderCount = 20 }
            },
            {
                "backhair",
                new PhysBoneInfo { AffectedCount = 20, ColliderCount = 18 }
            },
            {
                "Skirt_Root",
                new PhysBoneInfo { AffectedCount = 42, ColliderCount = 60 }
            },
        };

        private void OnEnable()
        {
            AutoInitializeSerializedProperties(this);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            EditorGUILayout.PropertyField(heelFlg1, new GUIContent("ヒールON"));
            EditorGUILayout.PropertyField(heelFlg2, new GUIContent("ハイヒールON"));

            EditorGUILayout.PropertyField(FreeClothFlg, new GUIContent("フリー衣装削除"));
            EditorGUILayout.PropertyField(FreeObjFlg, new GUIContent("フリーオブジェクト削除"));

            EditorGUILayout.PropertyField(NailGaoFlg, new GUIContent("Nail gao~削除"));
            EditorGUILayout.PropertyField(NailGaoFlg2, new GUIContent("  └ 有効にする"));
            EditorGUILayout.PropertyField(ArmAcceFlg, new GUIContent("Arm Acce削除"));
            EditorGUILayout.PropertyField(BeltFlg, new GUIContent("belt削除"));
            EditorGUILayout.PropertyField(LegBeltFlg, new GUIContent("leg belt削除"));
            EditorGUILayout.PropertyField(OuterFlg, new GUIContent("outer削除"));
            EditorGUILayout.PropertyField(ShoesFlg, new GUIContent("shoes削除"));

            EditorGUILayout.PropertyField(BreastSizeFlg, new GUIContent("バストサイズ変更削除"));
            if (!BreastSizeFlg.boolValue)
            {
                GUI.enabled = false;
                BreastSizeFlg1.boolValue = false;
                BreastSizeFlg2.boolValue = false;
                BreastSizeFlg3.boolValue = false;
            }
            EditorGUILayout.PropertyField(BreastSizeFlg1, new GUIContent("  ├ smallにする"));
            EditorGUILayout.PropertyField(BreastSizeFlg3, new GUIContent("  ├ 100にする"));
            EditorGUILayout.PropertyField(BreastSizeFlg2, new GUIContent("  └ rurune100にする"));
            GUI.enabled = true;
            {
                var Mizuki = (MizukiOptimizer)target;
                if (BreastSizeFlg1.boolValue != Mizuki.BreastSizeFlg1)
                {
                    BreastSizeFlg2.boolValue = false;
                    BreastSizeFlg3.boolValue = false;
                }
                else if (BreastSizeFlg2.boolValue != Mizuki.BreastSizeFlg2)
                {
                    BreastSizeFlg1.boolValue = false;
                    BreastSizeFlg3.boolValue = false;
                }
                else if (BreastSizeFlg3.boolValue != Mizuki.BreastSizeFlg3)
                {
                    BreastSizeFlg1.boolValue = false;
                    BreastSizeFlg2.boolValue = false;
                }
            }
            GUI.enabled = true;

            EditorGUILayout.PropertyField(AccesaryFlg, new GUIContent("アクセサリー削除"));

            EditorGUILayout.PropertyField(EarAngryFlg, new GUIContent("耳怒り削除"));

            EditorGUILayout.PropertyField(EarTailFlg, new GUIContent("耳しっぽ削除"));

            EditorGUILayout.PropertyField(ElfEarFlg, new GUIContent("エルフ耳削除"));

            EditorGUILayout.PropertyField(EyemaskFlg, new GUIContent("アイマスク削除"));

            EditorGUILayout.PropertyField(FronthairLeftFlg, new GUIContent("前髪左削除"));

            EditorGUILayout.PropertyField(FronthairRightFlg, new GUIContent("前髪右削除"));

            EditorGUILayout.PropertyField(HeadDressFlg, new GUIContent("ヘッドドレス削除"));
            GUILayout.EndVertical();
            GUILayout.BeginVertical();

            EditorGUILayout.PropertyField(EightBitFlg, new GUIContent("8bit削除"));
            EditorGUILayout.PropertyField(FootStampFlg, new GUIContent("足跡削除"));
            EditorGUILayout.PropertyField(
                FreeParticleFlg,
                new GUIContent("フリーパーティクル削除")
            );
            EditorGUILayout.PropertyField(HandTrailFlg, new GUIContent("ハンドトレイル削除"));
            EditorGUILayout.PropertyField(NailTrailFlg, new GUIContent("ネイルトレイル削除"));
            EditorGUILayout.PropertyField(PenCtrlFlg, new GUIContent("ペン操作削除"));
            EditorGUILayout.PropertyField(StatusFlg, new GUIContent("ステータス削除"));
            EditorGUILayout.PropertyField(WhiteBreathFlg, new GUIContent("ホワイトブレス削除"));

            EditorGUILayout.PropertyField(ColliderFlg, new GUIContent("コライダー・ジャンプ削除"));
            EditorGUILayout.PropertyField(AvatarLightFlg, new GUIContent("アバターライト削除"));
            EditorGUILayout.PropertyField(ClairvoyanceFlg, new GUIContent("透視削除"));
            EditorGUILayout.PropertyField(JointBallFlg, new GUIContent("JointBall削除"));
            EditorGUILayout.PropertyField(LightGunFlg, new GUIContent("ライトガン削除"));
            EditorGUILayout.PropertyField(TeleportFlg, new GUIContent("テレポート削除"));
            EditorGUILayout.PropertyField(TPSFlg, new GUIContent("TPS削除"));

            EditorGUILayout.PropertyField(FreeGimmickFlg, new GUIContent("フリーギミック削除"));
            EditorGUILayout.PropertyField(DrinkFlg, new GUIContent("飲み物削除"));
            EditorGUILayout.PropertyField(IssyouFlg, new GUIContent("一升瓶削除"));
            EditorGUILayout.PropertyField(HelpFlg, new GUIContent("Help削除"));
            EditorGUILayout.PropertyField(FaceEffectFlg, new GUIContent("FaceEffect削除"));

            EditorGUILayout.PropertyField(PictureFlg, new GUIContent("撮影ギミック削除"));
            EditorGUILayout.PropertyField(MenuFlg, new GUIContent("メニュー削除"));

            EditorGUILayout.PropertyField(
                FaceGestureFlg2,
                new GUIContent("デフォルトの表情プリセット削除(faceEmoなど使う場合)")
            );
            EditorGUILayout.PropertyField(FaceLockFlg, new GUIContent("表情固定機能削除"));
            EditorGUILayout.PropertyField(FaceValFlg, new GUIContent("顔差分変更機能削除"));

            FaceGestureFlg.boolValue =
                FaceLockFlg.boolValue || FaceValFlg.boolValue || FaceGestureFlg2.boolValue;
            EditorGUILayout.PropertyField(
                nadeFlg,
                new GUIContent("なでギミックをメニューから削除して常にON")
            );
            EditorGUILayout.PropertyField(
                kamitukiFlg,
                new GUIContent("噛みつきをメニューから削除して常にON")
            );
            EditorGUILayout.PropertyField(
                IKUSIA_emote,
                new GUIContent("IKUSIA_emoteをメニューのみ削除")
            );
            FaceContactFlg.boolValue = kamitukiFlg.boolValue || nadeFlg.boolValue;

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            questArea = EditorGUILayout.Foldout(questArea, "Quest用調整項目(素体のみ)", true);
            if (questArea)
            {
                var Mizuki = (MizukiOptimizer)target;
#if AVATAR_OPTIMIZER_FOUND
                if (Mizuki.transform.root.GetComponent<TraceAndOptimize>() == null)
                    EditorGUILayout.HelpBox(
                        "アバターにTraceAndOptimizeを追加してください",
                        MessageType.Error
                    );
#else
                EditorGUILayout.HelpBox(
                    "AvatarOptimizerが見つかりませんVCCに追加して有効化してください",
                    MessageType.Error
                );
#endif
                EditorGUILayout.HelpBox(
                    "Quest化に対応してないコンポーネントやシェーダーを使っているためペット、TPS、透視、コライダー・ジャンプ、撮影ギミック、ライトガン、ホワイトブレス、バブルブレス、ウォータースタンプ、8bit、ペン操作、ハートガン、ヘッドホンのparticle、AFKの演出の一部を削除します。\n"
                        + "",
                    MessageType.Info
                );
                EditorGUILayout.PropertyField(questFlg1, new GUIContent("quest用にギミックを削除"));

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
                    serializedObject.ApplyModifiedProperties();
                }
                if (GUILayout.Button("おすすめ設定にする"))
                {
                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
                    Head_002.boolValue = false;
                    Front_hair2_root.boolValue = true;
                    side_3_root.boolValue = true;
                    Side_root.boolValue = false;
                    Breast_collider.boolValue = true;
                    backhair.boolValue = false;
                    plane_collider.boolValue = true;
                    head_collider1.boolValue = false;
                    upperArm_collider.boolValue = true;
                    upperleg_collider1.boolValue = true;
                    chest_collider1.boolValue = true;
                    hair_2.boolValue = true;
                    Breast.boolValue = false;
                    side_1_root.boolValue = true;
                    sidehair.boolValue = true;
                    back_side_root.boolValue = true;
                    Skirt_Root.boolValue = true;
                    upperleg_collider2.boolValue = true;
                    tail_044.boolValue = false;
                    head_collider2.boolValue = true;
                    chest_collider2.boolValue = false;
                    upperleg_collider3.boolValue = false;
                    plane_tail_collider.boolValue = true;

                    tail_022.boolValue = true;

                    serializedObject.ApplyModifiedProperties();
                }

                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                GUILayout.BeginVertical();
                EditorGUILayout.PropertyField(
                    Head_002,
                    new GUIContent("前髪:Transform: " + physBoneList["Head_002"].AffectedCount)
                );
                EditorGUILayout.PropertyField(
                    Front_hair2_root,
                    new GUIContent(
                        "ぱっつん前髪:Transform : " + physBoneList["Front_hair2_root"].AffectedCount
                    )
                );

                EditorGUILayout.PropertyField(
                    side_3_root,
                    new GUIContent(
                        "前髪サイド:Transform : " + physBoneList["side_3_root"].AffectedCount
                    )
                );
                EditorGUILayout.PropertyField(
                    Side_root,
                    new GUIContent("サイド:Transform : " + physBoneList["Side_root"].AffectedCount)
                );
                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                GUILayout.BeginVertical();
                EditorGUILayout.PropertyField(
                    Breast_collider,
                    new GUIContent("胸部干渉 : " + physBoneList["Side_root"].ColliderCount)
                );
                if (Side_root.boolValue)
                    Breast_collider.boolValue = true;
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                EditorGUILayout.PropertyField(
                    backhair,
                    new GUIContent("後ろ髪:Transform : " + physBoneList["backhair"].AffectedCount)
                );
                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                GUILayout.BeginVertical();
                EditorGUILayout.PropertyField(
                    plane_collider,
                    new GUIContent("髪の地面干渉 : " + physBoneList["backhair"].ColliderCount)
                );
                EditorGUILayout.PropertyField(
                    head_collider1,
                    new GUIContent("頭部干渉 : " + physBoneList["backhair"].ColliderCount)
                );
                EditorGUILayout.PropertyField(
                    upperArm_collider,
                    new GUIContent("腕干渉 : " + physBoneList["backhair"].ColliderCount * 2)
                );
                EditorGUILayout.PropertyField(
                    upperleg_collider1,
                    new GUIContent("脚干渉 : " + physBoneList["backhair"].ColliderCount * 2)
                );
                EditorGUILayout.PropertyField(
                    chest_collider1,
                    new GUIContent("胸周り干渉 : " + physBoneList["backhair"].ColliderCount)
                );
                if (backhair.boolValue)
                {
                    plane_collider.boolValue = true;
                    head_collider1.boolValue = true;
                    upperArm_collider.boolValue = true;
                    upperleg_collider1.boolValue = true;
                    chest_collider1.boolValue = true;
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                EditorGUILayout.PropertyField(
                    hair_2,
                    new GUIContent("hair_2:Transform : " + physBoneList["hair_2"].AffectedCount)
                );
                EditorGUILayout.PropertyField(
                    Breast,
                    new GUIContent("胸:Transform : " + physBoneList["Breast"].AffectedCount)
                );

                EditorGUILayout.PropertyField(
                    side_1_root,
                    new GUIContent(
                        "前髪小:Transform : " + physBoneList["side_1_root"].AffectedCount
                    )
                );
                EditorGUILayout.PropertyField(
                    sidehair,
                    new GUIContent("横髪小:Transform : " + physBoneList["sidehair"].AffectedCount)
                );
                EditorGUILayout.PropertyField(
                    back_side_root,
                    new GUIContent(
                        "後ろ髪小:Transform : " + physBoneList["back_side_root"].AffectedCount
                    )
                );

                EditorGUILayout.PropertyField(
                    Skirt_Root,
                    new GUIContent(
                        "スカート:Transform : " + physBoneList["Skirt_Root"].AffectedCount
                    )
                );
                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                GUILayout.BeginVertical();
                EditorGUILayout.PropertyField(
                    upperleg_collider2,
                    new GUIContent("脚干渉 : " + physBoneList["Skirt_Root"].ColliderCount)
                );
                if (Skirt_Root.boolValue)
                {
                    upperleg_collider2.boolValue = true;
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                EditorGUILayout.PropertyField(
                    tail_044,
                    new GUIContent("尻尾:Transform : " + physBoneList["tail_044"].AffectedCount)
                );
                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                GUILayout.BeginVertical();
                EditorGUILayout.PropertyField(
                    head_collider2,
                    new GUIContent("頭部干渉 : " + physBoneList["tail_044"].ColliderCount)
                );
                EditorGUILayout.PropertyField(
                    chest_collider2,
                    new GUIContent("胸周り干渉 : " + physBoneList["tail_044"].ColliderCount)
                );
                EditorGUILayout.PropertyField(
                    upperleg_collider3,
                    new GUIContent("脚干渉 : " + physBoneList["tail_044"].ColliderCount * 2)
                );
                EditorGUILayout.PropertyField(
                    plane_tail_collider,
                    new GUIContent("尻尾の地面干渉 : " + physBoneList["tail_044"].ColliderCount)
                );
                if (tail_044.boolValue)
                {
                    head_collider2.boolValue = true;
                    chest_collider2.boolValue = true;
                    upperleg_collider3.boolValue = true;
                    plane_tail_collider.boolValue = true;
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                EditorGUILayout.PropertyField(
                    tail_022,
                    new GUIContent(
                        "尻尾リボン:Transform : " + physBoneList["tail_022"].AffectedCount
                    )
                );
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                int count = 200;
                if (Head_002.boolValue)
                    count -= physBoneList["Head_002"].AffectedCount;
                if (Front_hair2_root.boolValue)
                    count -= physBoneList["Front_hair2_root"].AffectedCount;
                if (side_1_root.boolValue)
                    count -= physBoneList["side_1_root"].AffectedCount;
                if (side_3_root.boolValue)
                    count -= physBoneList["side_3_root"].AffectedCount;
                if (Side_root.boolValue)
                    count -= physBoneList["Side_root"].AffectedCount;
                if (backhair.boolValue)
                    count -= physBoneList["backhair"].AffectedCount;
                if (back_side_root.boolValue)
                    count -= physBoneList["back_side_root"].AffectedCount;
                if (sidehair.boolValue)
                    count -= physBoneList["sidehair"].AffectedCount;
                if (hair_2.boolValue)
                    count -= physBoneList["hair_2"].AffectedCount;
                if (Breast.boolValue)
                    count -= physBoneList["Breast"].AffectedCount;
                if (Skirt_Root.boolValue)
                    count -= physBoneList["Skirt_Root"].AffectedCount;
                if (tail_044.boolValue)
                    count -= physBoneList["tail_044"].AffectedCount;
                if (tail_022.boolValue)
                    count -= physBoneList["tail_022"].AffectedCount;
                if (count > 64)
                    EditorGUILayout.HelpBox(
                        "影響transform数 :" + count + "/64 (64以下に調整してください)",
                        MessageType.Error
                    );
                else
                    EditorGUILayout.HelpBox("影響transform数 :" + count + "/64", MessageType.Info);

                int count2 = 271;
                if (Breast_collider.boolValue)
                    count2 -= physBoneList["Side_root"].ColliderCount;

                if (plane_collider.boolValue)
                    count2 -= physBoneList["backhair"].ColliderCount;
                if (head_collider1.boolValue)
                    count2 -= physBoneList["Side_root"].ColliderCount;
                if (upperArm_collider.boolValue)
                    count2 -= physBoneList["backhair"].ColliderCount * 2;
                if (upperleg_collider1.boolValue)
                    count2 -= physBoneList["backhair"].ColliderCount * 2;
                if (chest_collider1.boolValue)
                    count2 -= physBoneList["Side_root"].ColliderCount;

                if (chest_collider2.boolValue)
                    count2 -= physBoneList["tail_044"].ColliderCount;
                if (upperleg_collider3.boolValue)
                    count2 -= physBoneList["tail_044"].ColliderCount * 2;
                if (plane_tail_collider.boolValue)
                    count2 -= physBoneList["tail_044"].ColliderCount;
                if (head_collider2.boolValue)
                    count2 -= physBoneList["tail_044"].ColliderCount;

                if (upperleg_collider2.boolValue)
                    count2 -= physBoneList["Skirt_Root"].ColliderCount;

                if (count2 > 64)
                    EditorGUILayout.HelpBox(
                        "コライダー干渉数 :" + count2 + "/64 (64以下に調整してください)",
                        MessageType.Error
                    );
                else
                    EditorGUILayout.HelpBox(
                        "コライダー干渉数 :" + count2 + "/64",
                        MessageType.Info
                    );
                int selected = textureResize.enumValueIndex;
                textureResize.enumValueIndex = EditorGUILayout.Popup(
                    "メニュー画像解像度設定",
                    selected,
                    new[] { "下げる", "削除" }
                );

#if !AVATAR_OPTIMIZER_FOUND
                GUI.enabled = false;
                EditorGUILayout.HelpBox(
                    "AAOがインストールされている場合のみ「頬染めを削除」が有効になります。",
                    MessageType.Info
                );
#endif
                EditorGUILayout.PropertyField(AAORemoveFlg, new GUIContent("頬染めを削除"));
                GUI.enabled = true;
            }

            // Execute ボタンの追加
            if (GUILayout.Button("Execute"))
            {
                IKUSIAOverrideAbstract script = (IKUSIAOverrideAbstract)target;
                VRCAvatarDescriptor descriptor =
                    script.transform.root.GetComponent<VRCAvatarDescriptor>();
                if (descriptor != null)
                {
                    try
                    {
                        script.Execute(descriptor);
                    }
                    catch (System.Exception)
                    {
                        Debug.LogWarning("変換に失敗しました。再実行します。");
                        script.Execute(descriptor);
                    }
                }
                else
                {
                    Debug.LogWarning("VRCAvatarDescriptor が見つかりません。");
                }
            }
            EditorGUILayout.Space();
            GUILayout.TextField(
                "生成する元Asset",
                new GUIStyle
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 24,
                    normal = new GUIStyleState { textColor = Color.white },
                }
            );
            GUI.enabled = false;
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(controllerDef, new GUIContent("Animator Controller"));
            EditorGUILayout.PropertyField(menuDef, new GUIContent("Expressions Menu"));
            EditorGUILayout.PropertyField(paramDef, new GUIContent("Expression Parameters"));
            GUI.enabled = true;
            EditorGUILayout.Space();
            GUILayout.TextField(
                "生成されたAsset",
                new GUIStyle
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 24,
                    normal = new GUIStyleState { textColor = Color.white },
                }
            );
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(controller, new GUIContent("Animator Controller"));
            EditorGUILayout.PropertyField(menu, new GUIContent("Expressions Menu"));
            EditorGUILayout.PropertyField(param, new GUIContent("Expression Parameters"));

            // 変更内容の適用
            serializedObject.ApplyModifiedProperties();
        }

        [MenuItem("GameObject/illusive_tools/Create MizukiOptimizer Object", true)]
        private static bool ValidateCreateMizukiOptimizerObject(MenuCommand menuCommand)
        {
            return ValidateCreateObj(menuCommand, "mizukiAvatar");
        }

        [MenuItem("GameObject/illusive_tools/Create MizukiOptimizer Object", false, 10)]
        private static void CreateMizukiOptimizerObject(MenuCommand menuCommand)
        {
            CreateObj(menuCommand, "MizukiOptimizer");
        }
    }
}

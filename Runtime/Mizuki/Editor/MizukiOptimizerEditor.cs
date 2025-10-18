using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using Debug = UnityEngine.Debug;

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [CustomEditor(typeof(MizukiOptimizer))]
    [AddComponentMenu("")]
    internal partial class MizukiOptimizerEditor : IKUSIAOverrideEditor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            EditorGUILayout.PropertyField(heelFlg1, new GUIContent("ヒールON"));
            EditorGUILayout.PropertyField(heelFlg2, new GUIContent("ハイヒールON"));

            EditorGUILayout.PropertyField(FreeClothFlg, new GUIContent("フリー衣装削除"));
            EditorGUILayout.PropertyField(FreeObjFlg, new GUIContent("フリーオブジェクト削除"));

            EditorGUILayout.PropertyField(ClothFlg, new GUIContent("衣装メニュー削除"));
            GUI.enabled = true;
            if (!ClothFlg.boolValue)
                GUI.enabled = false;

            NailGaoFlg.boolValue = ClothFlg.boolValue;
            ArmAcceFlg.boolValue = ClothFlg.boolValue;
            BeltFlg.boolValue = ClothFlg.boolValue;
            LegBeltFlg.boolValue = ClothFlg.boolValue;
            OuterFlg.boolValue = ClothFlg.boolValue;
            ShoesFlg.boolValue = ClothFlg.boolValue;
            EyemaskFlg.boolValue = ClothFlg.boolValue;
            HeadDressFlg.boolValue = ClothFlg.boolValue;
            AccesaryFlg.boolValue = ClothFlg.boolValue;

            EditorGUILayout.PropertyField(OuterFlg2, new GUIContent(" │ ├ outerを削除"));
            EditorGUILayout.PropertyField(BeltFlg2, new GUIContent(" │ ├ beltを削除"));
            EditorGUILayout.PropertyField(LegBeltFlg2, new GUIContent(" │ ├ leg beltを削除"));
            EditorGUILayout.PropertyField(AccesaryFlg2, new GUIContent(" │ ├ アクセサリーを削除"));

            EditorGUILayout.PropertyField(ArmAcceFlg2, new GUIContent(" │ ├ Arm AcceをON"));
            EditorGUILayout.PropertyField(ShoesFlg2, new GUIContent(" │ ├ shoesをON"));
            EditorGUILayout.PropertyField(EyemaskFlg2, new GUIContent(" │ ├ アイマスクをON"));
            EditorGUILayout.PropertyField(HeadDressFlg2, new GUIContent(" │ ├ ヘッドドレスをON"));
            EditorGUILayout.PropertyField(NailGaoFlg2, new GUIContent(" │ └ Nail gao~をON"));
            EditorGUILayout.PropertyField(ClothDelFlg, new GUIContent(" └衣装削除"));
            GUI.enabled = true;
            if (!ClothDelFlg.boolValue)
                GUI.enabled = false;
            EditorGUILayout.PropertyField(ClothDelFlg2, new GUIContent("      └ 下着をON"));
            GUI.enabled = true;
            if (!ClothFlg.boolValue)
            {
                OuterFlg2.boolValue = false;
                BeltFlg2.boolValue = false;
                LegBeltFlg2.boolValue = false;
                ArmAcceFlg2.boolValue = false;
                ShoesFlg2.boolValue = false;
                EyemaskFlg2.boolValue = false;
                HeadDressFlg2.boolValue = false;
                NailGaoFlg2.boolValue = false;
                ClothDelFlg.boolValue = false;
                ClothDelFlg2.boolValue = false;
                AccesaryFlg2.boolValue = false;
            }
            if (!ClothDelFlg.boolValue)
            {
                ClothDelFlg2.boolValue = false;
            }
            else
            {
                OuterFlg2.boolValue = true;
                BeltFlg2.boolValue = true;
                LegBeltFlg2.boolValue = true;
                ArmAcceFlg2.boolValue = false;
                ShoesFlg2.boolValue = false;
                EyemaskFlg2.boolValue = false;
                HeadDressFlg2.boolValue = false;
            }

            EditorGUILayout.PropertyField(EarAngryFlg, new GUIContent("耳怒りメニュー削除"));

            EditorGUILayout.PropertyField(EarTailFlg, new GUIContent("耳しっぽメニュー削除"));
            if (!EarTailFlg.boolValue)
                GUI.enabled = false;
            EditorGUILayout.PropertyField(EarTailFlg2, new GUIContent("   └ 耳尻尾削除"));
            GUI.enabled = true;
            if (!EarTailFlg.boolValue)
            {
                EarTailFlg2.boolValue = false;
            }
            EditorGUILayout.PropertyField(ElfEarFlg, new GUIContent("エルフ耳メニュー削除"));

            if (!ElfEarFlg.boolValue)
                GUI.enabled = false;
            EditorGUILayout.PropertyField(ElfEarFlg2, new GUIContent("   └ エルフ耳をON"));
            GUI.enabled = true;
            if (!ElfEarFlg.boolValue)
            {
                ElfEarFlg2.boolValue = false;
            }
            EditorGUILayout.PropertyField(FronthairLeftFlg, new GUIContent("前髪左メニュー削除"));

            EditorGUILayout.PropertyField(FronthairRightFlg, new GUIContent("前髪右メニュー削除"));
            EditorGUILayout.PropertyField(HairFlg, new GUIContent("髪削除"));
            if (HairFlg.boolValue)
            {
                FronthairLeftFlg.boolValue = true;
                FronthairRightFlg.boolValue = true;
                AccesaryFlg2.boolValue = true;
            }

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
            EditorGUILayout.PropertyField(FaceEffectFlg, new GUIContent("FaceEffect削除"));

            EditorGUILayout.PropertyField(CameraPictureFlg, new GUIContent("撮影ギミック削除"));
            EditorGUILayout.PropertyField(PictureFlg, new GUIContent("撮影補助削除"));
            EditorGUILayout.PropertyField(MenuFlg, new GUIContent("メニュー削除"));
            EditorGUILayout.PropertyField(HelpFlg, new GUIContent("  └ Help削除"));
            FreeMenuFlg.boolValue =
                FreeClothFlg.boolValue
                && FreeObjFlg.boolValue
                && FreeGimmickFlg.boolValue
                && FreeParticleFlg.boolValue;
            EditorGUILayout.PropertyField(
                FaceGestureFlg2,
                new GUIContent("デフォルトの表情プリセット削除(faceEmoなど使う場合)")
            );
            EditorGUILayout.PropertyField(FaceLockFlg, new GUIContent("表情固定機能削除"));
            EditorGUILayout.PropertyField(FaceValFlg, new GUIContent("顔差分変更機能削除"));

            bool prevFaceGestureFlg =
                FaceLockFlg.boolValue || FaceValFlg.boolValue || FaceGestureFlg2.boolValue;
            if (FaceGestureFlg.boolValue != prevFaceGestureFlg)
                FaceGestureFlg.boolValue = prevFaceGestureFlg;

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
            bool prevFaceContactFlg = kamitukiFlg.boolValue || nadeFlg.boolValue;
            if (FaceContactFlg.boolValue != prevFaceContactFlg)
                FaceContactFlg.boolValue = prevFaceContactFlg;
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            Quest();

            // Execute ボタンの追加
            serializedObject.ApplyModifiedProperties();
            if (GUILayout.Button("Execute"))
            {
                if (isExecuting)
                {
                    Debug.LogWarning("現在実行中です。しばらくお待ちください。");
                    return;
                }
                isExecuting = true;
                var step1 = Stopwatch.StartNew();
                MizukiOptimizer script = (MizukiOptimizer)target;
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
                        Debug.LogWarning("変換に失敗しました。");
                    }
                }
                else
                {
                    Debug.LogWarning("VRCAvatarDescriptor が見つかりません。");
                }
                step1.Stop();
                Debug.Log("MizukiOptimizer: " + step1.ElapsedMilliseconds + "ms");
                isExecuting = false;
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
            EditorGUILayout.PropertyField(paryi_FXDef, new GUIContent("Animator Controller"));
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
            EditorGUILayout.PropertyField(paryi_FX, new GUIContent("Animator Controller"));
            EditorGUILayout.PropertyField(menu, new GUIContent("Expressions Menu"));
            EditorGUILayout.PropertyField(param, new GUIContent("Expression Parameters"));
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

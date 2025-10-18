#if UNITY_EDITOR
using VRC.SDK3.Avatars.Components;
#if AVATAR_OPTIMIZER_FOUND
using Anatawa12.AvatarOptimizer;
#endif
namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    public partial class MizukiOptimizer : IKUSIAOverrideAbstract
    {
        public bool questFlg1 = false;

        public bool Butt;
        public bool Skirt_Root;
        public bool Breast;
        public bool Cheek;
        public bool ahoge;
        public bool Backhair;
        public bool Front;
        public bool Frontside;
        public bool side;
        public bool headband_Root;
        public bool tang;
        public bool TigerEar;
        public bool Shoulder_Ribbon;
        public bool coat_hand;
        public bool Hand_frills;
        public bool tail;
        public bool Leg_frills;

        public bool Upperleg_collider1;
        public bool Upperleg_collider2;
        public bool Chest_collider;
        public bool Butt_collider;
        public bool UpperArm_collider1;
        public bool UpperArm_collider2;
        public bool Shoulder_collider;
        public bool AAORemoveFlg;

        protected override void Edit4Quest(VRCAvatarDescriptor descriptor)
        {
            if (questFlg1)
            {
                if (Butt)
                    DelPBByPathArray(descriptor, "Armature/Hips/Butt_L", "Armature/Hips/Butt_R");

                if (Skirt_Root)
                    DelPBByPathArray(
                        descriptor,
                        "Armature/Hips/Skirt_Root/Skirt_L.038",
                        "Armature/Hips/Skirt_Root/Skirt_Root_L",
                        "Armature/Hips/Skirt_Root/Skirt_Root_R"
                    );
                if (Upperleg_collider1)
                {
                    DelColliderSettingByPathArray(
                        descriptor,
                        new string[] { "Upperleg_L", "Upperleg_R" },
                        "Armature/Hips/Skirt_Root/Skirt_L.038",
                        "Armature/Hips/Skirt_Root/Skirt_Root_L",
                        "Armature/Hips/Skirt_Root/Skirt_Root_R"
                    );
                }
                if (Breast)
                    DelPBByPathArray(
                        descriptor,
                        "Armature/Hips/Spine/Chest/Breast_L",
                        "Armature/Hips/Spine/Chest/Breast_R"
                    );
                if (Shoulder_collider)
                {
                    DelColliderSettingByPathArray(
                        descriptor,
                        new string[] { "Shoulder_L", "Shoulder_R" },
                        "Armature/Hips/Spine/Chest/Breast_L",
                        "Armature/Hips/Spine/Chest/Breast_R"
                    );
                }
                if (UpperArm_collider1)
                {
                    DelColliderSettingByPathArray(
                        descriptor,
                        new string[] { "Upperarm_L", "Upperarm_R" },
                        "Armature/Hips/Spine/Chest/Breast_L",
                        "Armature/Hips/Spine/Chest/Breast_R"
                    );
                }
                if (Cheek)
                    DelPBByPathArray(
                        descriptor,
                        "Armature/Hips/Spine/Chest/Neck/Head/Cheek_L/Cheek_L.001",
                        "Armature/Hips/Spine/Chest/Neck/Head/Cheek_R/Cheek_R.001"
                    );
                if (ahoge)
                    DelPBByPathArray(
                        descriptor,
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/ahoge"
                    );
                if (Backhair)
                    DelPBByPathArray(
                        descriptor,
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair2_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair2_R",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair3_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair3_R",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair4_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair4_R",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair5",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/backhair7_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/backhair7_R",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair_R",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair_L"
                    );
                if (Chest_collider)
                {
                    DelColliderSettingByPathArray(
                        descriptor,
                        new string[] { "Chest" },
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair2_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair2_R",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair3_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair3_R",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair4_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair4_R",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair5",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/backhair7_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/backhair7_R",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair_R",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair_L"
                    );
                }
                if (Butt_collider)
                {
                    DelColliderSettingByPathArray(
                        descriptor,
                        new string[] { "Hips" },
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair2_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair2_R",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair3_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair3_R",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair4_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair4_R",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair5",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/backhair7_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/backhair7_R",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair_R",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Backhair_L"
                    );
                }
                if (Front)
                    DelPBByPathArray(
                        descriptor,
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Front",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Front1",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Front2",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Front_2"
                    );
                if (Frontside)
                    DelPBByPathArray(
                        descriptor,
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Frontside1_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Frontside1_R",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Frontside2_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/Frontside2_R"
                    );
                if (side)
                    DelPBByPathArray(
                        descriptor,
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side3_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side3_L.001",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side3_L.002",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side3_R",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side3_R.001",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side3_R.002",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side5_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side5_R",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side6_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side6_R",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side_Root_L",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side_Root_R"
                    );
                if (headband_Root)
                    DelPBByPathArray(
                        descriptor,
                        "Armature/Hips/Spine/Chest/Neck/Head/headband_Root"
                    );
                if (tang)
                    DelPBByPathArray(descriptor, "Armature/Hips/Spine/Chest/Neck/Head/tang");
                if (TigerEar)
                    DelPBByPathArray(
                        descriptor,
                        "Armature/Hips/Spine/Chest/Neck/Head/TigerEar/L/ear.L",
                        "Armature/Hips/Spine/Chest/Neck/Head/TigerEar/R/ear.R"
                    );
                if (Shoulder_Ribbon)
                    DelPBByPathArray(
                        descriptor,
                        "Armature/Hips/Spine/Chest/Shoulder_L/Shoulder_Ribbon_FrontRoot_L",
                        "Armature/Hips/Spine/Chest/Shoulder_R/Shoulder_Ribbon_FrontRoot_R",
                        "Armature/Hips/Spine/Chest/Shoulder_Ribbon_BackRoot_L",
                        "Armature/Hips/Spine/Chest/Shoulder_Ribbon_BackRoot_R"
                    );
                if (UpperArm_collider2)
                {
                    DelColliderSettingByPathArray(
                        descriptor,
                        new string[] { "Upperarm_L", "Upperarm_R" },
                        "Armature/Hips/Spine/Chest/Shoulder_L/Shoulder_Ribbon_FrontRoot_L",
                        "Armature/Hips/Spine/Chest/Shoulder_R/Shoulder_Ribbon_FrontRoot_R",
                        "Armature/Hips/Spine/Chest/Shoulder_Ribbon_BackRoot_L",
                        "Armature/Hips/Spine/Chest/Shoulder_Ribbon_BackRoot_R"
                    );
                }
                if (coat_hand)
                    DelPBByPathArray(
                        descriptor,
                        "Armature/Hips/Spine/Chest/Shoulder_R/Upperarm_R/Lowerarm_R/Right Hand/coat_hand_root_R",
                        "Armature/Hips/Spine/Chest/Shoulder_L/Upperarm_L/Lowerarm_L/Left Hand/coat_hand_root_L"
                    );
                if (Hand_frills)
                    DelPBByPathArray(
                        descriptor,
                        "Armature/Hips/Spine/Chest/Shoulder_R/Upperarm_R/Lowerarm_R/Right Hand/Hand_frills_R",
                        "Armature/Hips/Spine/Chest/Shoulder_L/Upperarm_L/Lowerarm_L/Left Hand/Hand_frills_L"
                    );
                if (tail)
                    DelPBByPathArray(descriptor, "Armature/Hips/tail/tail.001");
                if (Upperleg_collider2)
                {
                    DelColliderSettingByPathArray(
                        descriptor,
                        new string[] { "Upperleg_L", "Upperleg_R" },
                        "Armature/Hips/tail/tail.001"
                    );
                }
                if (Leg_frills)
                    DelPBByPathArray(
                        descriptor,
                        "Armature/Hips/Upperleg_L/Lowerleg_L/Foot_L/Leg_frills_Root_L"
                    );

                if (Upperleg_collider2)
                {
                    DelColliderSettingByPathArray(
                        descriptor,
                        new string[] { "chest_collider" },
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_R/backhair_R",
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/back_hair_root/back_hair_root_L/backhair_L"
                    );
                }
                if (AAORemoveFlg)
                {
#if AVATAR_OPTIMIZER_FOUND
                    if (
                        !descriptor
                            .transform.Find("Body")
                            .TryGetComponent<RemoveMeshByBlendShape>(out var removeMesh)
                    )
                    {
                        removeMesh = descriptor
                            .transform.Find("Body")
                            .gameObject.AddComponent<RemoveMeshByBlendShape>();
                        removeMesh.Initialize(1);
                    }
                    removeMesh.ShapeKeys.Add("照れ");
#endif
                }
            }
        }
    }
}
#endif

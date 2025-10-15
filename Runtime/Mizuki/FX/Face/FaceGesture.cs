using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    internal class FaceGesture : MizukiBase
    {
        public bool FaceGestureFlg = false;
        public bool FaceLockFlg = false;
        public bool FaceValFlg = false;
        public bool kamitukiFlg = false;
        public bool nadeFlg = false;
        public bool blinkFlg = false;
        internal static new readonly List<string> Layers = new()
        {
            "Left Right Hand",
            "Blink_Control",
            "FaceCtrl",
            "LipSynk",
        };
        internal static readonly List<string> FaceVariation = new()
        {
            "FaceVariation1",
            "FaceVariation2",
            "FaceVariation3",
        };
        internal static new readonly List<string> Parameters = new()
        {
            "FaceLock",
            "FaceVariation1",
            "FaceVariation2",
            "FaceVariation3",
        };
        private static readonly List<string> FistR = new() { "Fist R1", "Fist R2", "Fist R3" };
        private static readonly List<string> FistL = new() { "Fist L1", "Fist L2", "Fist L3" };

        public void Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController paryi_FX,
            MizukiOptimizer optimizer
        )
        {
            this.descriptor = descriptor;
            this.paryi_FX = paryi_FX;
            FaceGestureFlg = optimizer.FaceGestureFlg2;
            FaceLockFlg = optimizer.FaceLockFlg;
            FaceValFlg = optimizer.FaceValFlg;
        }

        public new void DeleteFx(List<string> Layers)
        {
            if (!FaceGestureFlg && FaceLockFlg)
                foreach (
                    var layer in paryi_FX.layers.Where(layer =>
                        layer.name is "Left Right Hand" or "Blink_Control"
                    )
                )
                {
                    if (layer.name is "Blink_Control")
                    {
                        var states = layer.stateMachine.states;

                        foreach (var state in states)
                        {
                            if (state.state.name == "blinkctrl")
                            {
                                foreach (var t in state.state.transitions)
                                    t.conditions = t
                                        .conditions.Where(c => c.parameter != "FaceLock")
                                        .ToArray();
                            }
                        }
                        layer.stateMachine.states = states;
                    }
                    if (layer.name is "Left Right Hand")
                    {
                        var states = layer.stateMachine.states;

                        foreach (var state in states)
                        {
                            if (FistR.Contains(state.state.name))
                            {
                                var parentBlendTree = state.state.motion as BlendTree;
                                var newMotion = AssetDatabase.LoadAssetAtPath<Motion>(
                                    AssetDatabase.GUIDToAssetPath(
                                        "df86b31028c0faa419461abeea3fba9f"
                                    )
                                );
                                state.state.motion = newMotion;
                            }
                            if (FistL.Contains(state.state.name))
                            {
                                var parentBlendTree = state.state.motion as BlendTree;
                                var newMotion = AssetDatabase.LoadAssetAtPath<Motion>(
                                    AssetDatabase.GUIDToAssetPath(
                                        "55f0b1192a1fe4646916b6c9274f7f37"
                                    )
                                );
                                state.state.motion = newMotion;
                            }
                        }
                    }
                    var stateMachine = layer.stateMachine;
                    foreach (var t in stateMachine.anyStateTransitions)
                        t.conditions = t.conditions.Where(c => c.parameter != "FaceLock").ToArray();
                }
            if (!FaceGestureFlg && FaceValFlg)
                foreach (
                    var layer in paryi_FX.layers.Where(layer =>
                        layer.name is "Left Right Hand" or "Blink_Control" or "FaceCtrl"
                    )
                )
                {
                    RemoveStatesAndTransitions(
                        layer.stateMachine,
                        layer
                            .stateMachine.states.Where(state =>
                                state.state.name
                                    is "Fist L2"
                                        or "Open L2"
                                        or "Point L2"
                                        or "Peace L2"
                                        or "RockNRoll L2"
                                        or "Gun L2"
                                        or "Thumbs up L2"
                                        or "Fist R2"
                                        or "Open R2"
                                        or "Point R2"
                                        or "Peace R2"
                                        or "RockNRoll R2"
                                        or "Gun R2"
                                        or "Thumbs up R2"
                                        or "Fist L3"
                                        or "Open L3"
                                        or "Point L3"
                                        or "Peace L3"
                                        or "RockNRoll L3"
                                        or "Gun L3"
                                        or "Thumbs up L3"
                                        or "Fist R3"
                                        or "Open R3"
                                        or "Point R3"
                                        or "Peace R3"
                                        or "RockNRoll R3"
                                        or "Gun R3"
                                        or "Thumbs up R3"
                            )
                            .Select(s => s.state)
                            .ToArray()
                    );

                    if (layer.name is "Blink_Control")
                    {
                        var states = layer.stateMachine.states;

                        foreach (var state in states)
                        {
                            if (state.state.name == "blinkctrl")
                            {
                                foreach (var t in state.state.transitions)
                                    t.conditions = t
                                        .conditions.Where(c => !FaceVariation.Contains(c.parameter))
                                        .ToArray();
                                state.state.transitions = state.state.transitions[0..2];
                            }
                        }
                        layer.stateMachine.states = states;
                    }
                    paryi_FX.layers = paryi_FX
                        .layers.Where(layer => !("FaceCtrl" == layer.name))
                        .ToArray();
                }

            if (!FaceGestureFlg)
                return;

            paryi_FX.layers = paryi_FX
                .layers.Where(layer => !Layers.Contains(layer.name))
                .ToArray();
        }

        public new void EditVRCExpressions(VRCExpressionsMenu menu, List<string> menuPath)
        {
            foreach (var control in menu.controls)
            {
                if (control.name == "Gimmick")
                {
                    var expressionsSubMenu = control.subMenu;

                    foreach (var control2 in expressionsSubMenu.controls)
                    {
                        if (control2.name == "Face")
                        {
                            var expressionsSub2Menu = control2.subMenu;
                            if (FaceGestureFlg || FaceLockFlg)
                                foreach (var control3 in expressionsSub2Menu.controls)
                                {
                                    if (control3.name is "FaceLock")
                                    {
                                        expressionsSub2Menu.controls.Remove(control3);
                                        break;
                                    }
                                }
                            if (FaceGestureFlg || FaceValFlg)
                                foreach (var control3 in expressionsSub2Menu.controls)
                                {
                                    if (control3.name is "Face_variation")
                                    {
                                        expressionsSub2Menu.controls.Remove(control3);
                                        break;
                                    }
                                }
                            control2.subMenu = expressionsSub2Menu;
                            break;
                        }
                    }
                    control.subMenu = expressionsSubMenu;
                    break;
                }
            }
        }
    }
}
#endif

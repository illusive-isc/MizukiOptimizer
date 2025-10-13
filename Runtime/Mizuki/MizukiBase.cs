#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    public class MizukiBase : IKUSIAOverrideCore
    {
        protected void DeleteBarCtrlHandHit(List<string> Parameters, params string[] stateNames)
        {
            for (int i = 0; i < animator.layers.Length; i++)
            {
                var layer = animator.layers[i];
                if (layer.name is "BarCtrlHandHit")
                {
                    layer.stateMachine.states = layer
                        .stateMachine.states.Where(s =>
                        {
                            if (stateNames.Contains(s.state.name))
                                return false;
                            foreach (var behaviour in s.state.behaviours)
                                if (behaviour is VRCAvatarParameterDriver paramDriver)
                                    foreach (var parameter in paramDriver.parameters)
                                        return !Parameters.Contains(parameter.name);

                            return true;
                        })
                        .ToArray();
                    if (layer.stateMachine.states.Length == 1)
                        animator.RemoveLayer(i);
                }
            }
        }

        protected void DeleteMenuButtonCtrl(List<string> Parameters)
        {
            animator
                .layers.Where(layer => layer.name == "MenuButtonCtrl")
                .ToList()
                .ForEach(layer => ProcessMenuButtonCtrlLayer(layer, Parameters));
        }

        private void ProcessMenuButtonCtrlLayer(
            AnimatorControllerLayer layer,
            List<string> Parameters
        )
        {
            var statesToRemove = layer
                .stateMachine.states.Where(s => s.state.name == "off")
                .SelectMany(s => s.state.transitions)
                .Where(transition =>
                    transition.conditions.Any(c => Parameters.Contains(c.parameter))
                )
                .Where(transition => transition.destinationState != null)
                .Select(transition => transition.destinationState.name)
                .ToHashSet();

            layer.stateMachine.states = layer
                .stateMachine.states.Where(s => !statesToRemove.Contains(s.state.name))
                .ToArray();
        }
    }
}
#endif

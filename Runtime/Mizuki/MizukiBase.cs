#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace jp.illusive_isc.IKUSIAOverride.Mizuki
{
    [AddComponentMenu("")]
    public class MizukiBase : IKUSIAOverrideCore
    {
        protected void DeleteBarCtrlHandHit(List<string> Parameters, params string[] stateNames)
        {
            for (int i = 0; i < paryi_FX.layers.Length; i++)
            {
                var layer = paryi_FX.layers[i];
                if (layer.name is "BarCtrlHandHit")
                {
                    RemoveStatesAndTransitions(
                        layer.stateMachine,
                        layer
                            .stateMachine.states.Where(s =>
                            {
                                if (stateNames.Contains(s.state.name))
                                    return true;
                                foreach (var behaviour in s.state.behaviours)
                                    if (behaviour is VRCAvatarParameterDriver paramDriver)
                                        foreach (var parameter in paramDriver.parameters)
                                            return Parameters.Contains(parameter.name);

                                return false;
                            })
                            .Select(s => s.state)
                            .ToArray()
                    );
                    if (layer.stateMachine.states.Length == 1)
                        paryi_FX.RemoveLayer(i);
                }
            }
        }

        protected void DeleteBarCtrl(params string[] stateNames)
        {
            for (int i = 0; i < paryi_FX.layers.Length; i++)
            {
                var layer = paryi_FX.layers[i];
                if (layer.name is "BarCtrl")
                {
                    RemoveStatesAndTransitions(
                        layer.stateMachine,
                        layer
                            .stateMachine.states.Where(s =>
                            {
                                if (stateNames.Contains(s.state.name))
                                    return true;

                                return false;
                            })
                            .Select(s => s.state)
                            .ToArray()
                    );
                    if (layer.stateMachine.states.Length <= 2)
                        paryi_FX.RemoveLayer(i);
                }
            }
        }

        protected void DeleteMenuButtonCtrl(List<string> Parameters)
        {
            var layer = paryi_FX.layers.FirstOrDefault(l => l.name == "MenuButtonCtrl");
            if (layer != null)
            {
                var offState = layer.stateMachine.states.FirstOrDefault(s => s.state.name == "off");
                if (offState.state != null)
                {
                    RemoveStatesAndTransitions(
                        layer.stateMachine,
                        offState
                            .state.transitions.Where(t =>
                                t.destinationState != null
                                && t.conditions.Any(c => Parameters.Contains(c.parameter))
                            )
                            .Select(t => t.destinationState)
                            .ToArray()
                    );
                }
            }
        }
    }
}
#endif

using nadena.dev.ndmf;
using UnityEngine;

namespace jp.illusive_isc.MizukiOptimizer
{
    public class IllMizukiOptimizerPass : Pass<IllMizukiOptimizerPass>
    {
        protected override void Execute(BuildContext context)
        {
            foreach (
                IllMizukiOptimizer IllMizukiOptimizer in context.AvatarRootObject.GetComponentsInChildren<IllMizukiOptimizer>()
            )
            {
                Object.DestroyImmediate(IllMizukiOptimizer.gameObject);
            }
        }
    }
}

#if NDMF_FOUND
using nadena.dev.ndmf;
using UnityEngine;

namespace jp.illusive_isc.MizukiOptimizer
{
    public class MizukiOptimizerPass : Pass<MizukiOptimizerPass>
    {
        protected override void Execute(BuildContext context)
        {
            foreach (
                MizukiOptimizer MizukiOptimizer in context.AvatarRootObject.GetComponentsInChildren<MizukiOptimizer>()
            )
            {
                Object.DestroyImmediate(MizukiOptimizer.gameObject);
            }
        }
    }
}
#endif

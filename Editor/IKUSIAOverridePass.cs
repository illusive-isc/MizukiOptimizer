#if NDMF_FOUND
using jp.illusive_isc.IKUSIAOverride.Mizuki;
using nadena.dev.ndmf;
using UnityEngine;

namespace jp.illusive_isc.IKUSIAOverride
{
    public class IKUSIAOverridePass : Pass<IKUSIAOverridePass>
    {
        protected override void Execute(BuildContext context)
        {
            foreach (
                IKUSIAOverrideAbstract Override in context.AvatarRootObject.GetComponentsInChildren<IKUSIAOverrideAbstract>()
            )
                Object.DestroyImmediate(Override.gameObject);
        }
    }
}
#endif

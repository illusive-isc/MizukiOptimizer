#if NDMF_FOUND
using jp.illusive_isc.IKUSIAOverride;
using nadena.dev.ndmf;

[assembly: ExportsPlugin(typeof(IKUSIAOverrideDifinition))]

namespace jp.illusive_isc.IKUSIAOverride
{
    public class IKUSIAOverrideDifinition : Plugin<IKUSIAOverrideDifinition>
    {
        public override string QualifiedName => "IllusoryOverride.IKUSIAOverride";
        public override string DisplayName => "IKUSIAOverride";

        protected override void Configure()
        {
            InPhase(BuildPhase.Transforming)
                .BeforePlugin("nadena.dev.modular-avatar")
                .Run(IKUSIAOverridePass.Instance);
        }
    }
}
#endif

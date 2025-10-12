#if NDMF_FOUND
using jp.illusive_isc.MizukiOptimizer;
using nadena.dev.ndmf;

[assembly: ExportsPlugin(typeof(MizukiOptimizerDifinition))]

namespace jp.illusive_isc.MizukiOptimizer
{
    public class MizukiOptimizerDifinition : Plugin<MizukiOptimizerDifinition>
    {
        public override string QualifiedName => "IllusoryOverride.MizukiOptimizer";
        public override string DisplayName => "MizukiOptimizer";

        protected override void Configure()
        {
            InPhase(BuildPhase.Transforming)
                .BeforePlugin("nadena.dev.modular-avatar")
                .Run(MizukiOptimizerPass.Instance);
        }
    }
}
#endif

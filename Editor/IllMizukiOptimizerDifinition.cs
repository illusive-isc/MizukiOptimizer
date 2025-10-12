using jp.illusive_isc.MizukiOptimizer;
using nadena.dev.ndmf;

[assembly: ExportsPlugin(typeof(IllMizukiOptimizerDifinition))]

namespace jp.illusive_isc.MizukiOptimizer
{
    public class IllMizukiOptimizerDifinition : Plugin<IllMizukiOptimizerDifinition>
    {
        public override string QualifiedName => "IllusoryOverride.IllMizukiOptimizer";
        public override string DisplayName => "MizukiOptimizer";

        protected override void Configure()
        {
            InPhase(BuildPhase.Resolving)
                .BeforePlugin("com.anatawa12.avatar-optimizer")
                .Run(IllMizukiOptimizerPass.Instance);
        }
    }
}

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Sample.DiscountExempt.Pipelines.Blocks;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Carts;
using Sitecore.Commerce.Plugin.Fulfillment;
using Sitecore.Commerce.Plugin.Promotions;
using Sitecore.Framework.Configuration;
using Sitecore.Framework.Pipelines.Definitions.Extensions;

namespace Plugin.Sample.DiscountExempt
{
    public class ConfigureSitecore : IConfigureSitecore
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.RegisterAllPipelineBlocks(assembly);

            services.Sitecore().Pipelines(
                config =>
                    config
                        .ConfigurePipeline<IApplyPromotionsBenefitsPipeline>(
                            d =>
                            {
                                d.Add<StashDiscountExemptCartlines>().After<EnsureFreeGiftsBlock>();
                                d.Add<ApplyStashedCartLines>().After<ApplyFreeGiftsBlock>();
                            })
                        .ConfigurePipeline<IRunningPluginsPipeline>(c =>
                        {
                            c.Add<Pipelines.Blocks.RegisteredPluginBlock>().After<RunningPluginsBlock>();
                        }));
        }
    }
}
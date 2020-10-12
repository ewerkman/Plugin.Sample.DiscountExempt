using System;
using System.Collections.Generic;
using Plugin.Sample.DiscountExempt.Models;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Carts;
using Sitecore.Commerce.Plugin.Promotions;
using Sitecore.Framework.Pipelines;

namespace Plugin.Sample.DiscountExempt.Pipelines.Blocks
{
    public class ApplyStashedCartLinesBlock : SyncPipelineBlock<Boolean, Boolean, CommercePipelineExecutionContext>
    {
        public override Boolean Run(Boolean arg, CommercePipelineExecutionContext context)
        {
            var cart = context.CommerceContext.GetObject<Cart>();
            var stashedCartLines = context.CommerceContext.GetObject<StashedCartLinesModel>();

            foreach (var stashedCartLine in stashedCartLines.CartLines)
            {
                cart.Lines.Add(stashedCartLine);    
            }
            
            context.CommerceContext.RemoveObjects<StashedCartLinesModel>();

            return arg;
        }
    }
}
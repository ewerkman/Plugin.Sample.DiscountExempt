using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Sample.DiscountExempt.Models;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Carts;
using Sitecore.Commerce.Plugin.Promotions;
using Sitecore.Framework.Pipelines;

namespace Plugin.Sample.DiscountExempt.Pipelines.Blocks
{
    public class ApplyStashedCartLinesBlock : SyncPipelineBlock<Boolean, Boolean, CommercePipelineExecutionContext>
    {
        private readonly CartCommander cartCommander;

        public ApplyStashedCartLinesBlock(CartCommander cartCommander)
        {
            this.cartCommander = cartCommander;
        }
        
        public override Boolean Run(Boolean arg, CommercePipelineExecutionContext context)
        {
            var cart = context.CommerceContext.GetObject<Cart>();
            var stashedCartLines = context.CommerceContext.GetObject<StashedCartLinesModel>();
            if (stashedCartLines != null)
            {
                foreach (var stashedCartLine in stashedCartLines.CartLines)
                {
                    cart.Lines.Add(stashedCartLine);
                }

                context.CommerceContext.RemoveObjects<StashedCartLinesModel>();
                
                // Recalculate the cart based on the re-added lines
                var subTotalAdjustment = stashedCartLines.CartLines.Sum(l => l.Totals.SubTotal.Amount);
                cart.Totals.SubTotal.Amount += subTotalAdjustment;
            }

            return arg;
        }
    }
}
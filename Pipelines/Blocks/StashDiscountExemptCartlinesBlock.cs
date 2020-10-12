using System;
using System.Collections.Generic;
using System.Linq;
using Plugin.Sample.DiscountExempt.Models;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Carts;
using Sitecore.Commerce.Plugin.Promotions;
using Sitecore.Framework.Pipelines;

namespace Plugin.Sample.DiscountExempt.Pipelines.Blocks
{
    public class StashDiscountExemptCartlinesBlock : SyncPipelineBlock<IEnumerable<Promotion>, IEnumerable<Promotion>, CommercePipelineExecutionContext>
    {
        public override IEnumerable<Promotion> Run(IEnumerable<Promotion> arg, CommercePipelineExecutionContext context)
        {
            var cart = context.CommerceContext.GetObject<Cart>();
            if (cart == null)
            {
                return arg;
            }
            
            var discountExemptCartLines = new List<CartLineComponent>();
            var discountExemptLines = cart.Lines.Where(x =>
                x.GetComponent<CartProductComponent>().Tags.Any(t => t.Name.Equals("discountexempt", StringComparison.InvariantCultureIgnoreCase)));
            
            if (discountExemptLines.Any())
            {
                foreach (var line in discountExemptLines.ToList())
                {
                    // pull out free gifts and cache them before evaluate promotions
                    discountExemptCartLines.Add(line);
                    cart.Lines.Remove(line);
                }
            }

            if (discountExemptCartLines.Any())
            {
                context.CommerceContext.AddUniqueObjectByType(new StashedCartLinesModel(discountExemptCartLines));
            }

            return arg; 
        }

    }
}
using System.Collections.Generic;
using Sitecore.Commerce.Plugin.Carts;

namespace Plugin.Sample.DiscountExempt.Models
{
    public class StashedCartLinesModel
    {
        public List<CartLineComponent> CartLines { get; } = new List<CartLineComponent>();

        public StashedCartLinesModel(List<CartLineComponent> cartLines)
        {
            CartLines = cartLines;
        }
    }
}
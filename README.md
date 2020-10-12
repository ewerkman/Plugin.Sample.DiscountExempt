# Plugin.Sample.DiscountExempt
Sample Sitecore Commerce Plugin that you can use to mark products as not eligible for promotions. 

__Created for version__: XC 10

This plugin adds two blocks to `IApplyPromotionsBenefitsPipeline`:
* `StashDiscountExemptCartlinesBlock`  
The `StashDiscountExemptCartlinesBlock` looks at the tags of a product (based on the `Tags` property on the `CartProductComponent` 
and if one of the tags is `discountexempt` it will temporarily remove the line from the cart so it will not be evaluated for a promotion.
* `ApplyStashedCartLinesBlock`  
Once the promotions have been applied, the `ApplyStashedCartLinesBlock` adds the stashed (discount exempt) lines again to the cart for normal calculation.

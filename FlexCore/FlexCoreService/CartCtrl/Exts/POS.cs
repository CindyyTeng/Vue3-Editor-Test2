using FlexCoreService.CartCtrl.Exts.Discount_dll;

namespace FlexCoreService.CartCtrl.Exts
{
    public class POS
	{
		public readonly List<BaseDiscountStrategy> ActivedRules = new List<BaseDiscountStrategy>();

		public bool CheckoutProcess(CartContext cart)
		{
			// reset cart
			cart.AppliedDiscounts.Clear();
			cart.OriginalTotalAmount = (decimal)(cart.CartItems.Select(p => p.SubTotal.Value).Sum());
			cart.TotalPrice = cart.OriginalTotalAmount;
			foreach (var rule in this.ActivedRules)
			{
				
				var discounts = rule.Process(cart);
				if(discounts != null)
				{
					cart.AppliedDiscounts.Add(discounts);
					cart.TotalPrice -= discounts.Amount;
				}
			}
			if (cart.Coupon != null)
			{
				cart.Coupon.Process(cart);
			}

			if (cart.TotalPrice > 2000)
			{
				cart.DeliveryFee = 0;
			}
			
			cart.TotalPrice += cart.DeliveryFee;
			return true;
		}
	}
}

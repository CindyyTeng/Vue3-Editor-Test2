using FlexCoreService.CartCtrl.Exts.Discount_dll;
using FlexCoreService.CartCtrl.Models.vm;
using Newtonsoft.Json.Linq;

namespace FlexCoreService.CartCtrl.Exts
{
    public class PercentageDiscount : BaseDiscountStrategy
	{
		// 門檻金額
		private readonly int _itemsAmount;
		// 折扣, 20表示八折
		private readonly int _percentOff;
		public PercentageDiscount(ProductDiscountVM vm) : base(vm)
		{
			_itemsAmount = vm.ConditionValue.Value;
			_percentOff = vm.DiscountValue.Value;
		}

		public override ItemDiscount Process(CartContext cart)
		{
			//throw new NotImplementedException();
			List<CartItemVM> matchedProducts = new List<CartItemVM>();

			foreach (CartItemVM p in cart.CartItems)
			{
				if (p.Product.MatchDiscounts.Any(x => x.DiscountId == this.Id))
				{
					matchedProducts.Add(p);
				}
			}
			var totalAmount = matchedProducts.Sum(x => x.SubTotal);

			if (totalAmount >= _itemsAmount)
			{
				return new ItemDiscount()
				{
					Rule = this,
					Products = matchedProducts.Select(x => x.Product).ToArray(),
					Amount = (decimal)matchedProducts.Sum(x => x.SubTotal) * _percentOff / 100
				};
			}
			return null;
		}
	}
}

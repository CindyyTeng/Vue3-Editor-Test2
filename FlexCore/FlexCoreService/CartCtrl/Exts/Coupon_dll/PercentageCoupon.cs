using FlexCoreService.CartCtrl.Models.vm;

namespace FlexCoreService.CartCtrl.Exts.Coupon_dll
{
	public class PercentageCoupon : BaseCouponStrategy
	{
		// 門檻金額
		private readonly int _itemsAmount;
		// 折扣, 20表示八折
		private readonly int _percentOff;
		public PercentageCoupon(CouponVM vm) : base(vm)
		{
			_itemsAmount = vm.MinimumPurchaseAmount;
			_percentOff = vm.DiscountValue;
		}

		public override void Process(CartContext cart)
		{
			if (cart.TotalPrice > _itemsAmount)
			{
				cart.CouponValue = (decimal)cart.TotalPrice * _percentOff / 100;
				cart.TotalPrice -= cart.CouponValue;
			}
		}
	}
}

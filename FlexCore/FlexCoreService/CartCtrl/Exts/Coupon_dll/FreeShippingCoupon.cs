using FlexCoreService.CartCtrl.Models.vm;

namespace FlexCoreService.CartCtrl.Exts.Coupon_dll
{
	public class FreeShippingCoupon : BaseCouponStrategy
	{
		// 門檻金額
		private readonly int _itemsAmount;
		// 折扣金額
		public FreeShippingCoupon(CouponVM vm) : base(vm)
		{
			_itemsAmount = vm.MinimumPurchaseAmount;
		}

		public override void Process(CartContext cart)
		{
			if (cart.TotalPrice > _itemsAmount)
			{
				cart.CouponValue = cart.DeliveryFee;
				cart.DeliveryFee = 0;
			}
		}
	}

}

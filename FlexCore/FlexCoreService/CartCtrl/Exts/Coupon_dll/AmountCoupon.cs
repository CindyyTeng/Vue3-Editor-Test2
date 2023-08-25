using FlexCoreService.CartCtrl.Models.vm;

namespace FlexCoreService.CartCtrl.Exts.Coupon_dll
{
    public class AmountCoupon : BaseCouponStrategy
	{
        // 門檻金額
        private readonly int _itemsAmount;
        // 折扣金額
        private readonly int _discountAmount;
        public AmountCoupon(CouponVM vm) : base(vm)
        {
			_itemsAmount = vm.MinimumPurchaseAmount;
            _discountAmount = vm.DiscountValue;
        }

        public override void Process(CartContext cart)
        {
            if (cart.TotalPrice > _itemsAmount)
            {
                cart.CouponValue = _discountAmount;
                cart.TotalPrice -= _discountAmount;
            }
        }
    }
}

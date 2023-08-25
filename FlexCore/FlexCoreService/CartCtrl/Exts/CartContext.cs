using EFModels.Models;
using FlexCoreService.CartCtrl.Exts.Coupon_dll;
using FlexCoreService.CartCtrl.Exts.Discount_dll;
using FlexCoreService.CartCtrl.Models.vm;

namespace FlexCoreService.CartCtrl.Exts
{
    public class CartContext
	{
		public IEnumerable<CartItemVM> CartItems { get; }
		public List<ItemDiscount> AppliedDiscounts { get; set; }
        public BaseCouponStrategy? Coupon { get; set; }
        public decimal OriginalTotalAmount { get; set; }
        public decimal DeliveryFee { get; set; }
		public decimal CouponValue { get; set; }
		public decimal TotalPrice { get; set; }
		public CheckoutDataVM checkoutData { get; set; }
        public CartContext(IEnumerable<CartItemVM> vms, BaseCouponStrategy? coupon = null)
		{
			CartItems = vms;
			AppliedDiscounts = new List<ItemDiscount>();
			TotalPrice = 0m;
			CouponValue = 0m;
			DeliveryFee = 500;
			Coupon = coupon;
            checkoutData = new CheckoutDataVM();

        }
	}
}

using EFModels.Models;
using FlexCoreService.CartCtrl.Models.vm;

namespace FlexCoreService.CartCtrl.Exts.Coupon_dll
{
    public abstract class BaseCouponStrategy
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public int SendingId { get; set; }

		public BaseCouponStrategy(CouponVM vm)
        {
            Id = vm.Id;
            Name = vm.CouponName;
            SendingId = vm.SendingId;
        }
        public abstract void Process(CartContext cart);
    }
}

using FlexCoreService.CartCtrl.Models.vm;

namespace FlexCoreService.CartCtrl.Exts.Discount_dll
{
    public abstract class BaseDiscountStrategy
    {
        public int Id;
        public string? Name;
        public string? Note;
        public BaseDiscountStrategy(ProductDiscountVM vm)
        {
            Id = vm.DiscountId;
            Name = vm.DiscountName;
            Note = vm.DiscountDescription;
        }
        public abstract ItemDiscount Process(CartContext cartItems);
        public string? Exclusivetag = null;
    }
}

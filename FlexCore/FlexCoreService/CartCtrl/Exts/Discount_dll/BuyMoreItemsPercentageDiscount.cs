using EFModels.Models;
using FlexCoreService.CartCtrl.Models.vm;

namespace FlexCoreService.CartCtrl.Exts.Discount_dll
{
    public class BuyMoreItemsPercentageDiscount : BaseDiscountStrategy
    {
        // 門檻數量
        private readonly int _itemsCount;
        // 折扣, 20表示八折
        private readonly int _percentOff;
        public BuyMoreItemsPercentageDiscount(ProductDiscountVM vm) : base(vm)
        {
            _itemsCount = vm.ConditionValue.Value;
            _percentOff = vm.DiscountValue.Value;
        }

        public override ItemDiscount Process(CartContext cart)
        {
            //throw new NotImplementedException();
            List<CartItemVM> matchedProducts = new List<CartItemVM>();

            foreach (CartItemVM p in cart.CartItems)
            {
                if (p.Product.MatchDiscounts.Any(x => x.DiscountId == Id))
                {
                    matchedProducts.Add(p);
                }
                //if (matchedProducts.Count == _itemsCount)
                //{
                //    // 數量符合門檻, 產生折扣
                //    yield return new Discount()
                //    {
                //        Rule = this,
                //        Products = matchedProducts.ToArray(),
                //        Amount = (decimal)matchedProducts.Sum(x => x.SubTotal) * _percentOff / 100
                //    };
                //    matchedProducts.Clear();
                //}
            }

            if (matchedProducts.Count >= _itemsCount)
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

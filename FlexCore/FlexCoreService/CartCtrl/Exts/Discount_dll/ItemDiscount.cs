using EFModels.Models;
using FlexCoreService.CartCtrl.Models.vm;
using Microsoft.AspNetCore.Routing;

namespace FlexCoreService.CartCtrl.Exts.Discount_dll
{
    public class ItemDiscount
    {
        public int Id { get; set; }
        public BaseDiscountStrategy Rule { get; set; } // 因為哪一個規則產生的折扣
        public CartItemProductVM[] Products { get; set; } // 這個折扣是針對哪些商品
        public decimal Amount { get; set; } // 折扣金額
    }
}

using EFModels.Models;
using FlexCoreService.CartCtrl.Exts;
using FlexCoreService.CartCtrl.Exts.Coupon_dll;
using FlexCoreService.CartCtrl.Exts.Discount_dll;
using FlexCoreService.CartCtrl.Interface;
using FlexCoreService.CartCtrl.Models.vm;
using FlexCoreService.CartCtrl.Service;
using FlexCoreService.ProductCtrl.Exts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlexCoreService.Controllers
{
    [EnableCors("AllowAny")]
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly AppDbContext _db;
		private ICartRepository _repo;
		private CartService _service;
		private int memberId = 1;

		public CartController(AppDbContext context, ICartRepository repo)
		{
			_db = context;
			_repo = repo;
			_service = new CartService(_repo);
		}

		// POST: api/Cart/
		[HttpPost]
		public async Task<ActionResult<IEnumerable<CartItemVM>>> GetCartItems()
		{
			var cartItems = await Task.Run(() => _service.GetCartItems(memberId).Select(x => x.ToViewModel()));
			return Ok(cartItems);
		}
		// PUT: api/Cart/UpdateItem
		[HttpPut("UpdateItem")]
		public async Task<ActionResult<Result>> UpdateItem(CartItemVM vm)
		{
			var result = await Task.Run(() => _service.UpdateCartItemQty(vm.ToDto(), memberId));
			return Ok(result);
		}

        // POST: api/Cart/Checkout
        [HttpPost("Checkout")]
        public async Task<ActionResult<CartContext>> Checkout()
        {
            try
            {
                var result = await Task.Run(() => _service.GetCartItems(memberId).Select(x => x.ToViewModel()));
                CartContext checkOutResult = CheckoutProcess(result);
                return Ok(checkOutResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred.");
            }
        }

        // POST: api/Cart/Checkout
        [HttpPost("TestCoupons")]
		public async Task<ActionResult<CouponVM>> TestCoupons()
		{
            var result = await Task.Run(() => _service.GetMemberCoupons(memberId).Select(x=>x.ToViewModel()));
			return Ok(result);
		}

        // POST: api/Cart/Checkout
        [HttpPost("TestCoupon")]
        public async Task<ActionResult<CouponVM>> TestCoupon()
        {
            var result = await Task.Run(() => _service.GetCouponById(1).ToViewModel());
            return Ok(result);
        }

        private CartContext CheckoutProcess(IEnumerable<CartItemVM> cartItems)
		{
			CartContext cart = new CartContext(cartItems);
			POS pos = new POS();
			pos.ActivedRules.AddRange(LoadDiscount());
			pos.CheckoutProcess(cart);
			return cart;
		}

		private IEnumerable<BaseDiscountStrategy> LoadDiscount()
		{
			var vms = _service.GetActiveDiscounts().Select(x => x.ToViewModel());
			foreach (var vm in vms)
			{
				// 折扣類型如為1是百分比折扣，0則是金額折扣
				if (vm.DiscountType == 1)
				{
					// 門檻類型為1為數量門檻，0為金額門檻
					if (vm.ConditionType == 1)
					{
						yield return new BuyMoreItemsPercentageDiscount(vm);
					}
					else
					{
						yield return new PercentageDiscount(vm);
					}
				}
				else
				{
					if (vm.ConditionType == 1)
					{
						yield return new BuyMoreItemsAmountDiscount(vm);
					}
					else
					{
						yield return new AmountDiscount(vm);
					}
				}
			}
		}
		private BaseCouponStrategy LoadCoupon(int sendingId)
		{
			var vm = _service.GetCouponById(sendingId).ToViewModel();
			// 折扣類型如為1是百分比折扣，0則是金額折扣,2是免運費
			if (vm.DiscountType == 1) return new PercentageCoupon(vm);
			else if (vm.DiscountType == 2) return new FreeShippingCoupon(vm);
			else return new AmountCoupon(vm);
		}
	}
}

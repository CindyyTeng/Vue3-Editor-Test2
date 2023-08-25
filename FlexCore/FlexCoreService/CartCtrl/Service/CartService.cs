using FlexCoreService.CartCtrl.Interface;
using FlexCoreService.CartCtrl.Models.Dtos;
using FlexCoreService.CartCtrl.Models.vm;
using FlexCoreService.ProductCtrl.Exts;

namespace FlexCoreService.CartCtrl.Service
{
	public class CartService
	{
		private ICartRepository _repo;
		public CartService(ICartRepository repo)
		{
			_repo = repo;
		}

		public IEnumerable<CartItemDto> GetCartItems(int memberId)
			=> _repo.GetCartItems(memberId);
		
		public Result UpdateCartItemQty(CartItemDto dto, int memberId)
		{
			// 驗證有無傳入商品跟數量
			if (dto.ProductId == null) return Result.Failed(string.Empty);
			if (dto.Qty == null) return Result.Failed(string.Empty);
			
			// 驗證購物車
			if (!_repo.ExistsCart(memberId)) _repo.CreateCart(memberId);
			dto.CartId = _repo.GetCartId(memberId);
            // 驗證商品是否已存在購物車中
            var existResult = _repo.ExistsCartItem(memberId, dto.ProductId.Value);

            // 如有，更新購物車項目
            dto.CartItemId = existResult.CartItemId;

            // 若商品數量小於等於0 刪除購物車項目
            if (dto.Qty <= 0)
			{
				_repo.DeleteCartItem(dto);
				return Result.Success();
			}

            // 如沒有，創建購物車項目
            if (!existResult.DoesExist)
            {
                _repo.CreateCartItem(dto);
                return Result.Success();
            }

            
			_repo.UpdateCartItemQty(dto);
			return Result.Success();
		}

		public IEnumerable<ProductDiscountDto> GetActiveDiscounts()
		{
			return _repo.GetActiveDiscounts();
		}

		public IEnumerable<CouponDto> GetMemberCoupons(int MemberId)
		{
			return _repo.GetMemberCoupons(MemberId);
		}
		public CouponDto GetCouponById(int SendingId)
		{
			return _repo.GetCouponById(SendingId);
		}
		public Result ChangeCouponUseageStatus(CouponDto dto,int MemberId)
		{
			// TODO 驗證券是否是該會員的

			// TODO 驗證券是否過期

			DateTime date = DateTime.Now;
			_repo.UpdateCouponUsage(dto, date);
			return Result.Success();
		}
	}
}

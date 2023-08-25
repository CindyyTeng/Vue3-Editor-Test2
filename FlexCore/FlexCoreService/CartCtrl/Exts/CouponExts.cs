using FlexCoreService.CartCtrl.Models.Dtos;
using FlexCoreService.CartCtrl.Models.vm;

namespace FlexCoreService.CartCtrl.Exts
{
	public static class CouponExts
	{
		public static CouponDto ToDto(this CouponVM vm)
		{
			return new CouponDto
			{
				Id = vm.Id,
				MinimumPurchaseAmount = vm.MinimumPurchaseAmount,
				CouponCategoryId = vm.CouponCategoryId,
				CouponName = vm.CouponName,
				DiscountType = vm.DiscountType,
				DiscountValue = vm.DiscountValue,
				EndDate = vm.EndDate,
				SendingId = vm.SendingId,
				StartDate = vm.StartDate,

			};
		}

		public static CouponVM ToViewModel(this CouponDto dto)
		{
			return new CouponVM
			{
				Id = dto.Id,
				MinimumPurchaseAmount = dto.MinimumPurchaseAmount,
				CouponCategoryId = dto.CouponCategoryId,
				CouponName = dto.CouponName,
				DiscountType = dto.DiscountType,
				DiscountValue = dto.DiscountValue,
				EndDate = dto.EndDate,
				SendingId = dto.SendingId,
				StartDate = dto.StartDate,

			};
		}
	}
}

using FlexCoreService.CustomeShoes.Models.Dtos;
using FlexCoreService.CustomeShoes.Models.VMs;
using FlexCoreService.ProductCtrl.Models.Dtos;
using FlexCoreService.ProductCtrl.Models.VM;

namespace FlexCoreService.CustomeShoes.Exts
{
	public static class ShoesCategoryExts
	{
		public static ShoesCategoryVM ToVM(this ShoesCategoryDto dto)
		{
			return new ShoesCategoryVM
			{
				ShoesCategoryId = dto.ShoesCategoryId,
				ShoesCategoryName = dto.ShoesCategoryName
			};
		}
	}
}

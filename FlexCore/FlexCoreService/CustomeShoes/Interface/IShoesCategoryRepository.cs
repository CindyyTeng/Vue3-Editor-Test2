using FlexCoreService.CustomeShoes.Models.Dtos;
using FlexCoreService.ProductCtrl.Models.Dtos;

namespace FlexCoreService.CustomeShoes.Interface
{
	public interface IShoesCategoryRepository
	{
		IEnumerable<ShoesCategoryDto> GetAllCategory();

    }
}

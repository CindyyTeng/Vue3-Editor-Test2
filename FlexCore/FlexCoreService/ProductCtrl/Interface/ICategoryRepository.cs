using FlexCoreService.ProductCtrl.Models.Dtos;

namespace FlexCoreService.ProductCtrl.Interface
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryDto> GetAllCategory();

        IEnumerable<CategoryDto> SearchCategory(int categoryId);

    }
}

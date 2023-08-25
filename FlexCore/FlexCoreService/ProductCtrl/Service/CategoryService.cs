using FlexCoreService.ProductCtrl.Interface;
using FlexCoreService.ProductCtrl.Models.Dtos;

namespace FlexCoreService.ProductCtrl.Service
{
    public class CategoryService
    {
        private ICategoryRepository _repo;
        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<CategoryDto> GetAllCategory()
        {
            var result = _repo.GetAllCategory();
            return result;
        }

        public IEnumerable<CategoryDto> SearchCategory(int categoryId)
        {
            var result = _repo.SearchCategory(categoryId);
            return result;
        }

    }
}

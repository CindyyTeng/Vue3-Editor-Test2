using FlexCoreService.CustomeShoes.Interface;
using FlexCoreService.CustomeShoes.Models.Dtos;

namespace FlexCoreService.CustomeShoes.Service
{
    public class ShoesChoosesService
    {
        private ICustomerChooseRepository _repo;
        public ShoesChoosesService(ICustomerChooseRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<ShoesColorCategoriesDto> GetColor()
        {
            var result = _repo.GetColor();
            return result;
        }

        public IEnumerable<CustomizedMaterialsDto> GetMaterial()
        {
            var result = _repo.GetMaterial();
            return result;
        }

        public IEnumerable<ShoesOptionsDto> GetOptions()
        {
            var result = _repo.GetOptions();
            return result;
        }

        public IEnumerable<ShoesSizeDto> GetSizes()
        {
            var result = _repo.GetSize();
            return result;
        }
    }
}

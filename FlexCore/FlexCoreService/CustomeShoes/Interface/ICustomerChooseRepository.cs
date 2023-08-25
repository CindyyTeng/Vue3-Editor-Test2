using FlexCoreService.CustomeShoes.Models.Dtos;

namespace FlexCoreService.CustomeShoes.Interface
{
    public interface ICustomerChooseRepository
    {
        IEnumerable<ShoesOptionsDto> GetOptions();
        IEnumerable<ShoesColorCategoriesDto> GetColor();
        IEnumerable<CustomizedMaterialsDto> GetMaterial();

        IEnumerable<ShoesSizeDto> GetSize();
        
    }
}

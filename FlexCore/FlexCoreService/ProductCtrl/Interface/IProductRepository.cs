using FlexCoreService.ProductCtrl.Models.Dtos;

namespace FlexCoreService.ProductCtrl.Interface
{
    public interface IProductRepository
    {
        //IEnumerable<ProductCardDto> SearchProducts();

        IEnumerable<ProductCardDto> SearchProducts(int? salesId, string? categoryName, string? subCategoryName);

        IEnumerable<ProductDetailDto> GetProductDetail(string productId);
        IEnumerable<ProductImgsDto> GetProductImgs(string productId);

        IEnumerable<ProductCommentDto> GetProductComment(string productId);

        IEnumerable<ProductCardDto> GetSimilarProducts(string productId,CategoryDto dto);

        CategoryDto GetProductCategories(string productId);
    }
}

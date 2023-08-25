using EFModels.Models;
using FlexCoreService.ProductCtrl.Models.Dtos;
using FlexCoreService.ProductCtrl.Models.VM;

namespace FlexCoreService.ProductCtrl.Exts
{
    public static class ProductsExts
    {
        public static ProductCardVM ToCardVM(this ProductCardDto dto)
        {
            return new ProductCardVM
            {
                ProductId = dto.ProductId,
                ProductName = dto.ProductName,
                UnitPrice = dto.UnitPrice,
                SalesPrice = dto.SalesPrice,
                SalesCategoryId = dto.SalesCategoryId,
                SalesCategoryName = dto.SalesCategoryName,
                ProductCategoryName=dto.ProductCategoryName,
                ProductSubCategoryName = dto.ProductSubCategoryName,
                FirstImgPath = dto.FirstImgPath,
            };
        }

        public static ProductDetailVM ToDetailVM(this IEnumerable<ProductDetailDto> dto)
        {
            var group = new Dictionary<string, List<ColorGroupDto>>();

            foreach(var detail in dto)
            {
                var colorGroup = new ColorGroupDto()
                {
                    ProductGroupId = detail.ProductGroupId,
                    SizeName = detail.SizeName,
                    Qty = detail.Qty,
                    DefaultColorImg = detail.DefaultColorImg,
                };
                if (!group.ContainsKey(detail.ColorName))
                {
                    group[detail.ColorName]= new List<ColorGroupDto> { colorGroup };
                }
                else
                {
                    group[detail.ColorName].Add(colorGroup);
                }
            }

            var vm = new ProductDetailVM()
            {
                ProductId = dto.FirstOrDefault().ProductId,
                ProductName = dto.FirstOrDefault().ProductName,
                ProductDescription = dto.FirstOrDefault().ProductDescription,
                ProductMaterial = dto.FirstOrDefault().ProductMaterial,
                ProductOrigin = dto.FirstOrDefault().ProductOrigin,
                UnitPrice = dto.FirstOrDefault()?.UnitPrice,
                SalesPrice = dto.FirstOrDefault().SalesPrice,
                SalesCategoryName = dto.FirstOrDefault()?.SalesCategoryName,
                ProductCategoryName= dto.FirstOrDefault()?.ProductCategoryName,
                ProductSubCategoryName = dto.FirstOrDefault()?.ProductSubCategoryName,
                ProductGroup =group
            };
            return vm;
        }

        public static ProductImgsVM ToImgVM(this ProductImgsDto dto)
        {
            var vm = new ProductImgsVM
            {
                ProductImgId = dto.ProductImgId,
                ImgPath = dto.ImgPath,
            };
            return vm;
        }

        public static ProductCommentVM ToCommentVM(this ProductCommentDto dto)
        {
            return new ProductCommentVM
            {
                CommentId = dto.CommentId,
                Description = dto.Description,
                MemberName = dto.MemberName,
                CreateTime = dto.CreateTime,
                Score = dto.Score,
                AverageScore = dto.AverageScore,
            };
        }
    }
}

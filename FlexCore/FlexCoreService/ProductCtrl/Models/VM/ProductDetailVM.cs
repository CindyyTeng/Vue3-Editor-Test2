using FlexCoreService.ProductCtrl.Models.Dtos;

namespace FlexCoreService.ProductCtrl.Models.VM
{
    public class ProductDetailVM
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string? ProductMaterial { get; set; }
        public string ProductOrigin { get; set; }
        public string? SalesCategoryName { get; set; }
        public string? ProductCategoryName { get; set; }
        public string? ProductSubCategoryName { get; set; }
        public int? UnitPrice { get; set; }
        public int SalesPrice { get; set; }
        public Dictionary<string,List<ColorGroupDto>> ProductGroup { get; set; }

    }
}

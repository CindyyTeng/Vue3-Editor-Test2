namespace FlexCoreService.ProductCtrl.Models.Dtos
{
    public class ProductCardDto
    {
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? UnitPrice { get; set; }
        public int SalesPrice { get; set; }
        public int SalesCategoryId { get; set; }
        public string? SalesCategoryName { get; set; }
        public string? ProductCategoryName { get; set; }
        public string? ProductSubCategoryName { get; set; }
        public string? FirstImgPath { get; set; }
    }
}

namespace FlexCoreService.ProductCtrl.Models.Dtos
{
    public class ProductDetailDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string? ProductMaterial { get; set; }
        public string ProductOrigin { get; set; }
        public int? UnitPrice { get; set; }
        public int SalesPrice { get; set; }
        public int ProductGroupId { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
        public int Qty { get; set; }
        public string DefaultColorImg { get; set; }
        public string? SalesCategoryName { get; set; }
        public string? ProductCategoryName { get; set; }
        public string? ProductSubCategoryName { get; set; }
    }
}

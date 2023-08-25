using Dapper;
using FlexCoreService.ProductCtrl.Interface;
using FlexCoreService.ProductCtrl.Models.Dtos;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FlexCoreService.ProductCtrl.Infra.DPRepository
{
    public class ProductDPRepository : IProductRepository
    {
        private string _connStr;
        private IConfiguration _configuration;
        public ProductDPRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("AppDbContext");
        }

        public CategoryDto GetProductCategories(string productId)
        {
            string sql = @"select sc.SalesCategoryName,pc.ProductCategoryName,psc.ProductSubCategoryName 
from Products as p 
join ProductSubCategories as psc on psc.ProductSubCategoryId=p.fk_ProductSubCategoryId 
join ProductCategories as pc on pc.ProductCategoryId=psc.fk_ProductCategoryId 
join SalesCategories as sc on sc.SalesCategoryId=pc.fk_SalesCategoryId 
where p.ProductId='"+ @productId + "'";

            using IDbConnection dbConnection = new SqlConnection(_connStr);
            var result = dbConnection.QueryFirst<CategoryDto>(sql, new { productId });
            return result;
        }

        public IEnumerable<ProductCardDto> GetSimilarProducts(string productId, CategoryDto dto)
        {
            string sql = @"select top 5
p.ProductId, p.ProductName, p.UnitPrice,p.SalesPrice,sc.SalesCategoryName,
pc.ProductCategoryName,psc.ProductSubCategoryName,sc.SalesCategoryId,MIN(pi.ImgPath) AS FirstImgPath 
from Products as p
join ProductSubCategories as psc on psc.ProductSubCategoryId=p.fk_ProductSubCategoryId
join ProductCategories as pc on pc.ProductCategoryId=psc.fk_ProductCategoryId
join SalesCategories as sc on sc.SalesCategoryId=pc.fk_SalesCategoryId
join ProductImgs as pi on pi.fk_ProductId=p.ProductId
group by p.ProductId, p.ProductName, p.UnitPrice,p.SalesPrice,sc.SalesCategoryName,
pc.ProductCategoryName,psc.ProductSubCategoryName,p.Status,p.LogOut,sc.SalesCategoryId  
having p.ProductId <> @productId and 
sc.SalesCategoryName=@SalesCategoryName and 
pc.ProductCategoryName= @ProductCategoryName and 
psc.ProductSubCategoryName =@ProductSubCategoryName and 
p.Status=0 and p.LogOut=0 order by NEWID();";

            using IDbConnection dbConnection= new SqlConnection(_connStr);
            var result = dbConnection.Query<ProductCardDto>(sql, new { productId=productId, SalesCategoryName =dto.SalesCategoryName, ProductCategoryName =dto.ProductCategoryName, ProductSubCategoryName =dto.ProductSubCategoryName});
            return result;
        }

        public IEnumerable<ProductCommentDto> GetProductComment(string productId)
        {
            string sql = @"select pc.Id as CommentId,pc.Description,pc.Score,
cast(pc.CreateTime as date) as CreateTime,
m.Name as MemberName,cast(avg(cast(pc.Score as decimal)) over() as decimal(10, 2)) as AverageScore 
from ProductComment as pc 
join ProductGroups as pg on pg.ProductGroupId=pc.fk_ProductGroupId 
join Products as p on p.ProductId=pg.fk_ProductId 
join Members as m on m.MemberId=pc.fk_MemberId 
where p.ProductId='"+@productId+ 
"' and pc.Status=0 " + 
"order by pc.CreateTime";

            using IDbConnection dbConnection = new SqlConnection(_connStr);
            var result = dbConnection.Query<ProductCommentDto>(sql, new { productId });
            return result;
        }

        public IEnumerable<ProductDetailDto> GetProductDetail(string productId)
        {
            string sql = @"select 
p.ProductId,p.ProductName,p.ProductDescription,p.ProductMaterial,p.ProductOrigin, 
p.UnitPrice,p.SalesPrice,pg.ProductGroupId,cc.ColorName,sc.SizeName, 
s.SalesCategoryName,pc.ProductCategoryName,psc.ProductSubCategoryName,pg.Qty,pi.ImgPath as DefaultColorImg 
from Products as p 
join ProductGroups as pg on pg.fk_ProductId=p.ProductId 
join ColorCategories as cc on cc.ColorId=pg.fk_ColorId 
join SizeCategories as sc on sc.SizeId=pg.fk_SizeId 
join ProductImgs as pi on pi.fk_ProductId =p.ProductId 
join ProductSubCategories as psc on psc.ProductSubCategoryId=p.fk_ProductSubCategoryId
join ProductCategories as pc on pc.ProductCategoryId=psc.fk_ProductCategoryId
join SalesCategories as s on s.SalesCategoryId=pc.fk_SalesCategoryId 
where p.Status=0 and 
p.LogOut=0 and 
pi.fk_ColorId=cc.ColorId and 
p.ProductId=@productId";

            using IDbConnection dbConnection = new SqlConnection(_connStr);
            var result = dbConnection.Query<ProductDetailDto>(sql, new { productId=productId });
            return result;
        }

        public IEnumerable<ProductImgsDto> GetProductImgs(string productId)
        {
            string sql = @"select ProductImgId,ImgPath from ProductImgs
where fk_ProductId='"+ @productId+"'";

            using IDbConnection dbConnection=new SqlConnection(_connStr);
            var result=dbConnection.Query<ProductImgsDto>(sql, new {productId});
            return result;

        }

        public IEnumerable<ProductCardDto> SearchProducts(int? salesId, string? categoryName, string? subCategoryName)
        {
            string sql = @"select p.ProductId, p.ProductName, p.UnitPrice,p.SalesPrice,s.SalesCategoryId,
pc.ProductCategoryName,ps.ProductSubCategoryName,s.SalesCategoryName,
MIN(pi.ImgPath) AS FirstImgPath 
from Products as p
join ProductImgs as pi on p.ProductId = pi.fk_ProductId
join ProductSubCategories as ps on ps.ProductSubCategoryId=p.fk_ProductSubCategoryId
join ProductCategories as pc on pc.ProductCategoryId=ps.fk_ProductCategoryId
join SalesCategories as s on s.SalesCategoryId=pc.fk_SalesCategoryId
group by p.ProductId, p.ProductName, p.UnitPrice, p.SalesPrice, p.Status,
p.LogOut,s.SalesCategoryId,pc.ProductCategoryName,ps.ProductSubCategoryName,s.SalesCategoryName 
having p.Status=0 and p.LogOut=0 "+
(salesId.HasValue ? " and s.SalesCategoryId = " + @salesId : "") +
" and  pc.ProductCategoryName like '%" + @categoryName + "%'" +
" and ps.ProductSubCategoryName like '%" + @subCategoryName + "%'" +
" order by p.SalesPrice";

            using IDbConnection dbConnection = new SqlConnection(_connStr);
            var result = dbConnection.Query<ProductCardDto>(sql, new { salesId , categoryName , subCategoryName });
            return result;
        }



        //        public IEnumerable<ProductCardDto> SearchProducts()
        //        {
        //            string sql = @"select p.ProductId, p.ProductName, p.UnitPrice, p.SalesPrice, MIN(pi.ImgPath) AS FirstImgPath 
        //from Products as p
        //join ProductImgs as pi on p.ProductId = pi.fk_ProductId
        //group by p.ProductId, p.ProductName, p.UnitPrice, p.SalesPrice, p.Status, p.LogOut
        //having p.Status=0 and p.LogOut=0
        //order by p.ProductId";

        //            using IDbConnection dbConnection = new SqlConnection(_connStr);
        //            var result = dbConnection.Query<ProductCardDto>(sql);
        //            return result;
        //        }

    }
}

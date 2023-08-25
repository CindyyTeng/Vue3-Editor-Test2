using Dapper;
using EFModels.Models;
using FlexCoreService.CartCtrl.Interface;
using FlexCoreService.CartCtrl.Models.Dtos;
using FlexCoreService.CartCtrl.Models.vm;
using Humanizer;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace FlexCoreService.CartCtrl.Infra
{
	public class CartDapperRepository : ICartRepository
	{
		private IConfiguration _configuration;
		private string _connStr;
		public CartDapperRepository(IConfiguration configuration)
		{
			_configuration = configuration;
			_connStr = _configuration.GetConnectionString("AppDbContext");
		}

		public IEnumerable<CartItemDto> GetCartItems(int memberId)
		{
			using (IDbConnection dbConnection = new SqlConnection(_connStr))
			{
				dbConnection.Open();
				string sql = @"select 
ci.CartItemId, c.CartId, pg.ProductGroupId as ProductId, ci.Qty, p.ProductId as ProductSaleId, 
p.ProductName, p.UnitPrice, p.SalesPrice, sc.SizeName as Size, cc.ColorName as Color, 
pir.ImgPath, ssc.SalesCategoryName, d.DiscountId, d.DiscountName, d.DiscountDescription, 
d.DiscountType, d.DiscountValue, d.ConditionType, d.ConditionValue, d.OrderBy as DiscountOrder
from CartItems as ci
inner join ShoppingCarts as C on c.CartId = ci.fk_CardId
inner join ProductGroups as pg on pg.ProductGroupId = ci.fk_ProductId
inner join Products as p on p.ProductId = pg.fk_ProductId
inner join ColorCategories as cc on cc.ColorId = pg.fk_ColorId
inner join SizeCategories as sc on sc.SizeId = pg.fk_SizeId
inner join ProductSubCategories as psc on psc.ProductSubCategoryId = p.fk_ProductSubCategoryId
inner join ProductCategories as pc on pc.ProductCategoryId = psc.fk_ProductCategoryId
inner join SalesCategories as ssc on ssc.SalesCategoryId = pc.fk_SalesCategoryId
left join ProjectTagItems as pti on pti.fk_ProductId = p.ProductId
left join(
    SELECT
        pi.fk_ProductId,
        pi.ProductImgId,
        pi.ImgPath
    FROM ProductImgs AS pi
    WHERE pi.ProductImgId = (
        SELECT TOP 1 pi2.ProductImgId
        FROM ProductImgs AS pi2
        WHERE pi2.fk_ProductId = pi.fk_ProductId
        ORDER BY pi2.ProductImgId
    )
) AS pir ON pir.fk_ProductId = p.ProductId
left join Discounts as d on d.fk_ProjectTagId = pti.fk_ProjectTagId
where p.Status=0 and p.LogOut=0 and c.fk_MemberID = 1 
AND (d.StartDate <= GETDATE() AND (d.EndDate > GETDATE() OR d.EndDate IS NULL) AND d.status = 1
OR d.DiscountId IS NULL)
GROUP BY
ci.CartItemId, c.CartId, pg.ProductGroupId, ci.Qty, p.ProductId,
p.UnitPrice, p.SalesPrice, sc.SizeName, cc.ColorName, pir.ImgPath,
d.DiscountId, d.DiscountName, d.DiscountDescription, d.DiscountType, d.DiscountValue,
d.ConditionType, d.ConditionValue, d.OrderBy, ssc.SalesCategoryName ,p.ProductName
ORDER BY p.ProductId asc, d.OrderBy asc;
";

				var cartItems = dbConnection.Query<CartItemDto, CartItemProductDto, ProductDiscountDto, CartItemDto>(
					sql,
					(cartItem, product, discount) =>
					{
						cartItem.Product = product;
						if (cartItem.Product != null)
						{
							cartItem.Product.MatchDiscounts = new List<ProductDiscountDto> { discount };
						}
						return cartItem;
					},
					new { memberId = memberId },
					splitOn: "ProductId,ProductSaleId,DiscountId"
				);

				// 將獲得資料組合起來 (相同商品不同折扣)
				var groupedCartItems = cartItems.GroupBy(cartItem => cartItem.CartItemId).Select(group =>
				{
					var firstCartItem = group.First();
					firstCartItem.Product.MatchDiscounts = group.Select(cartItem => cartItem.Product.MatchDiscounts.FirstOrDefault()).Where(discount => discount != null).ToList();
					return firstCartItem;
				});

				return groupedCartItems;
			}
		}
		public void CreateCartItem(CartItemDto dto)
		{
			using (var connection = new SqlConnection(_connStr))
			{
				connection.Open();
				string sql = @"
INSERT INTO CartItems(fk_CardId,fk_ProductId,Description,Qty)
VALUES (@CartId,@ProductId,@Description,@Qty)
";
				var parameters = new { CartId = dto.CartId.Value, ProductId = dto.ProductId.Value , Description = "", Qty = dto.Qty.Value };
				connection.Execute(sql, parameters);
			}
		}

		public void UpdateCartItemQty(CartItemDto dto)
		{
			using (var connection = new SqlConnection(_connStr))
			{
				connection.Open();
				string sql = @"
UPDATE CartItems SET
qty = @Qty
WHERE CartItemId = @CartItemId
";
				var parameters = new { dto.CartItemId, dto.Qty };
				connection.Execute(sql, parameters);
			}
		}

		public (bool DoesExist, int CartItemId) ExistsCartItem(int memberId, int productId)
		{
			using (var connection = new SqlConnection(_connStr))
			{
				connection.Open();
				string sql = @"
SELECT TOP 1 ci.cartitemId
FROM CartItems AS ci
JOIN ShoppingCarts AS sc ON sc.CartId = ci.fk_CardId
WHERE sc.fk_MemberID = @MemberID AND ci.fk_ProductId = @ProductId"
;
				var parameters = new { MemberID = memberId, ProductId = productId };
				int cartItemId = connection.QuerySingleOrDefault<int?>(sql, parameters) ?? 0;
				bool doesExist = cartItemId > 0;
				return (doesExist, cartItemId);
			}
		}
		public bool ExistsCart(int memberId)
		{
			using (var connection = new SqlConnection(_connStr))
			{
				connection.Open();
				string sql = @"
SELECT TOP 1 1
FROM ShoppingCarts as sc
WHERE sc.fk_MemberID = @MemberID;"
;
				var parameters = new { MemberID = memberId };
				bool doesExist = connection.QuerySingleOrDefault<bool>(sql, parameters);
				
				return doesExist;
			}
		}
		public int CreateCart(int memberId)
		{

			using (var connection = new SqlConnection(_connStr))
			{
				connection.Open();
				string sql = @"
INSERT INTO ShoppingCarts(fk_MemberID)
VALUES (@MemberId);
SELECT SCOPE_IDENTITY();
"
;
				var parameters = new { MemberId = memberId };
				int newCartId = connection.QuerySingle<int>(sql, parameters);

				return newCartId;
			}
		}

		public int GetCartId(int memberId)
		{
			using (var connection = new SqlConnection(_connStr))
			{
				connection.Open();
				string sql = @"
select CartId
from ShoppingCarts as sc
where sc.fk_MemberID = @MemberId
";
				var parameters = new { MemberId = memberId };
				int CartId = connection.QuerySingle<int>(sql, parameters);
				return CartId;
			}
		}

		public void DeleteCartItem(CartItemDto dto)
		{
			using (var connection = new SqlConnection(_connStr))
			{
				connection.Open();

				string sql = @"
DELETE FROM CartItems
WHERE CartItemId = @CartItemId"
;

				var parameters = new { CartItemId = dto.CartItemId.Value };

				connection.Execute(sql, parameters);
			}
		}

		public IEnumerable<ProductDiscountDto> GetActiveDiscounts()
		{
			
			using (IDbConnection dbConnection = new SqlConnection(_connStr))
			{
				dbConnection.Open();
				string sql = @"
select * from discounts as d
where d.StartDate <= getdate() 
and ( d.EndDate > getdate() OR d.EndDate IS NULL) 
and d.status = 1
";

				return dbConnection.Query<ProductDiscountDto>(sql);
			}
		}

		public IEnumerable<CouponDto> GetMemberCoupons(int memberId)
		{
			using (IDbConnection dbConnection = new SqlConnection(_connStr))
			{
				dbConnection.Open();
				string sql = @"select 
c.CouponId as Id, c.fk_CouponCategoryId as CouponCategoryId, cs.SendingId, c.CouponName, c.MinimumPurchaseAmount,
c.DiscountType, c.DiscountValue, cs.StartDate, cs.EndDate 
from CouponSendings as cs
inner join Coupons as c on c.CouponId = cs.fk_CouponId
where cs.fk_MemberId = @MemberId and RedemptionStatus = 0
and cs.StartDate <= CURRENT_TIMESTAMP and cs.EndDate > CURRENT_TIMESTAMP;
";
				var parameters = new { MemberId = memberId };

				return dbConnection.Query<CouponDto>(sql,parameters);
			}
		}

		public CouponDto GetCouponById(int sendingId)
		{
			using (IDbConnection dbConnection = new SqlConnection(_connStr))
			{
				dbConnection.Open();
				string sql = @"select 
c.CouponId as Id, c.fk_CouponCategoryId as CouponCategoryId, cs.SendingId, c.CouponName, c.MinimumPurchaseAmount,
c.DiscountType, c.DiscountValue, cs.StartDate, cs.EndDate 
from CouponSendings as cs
inner join Coupons as c on c.CouponId = cs.fk_CouponId
where cs.SendingId = @SendingId
";
				var parameters = new { SendingId = sendingId };

				return dbConnection.QueryFirstOrDefault<CouponDto>(sql, parameters);
			}
		}

		public void UpdateCouponUsage(CouponDto dto, DateTime date)
		{
			using (var connection = new SqlConnection(_connStr))
			{
				connection.Open();
				string sql = @"
UPDATE CouponSendings SET
RedemptionStatus = 1,
RedeemedDate = @RedeemedDate
WHERE SendingId = @SendingId
";
				var parameters = new
				{
					SendingId = dto.SendingId,
					RedeemedDate = date
				};
				connection.Execute(sql, parameters);
			}
		}
	}
}

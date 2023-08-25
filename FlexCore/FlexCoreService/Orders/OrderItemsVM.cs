using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FlexCoreService.Orders
{
		public class OrderItemsVM
		{
			[Display(Name = "明細ID")]
			public int Id { get; set; }
			[Display(Name = "訂單ID")]
			public int order_Id { get; set; }
			[Display(Name = "名稱")]
			[StringLength(50)]
			public string? product_name { get; set; }
			//[Display(Name = "類別ID")]
			public int fk_typeId { get; set; }
			//[Display(Name = "類別")]
			public string? type { get; set; }
			[Display(Name = "單價")]
			public int? per_price { get; set; }
			[Display(Name = "數量")]
			public int? quantity { get; set; }
			[Display(Name = "折扣名稱")]
			[StringLength(50)]
			public string? discount_name { get; set; }
			[Display(Name = "小計")]
			public int? subtotal { get; set; }
			[Display(Name = "折扣後金額")]
			public int? discount_subtotal { get; set; }
			[Display(Name = "描述")]
			[StringLength(50)]
			public string? Items_description { get; set; }

			//[Display(Name = "收件人電話")]
			//public string? cellphone { get; set; }
			//[Display(Name = "收據號碼")]
			//public string? receipt { get; set; }
			//[Display(Name = "收件人名稱")]
			//public string? receiver { get; set; }
			//[Display(Name = "收件人地址")]
			//public string? recipient_address { get; set; }
			//[Display(Name = "結單")]
			//public bool close { get; set; }
		}
	
}

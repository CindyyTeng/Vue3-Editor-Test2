namespace FlexCoreService.CartCtrl.Models.vm
{
	public class CartItemDto
	{
		public int? CartItemId { get; set; }
		public int? CartId { get; set; }
		public int? ProductId { get; set; }
		public int?	 Qty { get; set; }
		public CartItemProductDto? Product { get; set; }
	}
}

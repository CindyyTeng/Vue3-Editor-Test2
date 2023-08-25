namespace FlexCoreService.CartCtrl.Models.vm
{
	public class CartItemVM
	{
		public int? CartItemId { get; set; }
		public int? CartId { get; set; }
		public int? ProductId { get; set; }
		public int? Qty { get; set; }
		public CartItemProductVM? Product { get; set; }
		public int? SubTotal => Product.SalesPrice * Qty;
		public int? UnitSubTotal => Product.UnitPrice * Qty;
    }
}

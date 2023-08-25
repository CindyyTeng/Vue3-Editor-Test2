namespace FlexCoreService.CartCtrl.Models.vm
{
	public class ProductDiscountVM
	{
		public int DiscountId { get; set; }
		public string? DiscountName { get; set; }
		public string? DiscountDescription { get; set; }
		public int? DiscountType { get; set;}
		public int? DiscountValue { get; set; }
		public int? ConditionType { get; set; }
		public int? ConditionValue { get; set; }
		public int DiscountOrder { get; set; }


	}
}
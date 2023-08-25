namespace FlexCoreService.Orders
{
	public class ReturnVM
	{
		public int ID { get; set; }
		public DateTime? 退貨日期 { get; set; }
		public int? fk訂單 { get; set; }
		public int? 退貨轉帳帳號 { get; set; }
		public bool? 退款狀態 { get; set; }
		public int? 退貨理由 { get; set; }
	}
}

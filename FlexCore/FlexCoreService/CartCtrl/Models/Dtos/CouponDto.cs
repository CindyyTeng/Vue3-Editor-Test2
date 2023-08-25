namespace FlexCoreService.CartCtrl.Models.Dtos
{
    public class CouponDto
    {
        public int Id { get; set; }
        public int CouponCategoryId { get; set; }
        public int SendingId { get; set; }
        public string CouponName { get; set; }
        public int MinimumPurchaseAmount { get; set; }
        public int DiscountType { get; set;}
        public int DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}

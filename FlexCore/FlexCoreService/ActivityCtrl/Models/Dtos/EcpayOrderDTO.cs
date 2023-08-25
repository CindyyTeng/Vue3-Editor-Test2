namespace FlexCoreService.ActivityCtrl.Models.Dtos
{
    public class EcpayOrderDTO
    {
        public int MemberID { get; set; }

        public string? MerchantTradeNo { get; set; } //訂單編號

        public int? RtnCode { get; set; }  //付款和未付款
        public string? RtnMsg { get; set; } //"訂單成功尚未付款"等資訊

        public string? TradeNo { get; set; } //綠界的交易編號

        public int? TradeAmt { get; set; } //交易金額

        public string? TradeDate { get; set; } //訂單成立時間

        public string? PaymentDate { get; set; } //付款時間 (yyyy/MM/dd HH:mm:ss) 

        public string? PaymentType { get; set; } //付款方式
        public decimal? PaymentTypeChargeFee { get; set; } //交易手續費金額
        public int? SimulatePaid { get; set; }  //是否為模擬付款

        public string? TradeDesc { get; set; } //交易描述

        public string? ItemName { get; set; } //品名

        public int? ActivityId { get; set; } //活動編號







    }
}

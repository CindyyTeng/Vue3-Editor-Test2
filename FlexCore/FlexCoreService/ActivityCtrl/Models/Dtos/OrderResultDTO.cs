namespace FlexCoreService.ActivityCtrl.Models.Dtos
{
    public class OrderResultDTO
    {
        public string MerchantID { get; set; }
        public string MerchantTradeNo { get; set; }
        public string StoreID { get; set; }
        public int RtnCode { get; set; }
        public string RtnMsg { get; set; }

        public string TradeNo { get; set; }
        public int TradeAmt { get; set; }
        public string PaymentDate { get; set; }

        public string PaymentType { get; set; }
        public decimal PaymentTypeChargeFee { get; set; } //交易手續費金額

        public string TradeDate { get; set; }

        public int SimulatePaid { get; set; }

        public string CheckMacValue { get; set; }
        public string CustomField1 { get; set; }
        public string CustomField2 { get; set; }
        public string CustomField3 { get; set; }
        public string CustomField4 { get; set; }



    }
}

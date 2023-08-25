namespace FlexCoreService.CartCtrl.Models.vm
{
    public class CheckoutDataVM
    {
        // 寄送資訊
        public ContactInfoVM? ContactInfo { get; set; }

        // 帳單
        public BillingAddressVM? BillingAddress { get; set; }

        // 付款
        public PaymentInfoVM? PaymentInfo { get; set; }
        public CheckoutDataVM()
        {
            ContactInfo = new ContactInfoVM();
            BillingAddress = new BillingAddressVM();
            PaymentInfo = new PaymentInfoVM();
        }
    }

    public class ContactInfoVM
    {
        public string ContactName { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class BillingAddressVM
    {
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }

    public class PaymentInfoVM
    {
        public int? CouponId { get; set; }
        public string PaymentMethod { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public bool ConfirmTerms { get; set; }
    }
}

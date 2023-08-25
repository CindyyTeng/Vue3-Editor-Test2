using EFModels.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Web;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cors;


namespace FlexCoreService.Controllers
{
    [EnableCors("AllowAny")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private AppDbContext _db;
        public PaymentController(AppDbContext context)
        {
            _db = context;
        }

        [HttpGet]
        public Dictionary<string, string> MakePayment()
        {
            var orderId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
            var website = $"https://localhost:7183";

            var order = new Dictionary<string, string>
            {
                { "MerchantID", "3002607" }, //商店編號
                { "MerchantTradeNo", orderId }, //訂單編號
                { "MerchantTradeDate", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") }, //交易時間
                { "PaymentType", "aio" }, //交易類型，固定填aio
                { "TotalAmount", "777" }, //金額
                { "TradeDesc", "拜託讓我金流過666" },//交易描述
                { "ItemName", "羽球大戰" },//品名
                { "ReturnURL", $"{website}/api/Ecpay/AddPayInfo" }, //付款完成通知回傳網址
                { "ChoosePayment", "ALL" }, //預設付款方式
                { "EncryptType", "1" },//CheckMacValue加密類型，固定填1
                { "ClientBackURL", "http://localhost:8084/activityInfo" },//Client端返回商店的按鈕連結
              /*  { "OrderResultURL", $"{website}/Payment/PayInfo/{orderId}" }*/ //Client端回傳付款結果網址
                  
        };
            order["CheckMacValue"] = GetCheckMacValue(order); //為 order 物件添加一個名為 "CheckMacValue" 【檢查碼】的屬性，並將其值設定為 GetCheckMacValue(order)

            return order;

        }



        //[HttpPost("addOrder")]
        //public string AddOrders(FormCollection order)
        //{
        //    EcpayOrderDTO dto = new EcpayOrderDTO
        //    {
        //        MemberID = int.Parse(order["MemberID"]),
        //        MerchantTradeNo = order["MerchantTradeNo"],
        //        RtnCode = 0, //未付款
        //        RtnMsg = "訂單成功尚未付款",
        //        TradeNo = order["TradeNo"],
        //        TradeAmt = int.Parse(order["TradeAmt"]),
        //        TradeDate = order["TradeDate"],
        //        PaymentDate = order["PaymentDate"],
        //        PaymentType = order["PaymentType"],
        //        PaymentTypeChargeFee = int.Parse(order["PaymentTypeChargeFee"]),
        //        SimulatePaid = 0,
        //        TradeDesc = order["TradeDesc"],
        //        ItemName = order["ItemName"],
        //        ActivityId = int.Parse(order["ActivityId"])
        //    };

        //    try { 
        //    _db.EcpayOrders.
        //    }
        //    catch { }

        //}


        private string GetCheckMacValue(Dictionary<string, string> order)
        {
            var param = order.Keys.OrderBy(x => x).Select(key => key + "=" + order[key]).ToList();
            string checkValue = string.Join("&", param);
            //測試用的 HashKey
            var hashKey = "pwFHCqoQZGmho4w6";
            //測試用的 HashIV
            var HashIV = "EkRm7iFT261dpevs";
            checkValue = $"HashKey={hashKey}" + "&" + checkValue + $"&HashIV={HashIV}";
            checkValue = HttpUtility.UrlEncode(checkValue).ToLower();
            checkValue = GetSHA256(checkValue);
            return checkValue.ToUpper();
        }

        private string GetSHA256(string value)
        {
            var result = new StringBuilder();
            var sha256 = SHA256.Create();
            var bts = Encoding.UTF8.GetBytes(value);
            var hash = sha256.ComputeHash(bts);
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }






    }
}

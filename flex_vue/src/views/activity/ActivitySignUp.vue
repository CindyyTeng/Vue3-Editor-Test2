<template>
    <div >
        <div class="mb-3">
            <label for="form-label">會員編號</label>
            <span class="space"></span>
            
            {{memberId}}
        </div>
    
        <div class="mb-3">
            <label class="form-label">姓名</label>
            <input type="text" v-model="member.name" class="form-control" placeholder="請輸入姓名">
            <!-- {{member.mame}} -->
        </div>

        <div class="mb-3">
            <label class="form-label">出生年月日</label>
            <input type="text" v-model="member.birthday" class="form-control" placeholder="請輸入生日">
        </div>

        <div class="mb-3">
            <label class="form-label">性別</label>
            <input type="text" v-model="member.gender" class="form-control" placeholder="請輸入性別">
        </div>

        <div class="mb-3">
            <label class="form-label">電話</label>
            <input type="text" v-model="member.mobile" class="form-control" placeholder="請輸入電話">
        </div>

        <div class="mb-3">
            <label class="form-label">通訊地址</label>
            <input type="text" v-model="member.commonAddress" class="form-control" placeholder="請輸入地址">
        </div>

        <div class="mb-3">
            <label class="form-label">聯絡信箱</label>
            <input type="text" v-model="member.email" class="form-control" placeholder="請輸入電子信箱">
        </div>
        <br>
        <div class="mb-3">
            
            <p>本人 {{member.name}} 謹此聲明，自願參加貴單位舉辦的（活動名稱），並願意遵守以下所有條款與約定。在此，本人郑重承諾遵守本切結書所規定的所有規定與要求，並將全力配合活動主辦方的運作。
                <br>

健康安全：本人保證自身身體狀況良好，並無任何潛在的傳染病或感染風險。若本人在活動期間出現任何症狀或發現任何健康風險，將立即通報活動主辦單位。
<br>

活動規定：本人將遵守活動主辦單位所制定的一切規定和指導方針，包括但不限於活動時間、地點、行程安排和參與活動的規範。
<br>
財物損失：本人了解在活動期間所攜帶的個人財物和物品由本人負責保管，若有任何損失或損壞，活動主辦單位將不負擔任何賠償責任。
<br>
活動安全：本人明白參加活動可能存在某些風險，包括但不限於身體受傷、財物損失等，並願意自行承擔相關風險。本人同意在活動期間嚴格遵守活動主辦單位的安全指示，確保自身和他人的安全。</p>
        </div>
    <div class="form-check">
        <input class="form-check-input" type="checkbox" value="" id="agreeCheck">
        <label class="form-check-label" for="agreeCheck">同意此聲明 </label>
    </div>

    <button class="btn btn-primary d-none" click="send" id="confirmBtn" data-bs-toggle="modal" data-bs-target="#staticBackdrop">確認報名</button>

</div>



<!-- Modal -->
<div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
<div class="modal-dialog">
<div class="modal-content d-flex justify-content-center align-items-center flex-colum">
  <div class="modal-header">
    <h1 class="modal-title fs-5" id="staticBackdropLabel">再次確認報名並前往付款頁面</h1>
  </div>
  <div class="modal-body  n">
    <form id="payForm" action="https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5" method="post">
        <input type="hidden" name="MerchantID" :value="payInfo.MerchantID ">
        <input type="hidden" name="MerchantTradeNo" :value="payInfo.MerchantTradeNo">
        <input type="hidden" name="MerchantTradeDate" :value="payInfo.MerchantTradeDate">
        <input type="hidden" name="PaymentType" :value="payInfo.PaymentType">
        <input type="hidden" name="TotalAmount" :value="payInfo.TotalAmount">
        <input type="hidden" name="TradeDesc" :value="payInfo.TradeDesc">
        <input type="hidden" name="ItemName" :value="payInfo.ItemName">
        <input type="hidden" name="ReturnURL" :value="payInfo.ReturnURL">
        <input type="hidden" name="ChoosePayment" :value="payInfo.ChoosePayment">
        <input type="hidden" name="EncryptType" :value="payInfo.EncryptType">
        <input type="hidden" name="ClientBackURL" :value="payInfo.ClientBackURL">
  
        <input type="hidden" name="CheckMacValue" :value="payInfo.CheckMacValue">

        <div class="mb-3 d-flex gap-3">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">取消</button>
            <button type="button" class="btn btn-primary" id="payButton" @click="submitPayment">確認</button>
        </div>
     </form>
  </div>

</div>
</div>
</div>
 
</template>

<script setup>
import axios from 'axios';
import{ref, reactive, onMounted} from 'vue';
const memberId = ref("23"); 
const activityId = ref("3"); 

const member = reactive({
name:"",
gender:"",
commonAddress:"",
mobile:"",
email:"",
birthday:""
})
const loadMember = async (id)=>{
axios.get(`https://localhost:7183/api/Activity/SignUp${id}`)
    .then(res=>{
        console.log(res.data);
        const datas = res.data;
        member.name = datas.name;
        member.commonAddress = datas.commonAddress;
        member.mobile = datas.mobile;
        member.email = datas.email;
        member.birthday = datas.birthday;
        member.gender = datas==true? "男":"女";    
        console.log(member.gender);       
    })
    .catch(err=>{
        console.log(err);
    })
}
loadMember(memberId.value);
//確保整個頁面的html都跑完，才開始執行onMounted裡面的js
onMounted(()=>{
const agreeCheck = document.querySelector("#agreeCheck");
const confirmBtn = document.querySelector("#confirmBtn");
agreeCheck.addEventListener("change", function(){
    if (agreeCheck.checked){
        confirmBtn.classList.remove("d-none");
    }else{
        confirmBtn.classList.add("d-none");
        
    }
});

})

const payInfo = reactive({
MerchantID:"",
MerchantTradeNo:"",
MerchantTradeDate:"",
PaymentType:"",
TotalAmount:"",
TradeDesc:"",
ItemName:"",
ReturnURL:"",
ChoosePayment:"",
EncryptType:"",
ClientBackURL:"",
CheckMacValue:""
})

axios.get("https://localhost:7183/api/Payment")
.then(res=>{

    console.log(res.data);
    console.log(res);
    
    const payresult = res.data;
    payInfo.MerchantID = payresult.MerchantID;
    console.log("畫面的ID="+payInfo.MerchantID);
    payInfo.MerchantTradeNo = payresult.MerchantTradeNo;
    payInfo.MerchantTradeDate = payresult.MerchantTradeDate;
    payInfo.PaymentType = payresult.PaymentType;
    payInfo.TotalAmount = payresult.TotalAmount;
    payInfo.TradeDesc = payresult.TradeDesc;
    payInfo.ItemName = payresult.ItemName;
    payInfo.ReturnURL = payresult.ReturnURL;
    payInfo.ChoosePayment = payresult.ChoosePayment;
    payInfo.EncryptType = payresult.EncryptType;
    payInfo.ClientBackURL = payresult.ClientBackURL;
    payInfo.CheckMacValue = payresult.CheckMacValue;
})
.catch(err=>{
    console.log(err);
})



const submitPayment = () => {
    document.querySelector("#payForm").submit();
    const formData = {
        MemberID:memberId.value,
        MerchantTradeNo:payInfo.MerchantTradeNo,
        MerchantID:payInfo.MerchantID,
        TotalAmount:payInfo.TotalAmount,
        MerchantTradeDate:payInfo.MerchantTradeDate,
        PaymentType:payInfo.PaymentType,
        CheckMacValue:payInfo.CheckMacValue,
        ItemName:payInfo.ItemName,
        TradeDesc:payInfo.TradeDesc,
        ActivityId:activityId.value 
    };
    console.log(formData);
   axios.post().then().catch(err=>{
    console.log(err);
   })
};

</script>

<style>
.space{
margin-right: 10px;
}

</style>
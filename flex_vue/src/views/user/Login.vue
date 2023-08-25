<template>
  <navBar></navBar>
  <div class="container loginBox" v-if="loginBox">
    <div class="loginText">
      <h4>Flex Your Journey. Join us!</h4>
    </div>
    <div class="from-group" v-if="errors.length">
      <ul class="mb-3 errorsText">
        <span v-for="error in errors" class="text-danger">{{ error }}</span>
      </ul>
    </div>
    <!-- 欄位 -->
    <div class="from-group mb-3">
      <label for="account" class="mb-1" v-if="accInput">帳號</label>
      <input
        type="text"
        name="account"
        v-if="accInput"
        id="account"
        v-model="account"
        class="form-control"
        placeholder="手機號碼/帳號/Email"
      />
    </div>
    <div class="from-group mb-3" v-if="validated">
      <label for="password" class="mb-1">密碼</label>
      <input
        type="password"
        name="password"
        id="password"
        v-model="password"
        class="form-control"
        placeholder="輸入6-20碼英數字"
      />
    </div>
    <!-- 00再次輸入確認密碼 -->
    <div class="from-group mb-3" v-if="nameInput">
      <label for="name">姓名</label>
      <input
        type="text"
        id="name"
        v-model="name"
        class="form-control"
        placeholder="ex.吳哈哈"
      />
    </div>
    <div class="from-group mb-3" v-if="unValidated">
      <label for="email">信箱</label>
      <input
        type="email"
        name="email"
        id="email"
        v-model="email"
        class="form-control"
        placeholder="ex.fuen28flex@gmail.com"
      />
    </div>
    <div class="from-group mb-3" v-if="birInput">
      <label for="birthday">生日</label>
      <input
        type="text"
        name="birthday"
        id="birthday"
        v-model="birthday"
        class="form-control"
        placeholder="生日套用日期插件阿"
      />
    </div>
    <div class="from-group mb-3" v-if="mobInput">
      <label for="mobile">手機</label>
      <input
        type="text"
        id="mobile"
        v-model="mobile"
        class="form-control"
        placeholder="ex.0912345678"
      />
    </div>
    <div class="from-group mb-3" v-if="addressInput">
      <label for="address">收件地址</label>
      <input
        type="text"
        id="address"
        v-model="address"
        class="form-control"
        placeholder="收件地址套用選項按鈕阿"
      />
    </div>
    <!-- 按鈕 -->
    <div class="from-group mb-3">
      <button
        type="submit"
        class="btn logAndRegBtn"
        v-if="registered"
        v-show="logAndRegBtn"
        @click="ValidatedIdentity"
      >
        登入 / 註冊
      </button>
      <button
        type="submit"
        class="btn logBtn"
        @click="Login"
        v-if="!registered"
      >
        登入
      </button>
      <div class="forgetPwd" v-if="forgetPwd" @click="forgetPwdClick">
        <a href="#" class="underline">忘記密碼 ?</a>
      </div>
      <button
        type="submit"
        class="btn btn btn-outline-dark registerBtn"
        @click="register"
        v-if="unRegistered"
      >
        註冊
      </button>
    </div>
    <div>
      <p>或</p>
    </div>
    <div class="from-group mb-3">
      <button
        type="submit"
        class="btn btn-info googleBtn"
        @click="ValidatedIdentity"
      >
        我是假的google登入
      </button>
    </div>
    <div class="secret">
      <div>擁有帳號即表示你同意</div>
      <a href="#" class="underline">會員權益聲明</a>
      <a href="#">與</a>
      <a href="#" class="underline">隱私權及網站使用條款</a>
    </div>
  </div>
  <!-- 忘記密碼請收驗證信 -->
  <forgetPwdAndSetPwd
    v-if="forgetPwdSetPwd"
    :email="email"
  ></forgetPwdAndSetPwd>
  <!-- 收完驗證信重新設定密碼 -->
  <!-- <resetPwd></resetPwd> -->
</template>

<script setup>
import axios from 'axios';
import navBar from '@/components/home/navBar.vue';
import { ref, onMounted } from 'vue';
import forgetPwdAndSetPwd from '@/components/user/forgetPwdAndSetPwd.vue';
// import resetPwd from '@/components/user/resetPwd.vue';

//pinia
import { useRouter } from 'vue-router';
import { storeToRefs } from 'pinia';
import { useGetApiDataStore } from '@/stores/useGetApiDataStore.js';

axios.defaults.withCredentials = true;

const getApiStore = useGetApiDataStore();
const { setMemberUsername } = getApiStore;
const { setLoginSuccess } = getApiStore;
const router = useRouter();

const userAcc = ref(null);
const loggedInUser = ref(null);

onMounted(() => {
  //檢查本地儲存是否有登錄信息
  const storedUser = localStorage.getItem('loggedInUser');
  //console.log('storedUser', storedUser);

  if (storedUser) {
    loggedInUser.value = JSON.parse(storedUser);
    // 同步到 pinia store
    getApiStore.setMemberUsername(loggedInUser.value.username);
    getApiStore.setMemberUsername(loggedInUser.value.memberId);
    console.log('onMountedusername', loggedInUser.value.username);
    console.log('onMountedmemberId', loggedInUser.value.memberId);
    setLoginSuccess(true);
  }
});

const errors = ref([]);
const userData = ref([]);
const accInput = ref(true);
const validated = ref(false); //初始化狀態為不顯示
const unValidated = ref(false);

const nameInput = ref(false);
const birInput = ref(false);
const mobInput = ref(false);
const addressInput = ref(false);
const logAndRegBtn = ref(true);
const registered = ref(true);
const forgetPwd = ref(false);
const unRegistered = ref(false);

//登入表單
const account = ref('');
const password = ref('');

//註冊表單
const name = ref('');
const email = ref('');
const birthday = ref('');
const mobile = ref('');
const address = ref('');

const baseAddress = 'https://localhost:7183/api';
const uri = `${baseAddress}/Users/Login`;
var loginData = {}; //儲存傳給後端的登入資料

function ValidatedIdentity() {
  //存入帳號
  localStorage.setItem('userAcc', account.value);

  //載入開始
  //alert('loginAndRegister');
  //未填寫
  if (account.value === '') {
    errors.value = [];
    errors.value.push('沒有填誰知道你是誰'); //結束載入
  } else {
    //已填寫
    errors.value = [];
    loginData.Account = account.value;
    //console.log(loginData.Account.value);

    axios
      .post(uri, loginData)
      .then((res) => {
        //結束載入
        userData.value = res.data;
        //console.log('帳號' + userData.value); //後端return的訊息

        //從回傳的資料中取得帳號並進行比較
        //已註冊
        if (userData.value == account.value) {
          //console.log('帳號驗證成功囉!');
          validated.value = true;
          accInput.value = false;
          registered.value = false;
          //忘記密碼
          forgetPwd.value = true;
        } else {
          //未註冊
          validated.value = false;
          //console.log('帳號驗證失敗');
          logAndRegBtn.value = false;

          errors.value = [];
          errors.value.push('484沒有註冊');
          validated.value = true;
          unValidated.value = true; //信箱
          nameInput.value = true;
          birInput.value = true;
          mobInput.value = true;
          addressInput.value = true;
          unRegistered.value = true;

          //todo驗證帳號是否唯一
          //todo寄信
        }
      })
      .catch((err) => {
        //結束載入
        alert('API請求失敗：' + err.message);
      });
  }
}
uri;
function Login() {
  //alert('Login');
  //todo是否與資料庫的密碼相符
  loginData.EncryptedPassword = password.value;
  //console.log(loginData);

  //未填寫密碼
  if (password.value === '') {
    errors.value = [];
    errors.value.push('密碼沒有填想怎樣');
    return;
  }

  axios
    .post(uri, loginData)
    .then((res) => {
      const jsonData = res.data;
      const userPassword = jsonData.find(
        (claim) => claim.Type === 'UserPassword'
      );
      //console.log(userPassword.Value);

      if (userPassword.Value === password.value) {
        //密碼正確
        errors.value = [];

        //取得登入者資料
        const userName = jsonData.find((claim) => claim.Type === 'FullName');
        const userId = jsonData.find((claim) => claim.Type === 'MemberId');

        //登入者資料包成物件
        const memberInfo = {
          username: userName.Value,
          memberId: userId.Value,
        };

        if (memberInfo) {
          setMemberUsername(memberInfo);
          //console.log('memberInfo', memberInfo);
        }
        //alert('登入成功啦港動~~~');
        handleSuccessfulLogin(memberInfo);
        router.replace({ path: '/' });
      }
    })
    .catch((err) => {
      errors.value = [];
      //errors.value.push('密碼錯誤');

      console.error(err);
      //todo錯誤累計三次
    });
}

function handleSuccessfulLogin(memberInfo) {
  // 將用戶信息轉成字串儲存到本地存儲中
  localStorage.setItem('loggedInUser', JSON.stringify(memberInfo));

  // 同步用戶信息到 pinia store
  loggedInUser.value = memberInfo;
  //console.log(loggedInUser.value);
}

const regUri = `${baseAddress}/Users/Register`;
var registerData = {};

function register() {
  //alert('register');
  //todo檢查信箱格式
  if (email.value === '') {
    errors.value = [];
    errors.value.push('給我確實填寫喔');
  } else {
    //格式驗證通過
    errors.value = [];
    registerData.Account = account.value;
    registerData.EncryptedPassword = password.value;
    registerData.Name = name.value;
    registerData.Email = email.value;
    registerData.Birthday = birthday.value;
    registerData.Mobile = mobile.value;
    registerData.CommonAddress = address.value;
    console.log(registerData);
    axios
      .post(regUri, registerData)
      .then((res) => {
        registerData.value = res.data;
        //console.log(registerData.value);
        //驗證信驗證完囉
        window.location.reload();
      })
      .catch((err) => {});
  }
}
const loginBox = ref(true);
const forgetPwdSetPwd = ref(false);
userAcc.value = localStorage.getItem('userAcc');
//console.log('userAcc', userAcc.value);

function forgetPwdClick() {
  loginBox.value = false;
  forgetPwdSetPwd.value = true;
  //呼叫api取得信箱
  if (userAcc.value) {
    const forgetUri = `${baseAddress}/Users/account/` + userAcc.value;
    //console.log(forgetUri);
    // 呼叫 API 取得信箱
    axios
      .get(forgetUri)
      .then((res) => {
        email.value = res.data; // 回傳信箱
        //console.log('Email:', email.value);
      })
      .catch((err) => {
        console.error('Error:', err);
      });
  }
}
</script>
<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Bebas+Neue&family=Lilita+One&display=swap');
.loginBox {
  width: 23%;
  justify-content: center;
  align-items: center;
  padding: 45px;
  margin-top: 150px;
  border: solid 1px;
}
.loginText {
  display: flex;
  justify-content: center;
}
.loginText > h4 {
  font-family: 'Bebas Neue', sans-serif;
  font-weight: bold;
  font-size: 40px;
}
.errorsText {
  display: flex;
  justify-content: center;
  padding: 0px;
  margin: 0px;
}
.logAndRegBtn {
  width: 100%;
  margin: auto;
  background-color: black;
  color: white;
}
.logBtn {
  width: 100%;
  margin: auto;
  background-color: black;
  color: white;
}
.googleBtn {
  width: 100%;
}

.registerBtn {
  width: 100%;
}

p {
  position: relative;
  text-align: center; /* 將文字置於中間 */
}

p::before,
p::after {
  content: '';
  position: absolute;
  top: 50%;
  width: 150px; /* 調整線段的長度 */
  height: 1px; /* 調整線段的粗細 */
  background-color: black; /* 設定線段的顏色 */
}

p::before {
  left: 0;
}

p::after {
  right: 0;
}
.forgetPwd {
  display: flex;
  justify-content: center;
  margin-top: 10px;
}
.underline {
  text-decoration-line: underline;
  color: rgba(var(--bs-link-color-rgb), var(--bs-link-opacity, 1));
  display: flex;
  justify-content: center;
}
.secret {
  margin: auto;
}
.secret div {
  display: flex;
  justify-content: center;
}
.secret a {
  display: flex;
  justify-content: center;
}
</style>

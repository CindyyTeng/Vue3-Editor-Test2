<template>
  <div class="container area">
    <div class="from-group">
      <ul class="mb-3 errorsText">
        <span v-for="error in errors" class="text-danger">{{ error }}</span>
      </ul>
    </div>
    <div class="text">
      <label for="resetPwd">重新設定登入密碼</label>
    </div>
    <input
      type="text"
      class="form-control mb-3"
      name="resetPwd"
      id="resetPwd"
      placeholder="輸入6-20碼英數字"
      v-model="resetPwd"
    />
    <div class="finish">
      <button type="submit" class="btn finishBtn" @click="finish">
        完成設定
      </button>
    </div>
  </div>
</template>

<script setup>
import router from '@/router/index.js';
import { ref, onMounted, defineProps } from 'vue';
import axios from 'axios';

const errors = ref([]);
const resetPwd = ref('');
const userAcc = ref(null);
userAcc.value = localStorage.getItem('userAcc');

const baseAddress = 'https://localhost:7183/api';
const uri = `${baseAddress}/Users/ResetPwd`;
function finish() {
  //欄位驗證
  if (resetPwd.value == '') {
    //alert('finish');
    errors.value = [];
    errors.value.push('設定密碼看不懂嗎');
  } else {
    //欄位驗證通過
    errors.value = [];
    //新設密碼送到後端
    //console.log(uri);

    const resetUserData = {};
    resetUserData.account = userAcc.value;
    resetUserData.encryptedPassword = resetPwd.value;
    axios
      .put(uri, resetUserData)
      .then((res) => {
        console.log(res.data);
      })
      .catch((err) => {
        console.log(err);
      });
    //新密碼設定密碼完成，導入登入畫面
    router.push({ path: '/login' });
  }
}
</script>

<style scoped>
.area {
  border: 1px solid;
  max-width: 20%;
  padding: 30px;
  margin-top: 200px;
}
.resetPwd {
  margin-bottom: 5px;
}
.finish {
  margin-bottom: 10px;
  display: flex;
  justify-content: center;
}
.text {
  margin-bottom: 10px;
}
.errorsText {
  display: flex;
  justify-content: center;
  padding: 0px;
  margin: 0px;
}
.finishBtn {
  background-color: #bb3e20;
  color: white;
}
</style>

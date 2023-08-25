import { defineStore } from 'pinia';
import { ref } from 'vue';
import axios from 'axios';
import Cookies from 'js-cookie';
//import router from '../router'; 打開才可以執行，但是打開會有可怕的超連結樣式

//第一個參數getApiData指的是整個store的id
//官方建議pinia function 為use開頭
export const useGetApiDataStore = defineStore('getApiData', {
  state: () => {
    return {
      loginSuccess: ref(false),
      memberInfo: {
        username: null,
        memberId: null,
      },
    };
  },
  actions: {
    setLoginSuccess(value) {
      this.loginSuccess = value; //顯示登入字
      console.log('轉成大頭' + this.loginSuccess);
    },
    handleLogout() {
      this.memberInfo = null;
      this.loginSuccess = false;
      Cookies.remove('user_cookie');

      const baseAddress = 'https://localhost:7183';
      const uri = `${baseAddress}/api/Users/Logout`;
      axios
        .delete(uri)
        .then((res) => {
          //console.log('delet API');
        })
        .catch((err) => {
          console.error(err);
        });

      //router.push({ path: '/login' });
      window.location.href = '/login';
    },
    setMemberUsername(memberInfo) {
      this.memberInfo = memberInfo;
    },
  },
  getters: {
    getMemberId: (state) => state.memberInfo.memberId,
  },
});

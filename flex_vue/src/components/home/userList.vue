<template>
  <div class="list">
    <ul class="userList-items">
      <li class="mb-2"><router-link to="/user">個人資料</router-link></li>

      <li class="mb-2"><a href="/orders">訂單查詢/申請退貨</a></li>

      <li class="mb-2"><router-link to="/favorites">收藏清單</router-link></li>

      <li class="mb-2"><a href="javascript:;">積分中心</a></li>

      <li class="mb-2"><a href="javascript:;">優惠券</a></li>

      <li class="mb-2"><a href="javascript:;">瀏覽紀錄</a></li>

      <li class="mb-2"><a href="javascript:;">購物車</a></li>
    </ul>
    <button class="logout" type="submit" @click="logout">登出</button>
  </div>
</template>

<script setup>
import { storeToRefs } from 'pinia';
import { useGetApiDataStore } from '@/stores/useGetApiDataStore.js';
const getApiStore = useGetApiDataStore();
const { handleLogout } = getApiStore; //function透過store取資料

function logout() {
  // 登入狀態
  localStorage.removeItem('loggedInUser');
  //呼叫 Pinia 的登出函數
  handleLogout();
}
</script>

<style scoped>
.list {
  background-color: #fff;
  z-index: 9999;
  /* 設置較大的 z-index 值 */
  border: solid 2px;
  position: absolute;
  right: 0;
  top: 100%;
  padding: 30px 40px;
}

.userList-items {
  display: flex;
  flex-direction: column;
  justify-content: center;
  padding: 0;
  margin: 0;
}

.userList-items li:not(:last-child) {
  border-bottom: 1px solid gray;
  height: 23px;
}

.userList-items li a {
  white-space: nowrap;
}

.logout {
  width: 200px;
  border: solid 1px;
}

.logout:hover {
  background-color: black;
  color: white;
}
</style>

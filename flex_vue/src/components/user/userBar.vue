<template>
  <!-- 77這邊的樣式不要寫在nav -->
  <div class="container">
    <div class="nav nav-justified">
      <ul class="nav nav-tabs">
        <li class="nav-item active" :aria-current="page">
          <router-link to="/user">個人資料</router-link>
        </li>
        <!-- 77如果選到某一個怎麼換顏色 -->
        <li class="nav-item">
          <a name="order" href="/orders">訂單查詢 / 申請退貨</a>
        </li>

        <li class="nav-item" aria-disabled="true">
          <router-link to="/favorites" name="favorit">收藏清單</router-link>
        </li>

        <li class="nav-item"><a name="rank" :href="page">積分中心</a></li>

        <li class="nav-item"><a name="free" :href="page">優惠券</a></li>

        <li class="nav-item">
          <a name="history" :href="page">瀏覽紀錄</a>
        </li>

        <li class="nav-item"><a name="kart" :href="page">購物車</a></li>
      </ul>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue';
const page = ref('favorites');
</script>

<style scoped>
.nav {
  background-color: #333;
  /* 這裡使用你想要的顏色值 */
}

.nav-justified {
  margin-bottom: 50px;
  display: flex;
  justify-content: center;
}

.nav ul {
  background-color: #f5f5f5;
  border-bottom: solid 1px lightgray;
  padding: 15px;
  display: flex;
  width: 100%;
  align-items: center;
}

.nav-tabs {
  padding: 20px;
  justify-content: center;
}

.nav-tabs li {
  margin-right: 50px;
}

.pills {
  background-color: rebeccapurple;
  width: 20%;
  display: flex;
  justify-content: center;
}
</style>

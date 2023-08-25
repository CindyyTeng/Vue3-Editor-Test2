<template>
    <aside class="side-bar">
      <ul class="list-unstyled">
        <li
          v-for="shoescategory in shoescategories"
          :key="shoescategory.shoesCategoryId"
          class="shoescategory"
        >
          <router-link :to="getCategoryRoute(shoescategory.shoesCategoryName)">{{
            shoescategory.shoesCategoryName
          }}</router-link>
          <!-- <a :href="'/men/' + categoryName">{{ categoryName }}</a> -->       
        </li>
      </ul>
    </aside>
  </template>
  
  <script setup>
  import axios from "axios";
  import { onMounted, ref } from "vue";
  import { useRoute, useRouter } from "vue-router";
  
  const baseAddress = import.meta.env.VITE_API_BASEADDRESS;
  const route = useRoute();
  const router = useRouter();
  const shoescategories = ref({});
  const localStorageKey = "originalPath";
 
  //設置路徑
if (route.path.includes("/CustomeShoes")) {
  localStorage.setItem(localStorageKey, "/CustomeShoes");
}

// 使用 ref 來儲存 localStorage 中的值
const originalPath = ref(localStorage.getItem(localStorageKey));

const getCategoryRoute = (categoryName) => {
  return {
    path: originalPath.value,
    query: {
      categoryName: encodeURIComponent(categoryName)
    }
  };
};

const getshoesCategory = async () => {
  try {
    const response = await axios.get(`${baseAddress}api/ShoesCategory`);
    shoescategories.value = response.data; // 將整個回應資料賦值給 shoescategories
    console.log(shoescategories.value)
  } catch (error) {
    alert("無法獲取分類資料，請稍後再試。");
    console.error(error); // 輸出錯誤到控制台
  }
};
  
  onMounted(() => {
    originalPath.value = localStorage.getItem(localStorageKey);
    getshoesCategory();
  });
  </script>
  
  <style>
  .side-bar{
    position: fixed;
    left: 0;
    top: 0;
    height: 100vh;
    width: 200px;
  }
  .shoescategory a {
    color: #83521e;
    text-decoration: none;
    outline: none;
    font-size: 20px;
    font-weight: bold;
  }
  
  </style>
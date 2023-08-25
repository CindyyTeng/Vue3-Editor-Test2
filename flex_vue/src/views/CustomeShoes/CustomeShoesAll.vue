<template>
    <ul class="d-flex flex-wrap">
      <li v-for="card in cards" :key="card.shoesproductId" class="card text-center">
        <ShoesCard :card="card"></ShoesCard>
      </li>
    </ul>
  </template>
  
  <script setup>
  import ShoesCard from "@/components/customeShoes/ShoesCard.vue";
  import axios from "axios";
  import { onMounted, ref, computed, watchEffect, watch } from "vue";
  import { useRouter, useRoute } from "vue-router";
  
  const baseAddress = import.meta.env.VITE_API_BASEADDRESS;
  
  const cards = ref([]);
  
  //監聽選擇的分類資料，一旦資料變動，重新加載 cards
  //根據選擇的分類資料重新叫用API
  const route = useRoute();
  
  // 獲取當前的路由參數
  //const id = route.params.id;
  
  // 獲取查詢參數
  //const query = route.query;
  
  watch(
    () => route.params, //route.query,
    () => {
      loadShoesProducts();
    }
  );
  
  const loadShoesProducts = async () => {
    let url;
    if (route.params.shoescategoryName) {
      url = `${baseAddress}api/ShoesCategory/customeshoes?shoescategoryName=${route.params.shoescategoryName}`;
    }  else {
      url = `${baseAddress}api/CustomeShoes/GetAll`;
    }
    console.log(url);
    await axios
      .get(url)
      .then((response) => {
        //console.log(response.data);
        cards.value = response.data;
      })
      .catch((error) => {
        alert(error);
      });
  };
  onMounted(() => {
    loadShoesProducts();
  });
  </script>
  
  <style></style>
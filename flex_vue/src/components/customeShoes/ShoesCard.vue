<template>
   <router-link :to="'/CustomeShoes' + '/detail/' + card.shoesProductId">
    <div class="img">
      <!-- <router-link to="/detail/123"> -->
      <img :src="imgBaseUrl + 'Public/Img/' + card.firstImgPath" class="card-img-top mx-auto d-block"
        :title="card.shoesName" />
      <!-- </router-link> -->
    </div>
    <div class="card-body">
      <div class="card-title">{{ card.shoesName }}</div>
      <div class="card-category">{{ card.shoesCategoryName }}</div>
      <span class="" v-if="card.shoesUnitPrice != null">
        NT:{{ card.shoesUnitPrice }}
      </span>      
    </div>
  </router-link>
  </template>
  
  <script setup>
  import { computed, ref } from "vue";
  
  const props = defineProps(["card"]);
  const card = computed(() => props.card);
  const imgBaseUrl = ref(import.meta.env.VITE_API_BASEADDRESS);
  </script>
  
  <style>
  .card .img {
    min-height: 250px;
    max-height: 250px;
    width: 220px;
    overflow: hidden;
  }
  
  .card img {
    transition: transform 1s;
  }
  
  .card img:hover {
    transform: scale(1.2);
  }
  
  .card {
    overflow: hidden;
    margin-bottom: 35px;
    margin-right: 35px;
    padding: 0;
    border: none;
    height: 350px;
    width: 220px;
  }
  
  .nav-category {
    width: 165px;
  }
  
  .container-body {
    width: 1080px;
  }
  
  .text-red {
    color: red;
  }
  </style>
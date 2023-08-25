<template>
  <navBar></navBar>
  <div class="container mt-4">
    <div class="row">
      <div class="nav-category">
        <categoryNavBar style="position: relative"></categoryNavBar>
        <searchPrice
          style="position: absolute; top: 460px; width: 140px"
        ></searchPrice>
      </div>
      <router-view></router-view>
    </div>
  </div>
</template>

<script setup>
import navBar from "@/components/home/navBar.vue";
import categoryNavBar from "@/components/product/CategoryNavBar.vue";
import searchPrice from "@/components/product/SearchPrice.vue";
</script>

<style></style>

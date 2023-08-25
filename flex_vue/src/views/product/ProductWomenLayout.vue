<template>
  <navBar></navBar>
  <div class="container mt-4">
    <div class="row">
      <div class="nav-category">
        <categoryNavBar></categoryNavBar>
      </div>
      <router-view></router-view>
    </div>
  </div>
</template>

<script setup>
import navBar from "@/components/home/navBar.vue";
import categoryNavBar from "@/components/product/CategoryNavBar.vue";
</script>

<style></style>

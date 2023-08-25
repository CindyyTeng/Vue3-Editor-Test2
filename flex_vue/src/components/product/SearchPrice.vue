<template>
  <div>
    <h5 class="searchBoxColor">價格搜尋:</h5>
    <input
      type="number"
      v-model="minPrice"
      min="0"
      class="form-control mb-1"
      placeholder="最低價格"
    />
    <input
      type="number"
      v-model="maxPrice"
      min="0"
      class="form-control mb-3"
      placeholder="最高價格"
    />
    <button class="form-control setBtnColor setBtnColorHover">送出</button>
  </div>
</template>

<script setup>
import { ref } from "vue";
const minPrice = ref("");
const maxPrice = ref("");
</script>

<style>
.searchBoxColor {
  color: #83521e;
}
.setBtnColor {
  background-color: #83521e;
  color: white;
}

.setBtnColor:hover {
  background-color: white;
  color: #83521e;
  border: 1px solid #83521e;
}
</style>

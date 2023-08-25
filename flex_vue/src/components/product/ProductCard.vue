<template>
  <router-link :to="path + '/detail/' + card.productId">
    <div class="productCardImg">
      <img
        :src="imgBaseUrl + 'Public/Img/' + card.firstImgPath"
        class="card-img-top mx-auto d-block"
        :title="card.productName"
      />
    </div>
    <div class="card-body">
      <div class="card-title">{{ card.productName }}</div>
      <span class="text-decoration-line-through" v-if="card.unitPrice != null">
        NT:{{ card.unitPrice }}
      </span>
      <span v-if="card.unitPrice != null"> 活動價 </span>
      <span v-bind:class="{ 'text-red': card.unitPrice != null }">
        NT:{{ card.salesPrice }}
      </span>
    </div>
  </router-link>
</template>

<script setup>
import { computed, ref } from "vue";
import { useRoute } from "vue-router";

const props = defineProps(["card"]);
const card = computed(() => props.card);
const imgBaseUrl = ref(import.meta.env.VITE_API_BASEADDRESS);
const route = useRoute();
const path = ref("");
path.value = localStorage.getItem("originalPath");
//console.log(path.value);
</script>

<style>
.card .productCardImg {
  min-height: 250px;
  max-height: 250px;
  width: 220px;
  overflow: hidden;
}

.card img {
  transition: transform 1s;
  min-height: 250px;
  max-height: 250px;
  width: 220px;
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

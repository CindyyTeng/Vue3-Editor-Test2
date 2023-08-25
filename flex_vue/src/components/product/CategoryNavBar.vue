<template>
  <nav>
    <ul class="list-unstyled">
      <li
        v-for="(subCategories, categoryName) in categories"
        :key="categoryName"
        class="category"
      >
        <router-link :to="`${originalPath}?categoryName=${categoryName}`">{{
          categoryName
        }}</router-link>
        <!-- <router-link :to="'/men/' + categoryName">{{
          categoryName
        }}</router-link> -->
        <!-- <a :href="'/men/' + categoryName">{{ categoryName }}</a> -->
        <ul class="list-unstyled">
          <li
            class="subCategory"
            v-for="subCategoryName in subCategories"
            :key="subCategoryName"
          >
            <router-link
              :to="`${originalPath}?categoryName=${categoryName}&subCategoryName=${subCategoryName}`"
              >{{ subCategoryName }}</router-link
            >
            <!-- <router-link :to="'/men/' + categoryName + '/' + subCategoryName">{{
              subCategoryName
            }}</router-link> -->
          </li>
        </ul>
      </li>
    </ul>
  </nav>
</template>

<script setup>
import axios from "axios";
import { onMounted, ref } from "vue";
import { useRoute } from "vue-router";

const baseAddress = import.meta.env.VITE_API_BASEADDRESS;
const route = useRoute();
const categories = ref({});

//創建本機共用屬性
const localStorageKey = "originalPath";

//設置路徑
if (route.path.includes("/men")) {
  localStorage.setItem(localStorageKey, "/men");
} else if (route.path.includes("/women")) {
  localStorage.setItem(localStorageKey, "/women");
} else if (route.path.includes("/kid")) {
  localStorage.setItem(localStorageKey, "/kid");
}

//取得原始路徑
const originalPath = localStorage.getItem(localStorageKey);

//console.log(originalPath);
let getCategory = async () => {
  await axios
    .post(`${baseAddress}api/Category${originalPath}`)
    .then((response) => {
      categories.value = response.data.categories;
    })
    .catch((error) => {
      alert(error);
    });
};

onMounted(() => {
  getCategory();
});
</script>

<style>
.category a {
  color: #83521e;
  text-decoration: none;
  outline: none;
  font-size: 20px;
  font-weight: bold;
}

.subCategory a {
  color: #83521e;
  text-decoration: none;
  outline: none;
  font-size: 15px;
  font-weight: 400;
}

ul li.subCategory {
  background-image: url("../../../public/LOGO/CategoryIcon.jpg");
  background-size: 13px 13px;
  background-repeat: no-repeat;
  padding-left: 20px;
  font-size: 15px;
  margin-bottom: 5px;
  line-height: 18px;
}
</style>

// 這邊匯入App.vue的東西
import "./assets/main.css";
import { useGetApiDataStore } from "@/stores/useGetApiDataStore.js";

import { createApp } from "vue";
import { createPinia } from "pinia";
import App from "./App.vue";
import router from "./router/index";
import CKEditor from "@ckeditor/ckeditor5-vue";
import { QuillEditor } from "@vueup/vue-quill";
import "@vueup/vue-quill/dist/vue-quill.snow.css";
import ElementPlus from 'element-plus';
import 'element-plus/dist/index.css';
const pinia = createPinia();
// 使用 Vue Router 和 Pinia
createApp(App)
  .use(router)
  .use(pinia)
  .use(QuillEditor)
  .use(ElementPlus)
  .mount("#app");

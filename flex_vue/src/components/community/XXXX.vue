<template>
  <QuillEditor
    theme="snow"
    :modules="modules"
    :toolbar="toolbarOptions"
    class="quill-editor"
    contentType="html"
    v-model:content="editorContent"
  />
</template>

<script setup>
import { ref } from "vue";
import { QuillEditor } from "@vueup/vue-quill";
import "@vueup/vue-quill/dist/vue-quill.snow.css";
import axios from "axios";
import ImageUploader from "quill-image-uploader";

const toolbarOptions = [
  [{ header: [1, 2, 3, 4, 5, false] }], // custom button values
  ["bold", "italic", "underline", "strike"], // toggled buttons
  ["blockquote", "code-block"],
  [{ list: "ordered" }, { list: "bullet" }],
  [{ color: [] }, { background: [] }], // dropdown with defaults from theme
  ["image"],
  ["clean"], // remove formatting button
];

const props = defineProps({
  clearEditor: {
    type: Function,
  },
});


const editorContent = ref("");
const modules = {
  name: "imageUploader",
  module: ImageUploader,
  toolbar: toolbarOptions,
  // upload属性的值是一個函數
  options: {
    upload: (file) => {
      //resolve 和 reject 是Promise對象的兩個回調函数，他們用於處理Promise的狀態。
      return new Promise((resolve, reject) => {
        const formData = new FormData();
        formData.append("file", file);
        axios
          .post("https://localhost:7183/api/Products/uploadImage", formData, {
            headers: {
              "Content-Type": "multipart/form-data",
            },
            withCredentials: true,
          })
          .then((res) => {
            //console.log(res);
            let imgurLink = res.data;
            console.log(imgurLink);
            resolve(imgurLink); //将圖片链接通过 resolve 返回，表示上傳成功
            //resolve(value): 此函数用于将Promise狀態设置為已解决，其中 value 是解决的结果，通常是一个成功的结果值
          })
          .catch((err) => {
            reject("Upload failed");
            console.error("Error:", err);
          });
      });
    },
  },
};
</script>

<style>
</style>

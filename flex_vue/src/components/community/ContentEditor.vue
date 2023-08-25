<template>
  <div class="fg-black" style="--ck-border-radius: 0.25rem">
    <CKEditor :editor="ClassicEditor" v-model="editorData"></CKEditor>
  </div>
</template>

<script setup>
import { ref, watch, defineProps, defineEmits } from "vue";
import ClassicEditor from "@ckeditor/ckeditor5-build-classic";
import { component as CKEditor } from "@ckeditor/ckeditor5-vue";
//import { Base64Upload } from "../../../node_modules/@ckeditor/ckeditor5-upload";
class UploadAdapter {
  constructor(loader) {
    this.loader = loader;
  }

  upload() {
    return new Promise((resolve, reject) => {
      const reader = new window.FileReader();

      reader.addEventListener("load", () => {
        resolve({ default: reader.result });
        console.log(reader.result);
      });

      reader.addEventListener("error", (err) => {
        reject(err);
      });

      reader.addEventListener("abort", () => {
        reject();
      });

      this.loader.file.then((file) => {
        reader.readAsDataURL(file);
      });
    });
  }

  abort() {
    // Implement abort logic if needed.
  }
}

const uploader = (editor) => {
  editor.plugins.get("FileRepository").createUploadAdapter = (loader) =>
    new UploadAdapter(loader);
};

// ClassicEditor.create(document.querySelector("#editor"), {
//   cloudServices: {
//     tokenUrl: "https://example.com/cs-token-endpoint",
//     uploadUrl: "https://your-organization-id.cke-cs.com/easyimage/upload/",
//   },
//   extraPlugins: [Base64Upload], // 将 Base64Upload 插件添加到 extraPlugins 中
//   // ...（其他选项）
// })
//   .then((editor) => {
//     // 编辑器初始化后的操作
//     editor.plugins.get("FileRepository").createUploadAdapter = uploader;
//   })
//   .catch((error) => {
//     // 错误处理
//   });

const { modelValue } = defineProps();
const emit = defineEmits(["update:modelValue"]);
const editorData = ref(modelValue || "");

watch(editorData, () => {
  emit("update:modelValue", editorData.value);
});
const baseAddress = import.meta.env.VITE_API_BASEADDRESS;
const editorConfig = {
  extraPlugins: [uploader],
  ckfinder: {
    uploadUrl: `${baseAddress}/Public/Img`,
    // 後端的上傳圖片 API 路徑
    options: {
      resourceType: "Images",
      // 限定類型為圖片
    },
  },
  toolbar: [
    "heading",
    "|",
    "bold",
    "italic",
    "link",
    "bulletedList",
    "numberedList",
    "blockQuote",
    "image",
  ],
  heading: {
    options: [
      { model: "paragraph", title: "Paragraph", class: "ck-heading_paragraph" },
      {
        model: "heading1",
        view: "h1",
        title: "Heading 1",
        class: "ck-heading_heading1",
      },
      {
        model: "heading2",
        view: "h2",
        title: "Heading 2",
        class: "ck-heading_heading2",
      },
    ],
  },
};
</script>

<style>
.ck-editor_editable {
  min-height: 500px;
}
</style>

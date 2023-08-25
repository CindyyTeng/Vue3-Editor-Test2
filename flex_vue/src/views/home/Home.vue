<template>

  <input v-model="postTitle" placeholder="請輸入標題" class="inputTitle" />
  <QuillEditor
    theme="snow"
    :modules="modules"
    :toolbar="toolbarOptions"
    class="quill-editor"
    contentType="html"
    v-model:content="editorContent"
    ref="quill"
    @ready="quill"
  />
  <button @click="showArticle">確定</button>
  <br />
  <br />

  <el-container>
    <!-- 側邊攔 -->
    <el-aside width="300px">
      <h2>話題分類</h2>
      <ul>
        <li><a href="#">路跑</a></li>
        <li><a href="#">自行車</a></li>
        <li><a href="#">健行</a></li>
      </ul>
    </el-aside>

    <!-- 主要内容区域 -->
    <el-main>
      <h1>貼文</h1>
      <div class="forum-posts">
        <!-- 使用 v-for 渲染貼文列表 -->
        <div v-for="post in forumPosts" :key="post.id" class="post">
          <h3>{{ post.title }}</h3>
          <div v-html="renderPostContent(post.content)"></div>
        </div>
      </div>
    </el-main>
  </el-container>
  
</template>

<script setup>

import { ref, nextTick } from "vue";

// import ElementPlus from "element-plus";
// import "element-plus/dist/index.css";

const postTitle = ref("");
const quill = ref(null);

const forumPosts = ref([
  {
    id: 1,
    title: "標題一",
    content: "貼文一的內容。",
  },
  {
    id: 2,
    title: "標題二",
    content: "貼文二的內容。",
  },
  {
    id: 3,
    title: "標題三",
    content: "貼文三的內容。",
  },
]);

const clearEditor = () => {
  editorContent.value = "";
  postTitle.value = "";
  quill.value.setHTML("");
};

const showArticle = () => {
  const newPost = {
    id: forumPosts.length + 1,
    title: postTitle.value,
    content: editorContent.value,
  };
  forumPosts.value.push(newPost);
  nextTick(() => {
    clearEditor();
  });
};

//quill-vue
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
  ["link"],
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

const renderPostContent = (content) => {
  // 使用正则表达式将链接渲染为可点击的HTML链接
  const linkPattern = /<a href="([^"]+)">([^<]+)<\/a>/g;
  return content.replace(linkPattern, '<a href="$1" target="_blank">$2</a>');
};
</script>

<style>
.forum-posts ol li {
  list-style: decimal;
  margin: 0 20px;
}

.forum-posts ul li {
  list-style: disc;
  margin: 0 20px;
}

.forum-posts {
  padding: 20px;
  background-color: #f9f9f9;
  border: 1px solid #ddd;
}

.post {
  padding: 10px;
  margin-bottom: 20px;
  background-color: #fff;
  border: 1px solid #ddd;
}

.post h3 {
  font-size: 20px;
}

.post p {
  font-size: 16px;
  color: #333;
}

ul {
  list-style: none;
  padding: 0;
}

ul li {
  margin-bottom: 10px;
}

a {
  text-decoration: none;
  color: #007bff;
  font-weight: bold;
}

a:hover {
  text-decoration: underline;
}

.inputTitle {
  width: 100%;
  padding: 10px;
  margin-bottom: 10px;
}
</style>

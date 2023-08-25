<template>
  <div class="forum">
    <h1 class="forum-title">FLEX</h1>
    
    <!-- 發佈新文章 -->
    <div class="new-post">
      <h2>新貼文</h2>
      <input v-model="newPostTitle" class="post-input" placeholder="標題">
      <textarea v-model="newPostContent" class="post-input" placeholder="内容"></textarea>

      <tiptap v-model="content" />
      <div>
    <!-- <editor v-model="content" /> -->

    <div class="content">
      <h3>Content</h3>
      <pre><code>{{ content }}</code></pre>
    </div>
  </div>

      <button @click="createNewPost" class="post-button">發佈</button>
    </div>
    
    <!-- 文章列表 -->
    <div class="post-list">
      <h2>文章列表</h2>
      <ul>
        <li v-for="(post, index) in posts" :key="index" class="post">
          <h3 class="post-title">{{ post.title }}</h3>
          <p class="post-content">{{ post.content }}</p>
        </li>
      </ul>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import Tiptap from "@/components/community/Tiptap.vue";
// import Editor from '@/components/community/Editor.vue';

const content = '';













// // 定義 data
// const content = '<p>A Vue.js wrapper component for tiptap to use <code>v-model</code>.</p>'




const newPostTitle = ref('');
const newPostContent = ref('');
const posts = ref([]);

const createNewPost = () => {
  if (newPostTitle.value && newPostContent.value) {
    posts.value.push({ title: newPostTitle.value, content: newPostContent.value });
    newPostTitle.value = '';
    newPostContent.value = '';
  }
};
</script>

<style scoped>
.tiptap {
  > * + * {
    margin-top: 0.75em;
  }
  code {
    background-color: rgba(#616161, 0.1);
    color: #616161;
  }
}

.content {
  padding: 1rem 0 0;

  h3 {
    margin: 1rem 0 0.5rem;
  }

  pre {
    border-radius: 5px;
    color: #333;
  }

  code {
    display: block;
    white-space: pre-wrap;
    font-size: 0.8rem;
    padding: 0.75rem 1rem;
    background-color:#e9ecef;
    color: #495057;
  }
}
.forum {
  max-width: 800px;
  margin: 0 auto;
  padding: 20px;
  font-family: Arial, sans-serif;
}

.forum-title {
  text-align: center;
  font-size: 24px;
  margin-bottom: 20px;
}

.new-post {
  margin-bottom: 20px;
}

.post-input {
  width: 100%;
  padding: 10px;
  margin-bottom: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
}

.post-button {
  background-color: #007BFF;
  color: white;
  border: none;
  padding: 10px 20px;
  cursor: pointer;
  border-radius: 5px;
}

.post-button:hover {
  background-color: #0056b3;
}

.post-list {
  ul {
    list-style: none;
    padding: 0;
  }

  .post {
    border: 1px solid #ddd;
    padding: 10px;
    margin-bottom: 10px;
    background-color: #f9f9f9;
    border-radius: 5px;
  }

  .post-title {
    margin: 0;
    font-size: 20px;
  }

  .post-content {
    font-size: 16px;
  }
}
</style>
  

  

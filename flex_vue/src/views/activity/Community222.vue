<template>
    <ContentEditor v-model="content" />  
    
    <button @click="createPost">發文</button>

    <h2>文章列表</h2>
    <div v-if="posts.length">   
            <ul>
                <li v-for="(post, index) in posts" v-html="post"></li>
            </ul>  
    </div>
    
</template>

<script setup>
import {ref, watch} from 'vue';
import ContentEditor from '@/components/community/ContentEditor.vue';

const content = ref('');
// console.log(content);
const posts=ref([]);

const createPost = () =>{
    console.log(content);
    console.log(content._value);
    posts.value.push(content._value);

  
}
    
</script>
    

    
<style>
    
</style>
<template>
    <div class="row cardbody">
        <div v-for="(speaker, index) in cardsInfo" :key="index" class="card mb-3 col-4 cards" style="width: 18rem;">
        
            <img :src='imgBaseUrl+"/Public/Img/"+speaker.speakerImg' class="card-img-top" alt="...">
            <div class="card-body">
                <h5 class="card-title">{{ speaker.speakerName }}</h5>
                <!-- 使用計算屬性將日期格式化為所需形式 -->
                <p class="card-text">{{ speaker.fieldName }}</p>
                <a :href="'speakerInfo/'+speaker.speakerId" class="btn btn-primary">預約諮詢</a>
            </div>
        </div>
    </div>
</template>

<script setup>
    import axios from 'axios';
    import {ref, reactive, onMounted} from 'vue';
    const cardsInfo = ref([]);
    const imgBaseUrl = ref(import.meta.env.VITE_API_BASEADDRESS);

    onMounted(()=>{
        var uri = "https://localhost:7183/api/Reservation/index";
        axios.get(uri)
            .then(res=>{
                console.log(res.data);
                cardsInfo.value = res.data;
            })
            .catch(err=>{
                console.log(err);
            })
    })
</script>

<style>
 .cards {
            margin-right: 70px;
            margin-bottom: 70px;

        }

        .cardbody {
            margin: 100px
        }
    
</style>
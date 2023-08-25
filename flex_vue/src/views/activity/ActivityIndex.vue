<template>
<!-- 搜尋區 -->
<div class="search-container">
    <div class="row row2 ">
        <div class="col-3">
        <input v-model="searchArea" class="form-control search-input" placeholder="請輸入地區">
        </div>

        <div class="col-7">
            <VueMultiselect class="selectedBlock"
                v-model="selected"
                :options="options"
                :max="2"
                :searchable="false"
                :multiple="true"   
                placeholder="選擇運動類別，最多2項"    
            >
            </VueMultiselect>
        </div>

        <div class="col">
            <button class="btn btn-primary" @click="sendSelectedValues">搜尋</button>

        </div>

    </div>
</div>

    <!-- 湯圓區 -->
    <div class="row cardbody">
        <div v-for="(activity, index) in info" :key="index" class="card mb-3 col-4 cards" style="width: 18rem;">
        
            <img :src='imgBaseUrl+"/Public/Img/"+activity.imgPath' class="card-img-top" alt="...">
            <div class="card-body">
                <h5 class="card-title">{{ activity.activityName }}</h5>
                <!-- 使用計算屬性將日期格式化為所需形式 -->
                
                <p class="card-text"><font-awesome-icon icon="calendar-days" style="color: #ffd562" bounce  />{{ formatDate(activity.activityDate) }}</p>
                <a :href="'activityInfo/'+activity.activityId" class="btn btn-primary">前往報名</a>
            </div>
        </div>
    </div>

   

</template>
    
<script setup >
    import axios from 'axios';
    import {ref, reactive, onMounted} from 'vue';
    import { useRoute } from 'vue-router';
    import { library } from '@fortawesome/fontawesome-svg-core'
    import { fas } from '@fortawesome/free-solid-svg-icons'
    import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

    import VueMultiselect from 'vue-multiselect'
    const selected = ref(null);
    const options = ['路跑', '鐵人三項', '自行車', '健行', '登山', '瑜珈'];  
    const searchArea = ref("");
    let requestData=ref({});
   
    //icon
    library.add(fas)
    //得到照片路徑
    const imgBaseUrl = ref(import.meta.env.VITE_API_BASEADDRESS);
    const info = ref([]);


    // 按下搜尋鈕獲得最終篩選條件
    const sendSelectedValues = () => {
        const selectedVaules = getSelectedValues();
        requestData = {
            searchArea: searchArea.value || null,
            option1:selectedVaules.option1,
            option2:selectedVaules.option2 
        };
        console.log(requestData);
        //呼叫後端
        sendSearchRequest(requestData);
    };

    //把篩選條件變成物件
    function getSelectedValues() {
        const selectedOptions = selected.value || [];
        const option1 = selectedOptions[0] || null;
        const option2 = selectedOptions[1] || null;
        return {
            option1:option1,
            option2:option2
        }
}


    // 發送篩選條件到後端
    const sendSearchRequest = (data)=>{
        console.log(data);
        var uri = "https://localhost:7183/api/Activity/index";
        axios.post(uri, data)
             .then(res => {
                    console.log(res.data);
                    info.value = res.data;
                })
             .catch(err => {
                    console.log(err)
                })         
    }
    
    onMounted(()=>{
        var uri = "https://localhost:7183/api/Activity/index";
        // axios.post(uri, data)
        //      .then(res => {
        //             console.log(res.data);
        //             info.value = res.data;
        //         })
        //      .catch(err => {
        //             console.log(err)
        //         })     
        
        sendSearchRequest(requestData);
                
    });

            
                
         // 建一個計算屬性 formatDate來格式化日期 (不要時間，只要日期)
    const formatDate=(dateString)=>{
                const date = new Date(dateString);
                return date.toLocaleDateString(); // 格式化為本地化的日期字串
   }
                   
      
</script>
<style src="vue-multiselect/dist/vue-multiselect.css"></style>
<style>
        .cards {
            margin-right: 70px;
            margin-bottom: 70px;

        }

        .cardbody {
            margin: 100px
        }
     

        .selectedBlock {
           
            width: 100%; /* 调整为适当的宽度 */
        }

        .search-input{
            height: 43px;
        }

        .search-container {
            text-align: center; /* 水平居中 */
            margin-top: 50px; /* 调整上边距 */
            display: flex;
            position: relative;
        }
            
        .row2{
            position: absolute;
            display: flex;
            /* align-items: center; */
            margin-left: 10%;
        }
</style>
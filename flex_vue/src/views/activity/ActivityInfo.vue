<template>
  <!-- 上半部 【照片、活動日期、活動價格、報名鈕、倒數時間】-->
  <div class="top">
    <section class="featured row">
      <div
        class="col-md-8"
        data-aos="fade-left"
        data-aos-delay="400"
        data-aos-duration="800"
      >
        <div class="featured-img">
          <img
            :src="imgBaseUrl + '/Public/Img/' + activities.imgPath"
            alt="Featured 1"
            class="featured-big"
          />
        </div>
      </div>

      <div
        class="col-md-4"
        data-aos="fade-right"
        data-aos-delay="400"
        data-aos-duration="800"
      >
        <div class="title mb-5 me-5 text-end">
          <p class="date">{{ activities.activityDate }}</p>
        </div>
        <div class="price">
          <h2 class="text-center fw-bold">
            ${{ activities.activitySalePrice }}
          </h2>
          <h3 class="text-center fw-bold">
            ${{ activities.activityOriginalPrice }}
          </h3>
        </div>

        <div class="media-element d-flex justify-content-between"></div>
        <div class="countdown">
          <!-- 倒數計時器 -->
          <h5>距離報名結束還有
            <FlipDown
              :endDate=endDate
              :theme="2"
              :type="4"
              :timeUnit="['天','時','分','秒']"
              
              @timeUp="func"            
            />

            
          </h5>
        </div>
        <!-- 手刀報名按鈕 -->
        <div class="text-end">
          <a href="#" class="btn button">
            <!-- icon -->
            <span class="icon">
              <img
                width="100"
                height="100"
                src="https://img.icons8.com/stickers/100/exercise.png"
                alt="exercise"
              />
            </span>
            <!-- 按鈕文字 -->
            <span style="font-size: 50px"> 手刀報名 </span>
          </a>
        </div>
      </div>
    </section>
  </div>
  <!-- 下半部 【活動詳細資訊】 -->
  <div class="info">
    <h1 class="slanted-text">{{ activities.activityName }}</h1>
    <div class="mb-5">
      <h4>活動類別</h4>
      <p>{{ activities.activityCategoryName }}</p>
      <h4>活動主講人</h4>
      <p>{{ activities.speakerName }}</p>
    </div>
    <div class="mb-5">
      <h4>活動年齡</h4>
      <p>{{ activities.activityAge }}歲</p>
    </div>
    <div class="mb-5">
      <h4>活動地點</h4>
      <p>{{ activities.activityPlace }}</p>
    </div>
    <div class>
      <h4>活動簡介</h4>
      <p>{{ activities.activityDescription }}</p>
    </div>
  </div>
</template>

<script setup>
import axios from "axios";
import { ref, reactive, onMounted } from "vue";
import AOS from "aos";
import { useRoute } from "vue-router";
import FlipDown from 'vue-flip-down';

const bookingTime = ref([]);

const route = useRoute();
// const activityId = route.params.id;
const imgBaseUrl = ref(import.meta.env.VITE_API_BASEADDRESS);
//從F12的Vue去看的
// console.log(activityId);
AOS.init();



onMounted(async () => {
  const activityId = route.params.id;
  await loadActivities(activityId);
  await getActivityBookingTime(activityId);
  await loadActivities(activityId);
});


const activities = reactive({
  activityName: "",
  activityDate: "",
  speakerName: "",
  activityCategoryName: "",
  activityAge: "",
  activityPlace: "",
  activityDescription: "",
  imgPath: "",
  activitySalePrice: 0,
  activityOriginalPrice: 0,
});

//格式化日期的方法
const formatDateTime = (dateString) => {
    const date = new Date(dateString);
    
    const year = date.getFullYear();
    const month = date.getMonth() + 1;
    const day = date.getDate();
    
    const hours = date.getHours();
    const minutes = date.getMinutes();
    const seconds = date.getSeconds();

    // 組裝成我要的格式
    const formattedDateTime = `${year}/${month}/${day} ${hours}:${minutes}:${seconds}`;
    return formattedDateTime;
}

// console.log(formatDateTime("2023-08-18T10:50:35")); // 測試日期格式
// const testDate = formatDateTime("2023-08-18T10:50:35");
// console.log(testDate); //2023/8/18 10:50:35
// let testEndDate ;
// testEndDate = new Date(testDate);
let showDate;
let endDate = ref(null); 



//從後端抓活動資料
const loadActivities = async (id) => {
  axios
    .get(`https://localhost:7183/api/Activity/${id}`)
    .then((res) => {
      // console.log(res.data);
      const datas = res.data;
      console.log(datas);
      console.log(datas.activityName);
      activities.activityName = datas.activityName;
      activities.activityDate = datas.activityDate;
      activities.speakerName = datas.speakerName;
      activities.activityAge = datas.activityAge;
      activities.activityPlace = datas.activityPlace;
      activities.activityDescription = datas.activityDescription;
      activities.activitySalePrice = datas.activitySalePrice;
      activities.activityOriginalPrice = datas.activityOriginalPrice;
      activities.activityCategoryName = datas.activityCategoryName;
      activities.imgPath = datas.imgPath;
    })
    .catch((err) => {
      console.log(err);
    });
};
// loadActivities(activityId);
let startTime;
//從後端獲得報名起訖時間
const getActivityBookingTime = async(id)=>{
  axios.get(`https://localhost:7183/api/Activity/GetActivityBookingTime?id=${id}`)
      .then(res=>{
        console.log(res.data);
        startTime= res.data;     
        showDate = formatDateTime(startTime.activityBookStartTime);   
        console.log(showDate); //2023/5/7 0:0:0
        endDate.value = new Date(showDate);
      })
      .catch(err=>{
        console.log(err);
      })
}
// console.log(endDate);
//  getActivityBookingTime(activityId)
// console.log(startTime);




</script>

<style>
.featured {
  /* padding: 80px 0; */
  padding: 0 0;
  background-color: #fafafa;
  height: 530px;
}

/* .featured .row {
    background-color: red;
} */

.featured p {
  margin: 35px 0 53px;
}

.featured .media i {
  font-size: 24px;
}

.featured .media h5 {
  text-transform: uppercase;
  color: #26264b;
}

.featured .btn {
  margin-top: 80px;
}

.featured-img {
  position: relative;
  /* height: 530px; */
  width: 100%;
  height: 100%;
  /* margin-top: 60px; */
  /* background-color: #ee1515; */
}

.featured-big {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  object-fit: contain;
}

.fa-magic:before {
  content: "";
}

.media-body {
  flex: 1;
}

.media {
  display: flex;
  align-items: flex-start;
}

* {
  box-sizing: border-box;
}

body {
  min-height: 100vh;
}

.info {
  margin: 10%;
}

.slanted-text {
  position: relative;
  /* 設定相對定位 */
  display: inline-block;
  /* 讓元素內容不會佔據整行 */
}

.slanted-text::after {
  content: "";
  /* 必需的content屬性，用於創建偽元素 */
  position: absolute;
  /* 設定絕對定位，以相對活動名稱位置 */
  top: 0;
  right: -10px;
  /* 距離活動名稱的偏移距離，根據需要調整 */
  bottom: 0;
  width: 700px;
  /* 斜方形的寬度，根據需要調整 */

  background-color: #fff957;
  /* 斜方形的背景顏色，根據需要調整 */

  transform: skew(-20deg);
  /* 設定斜斜四邊形效果，調整角度來改變斜度 */

  z-index: -1;
  /* 讓斜斜四邊形在標題文字的下方 */
}

.top {
  margin: 5%;
}

/* 使用 flexbox 讓 h2 和 h3 橫向排列 */
/* 使用 margin 來自定義元素之間的間距 */
.price {
  display: flex;
  align-items: center;
}

.price h2,
.price h3 {
  font-size: 50px;
  margin-right: 70px;
  /* 自定義間距值，根據需要調整 */
}

.price h3 {
  text-decoration: line-through;
  text-decoration-color: red;
}

.date {
  font-size: 50px;
}

/* 按鈕 */
.button {
  display: inline-block;
  padding: 15px 30px;
  font-size: 18px;
  text-align: center;
  border: none;
  background-color: #3498db;
  /* 按鈕背景顏色 */
  color: #fff;
  /* 按鈕文字顏色 */
  text-decoration: none;
  cursor: pointer;
  border-radius: 8px;
  /* 圓角半徑，可調整按鈕的外觀 */
  transition: 0.2s;
}

.button:hover {
  border: 0.1px solid #3498db;
  /* 設定邊界樣式 */
  background-color: #fff;
  color: #3498db;
}

.button:hover .icon {
  animation: bounce 0.7s infinite;
  /* animation 動畫 */
  /* infinite無限執行 */
  /* bounce 彈跳，在animation:bounce的名字和 @keygrames bounce的名字要一模一樣 */
  /* @keyframes就是動畫的部分 */
}

/* 在1秒內，從0%到50%到100%， 0%的時候Y軸在0，50%的時候Y軸在-30px，100%的時候Y軸又回到0%*/
@keyframes bounce {
  0%,
  100% {
    transform: translateY(0);
  }

  50% {
    transform: translateY(-60px);
  }
}

.icon {
  display: inline-block;
  margin-right: 10px;
  /* 調整圖標和文字之間的間距 */
  font-size: 20px;
  /* 調整圖標大小 */
}

.button span {
  vertical-align: middle;
  /* 垂直居中 */
}

.countdown {
  font-size: 70px;
}
</style>

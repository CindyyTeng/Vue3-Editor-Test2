<template>
  <div class="container">

  
<div class="row main">
  <div class="col-md-8">左
      <div class="featured-img">
          <img :src='imgBaseUrl+"/Public/Img/"+speaker.speakerImg'
              alt="Featured 1" class="featured-big">
      </div>
      
          <p>講師姓名：{{ speaker.speakerName }}</p>
          <p>擅長領域：{{speaker.fieldName}}</p>
          <p>講師描述：{{speaker.speakerDescription}}</p>
          <p>駐點分店：{{speaker.branchName}} ({{speaker.branchAddress}})</p>
          
      
     

      
          <!-- 預約系統 -->
          <p>預約時間表</p>
          <div id="datePicker">
         
              <button id="prevButton"><font-awesome-icon icon="fa-solid fa-angles-left" beat-fade /></button>
              <span id="currentDate"></span>
              <button id="nextButton"><font-awesome-icon icon="fa-solid fa-angles-right" beat-fade /></button>
          </div>
          <div id="schedule">

          </div>

           <!-- 評論 -->
           <p>心得</p>
     

  </div>


  <div class="col-md-4">
右
  </div>
</div>

</div>
</template>

<script setup>
import {onMounted, reactive, ref} from 'vue';
import axios from 'axios';
import {useRoute} from 'vue-router';
import { library } from '@fortawesome/fontawesome-svg-core'
import { fas } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
library.add(fas)





const route=useRoute();
const speakerId = route.params.id;
console.log(speakerId);
const speaker = ref({});
const imgBaseUrl = ref(import.meta.env.VITE_API_BASEADDRESS);
onMounted(()=>{
  const datePicker = document.querySelector("#datePicker");
      const prevButton = document.querySelector("#prevButton");
      const nextButton = document.querySelector("#nextButton");
      const currentDateDisplay = document.querySelector("#currentDate");
      const schedule = document.querySelector("#schedule");
      
      let newDateResult;
      let newTimeResult;

      const loadSpeaker = async(id)=>{
          //呼叫controller得到講師資訊
          axios.get(`https://localhost:7183/api/Reservation/id?id=${id}`)
                      .then(res=>{
                          console.log(res.data);
                          speaker.value= res.data;
                          console.log(speaker.value.branchId);
                          console.log(speaker.value.speakerDescription);
                      })
                      .catch(err=>{
                          console.log(err);
                      })
      }
     
      ////呼叫controller得到講師資訊
      loadSpeaker(speakerId);
      
      

      const testTime = "2023-08-17T15:00:00"

      //把資料庫傳來的時間格式化為【年/月/日】字串
      const formatDate=(dateString)=>{
              const date = new Date(dateString);
              return date.toLocaleDateString(); // 格式化為本地化的日期字串
      }
      const hasDate = formatDate(testTime);
      console.log(hasDate);

      //把資料庫傳來的時間格式化為【數字】時間字串
      const formatTime = (dateString)=>{
          const date = new Date(dateString);
          const hour = date.getHours();
          return hour;
      }
      
      const hasTime = formatTime(testTime);
      console.log(hasTime);

      //呼叫後端，從資料庫得到這位講師已經被預約的時間
      const loadReservationHistory = async(id)=>{
          axios.get(`https://localhost:7183/api/Reservation/GetReservationHistory${id}`)
              .then(res=>{
                  const result = res.data;
                  console.log(result);
                  
                  newDateResult = result.map(element=>formatDate(element.reservationStartTime));
                  console.log(newDateResult);

                  newTimeResult = result.map(element=>formatTime(element.reservationStartTime));
                  console.log(newTimeResult);
                  
                  updateCalendar();

                  // console.log(formatDate(result[0].reservationStartTime));
                 
                  
              })
              .catch(err=>{
                  console.log(err);
              })
      }
      loadReservationHistory(speakerId);


      const timeSlots = [
          "11:00  - 12:00 ",
          "12:00  - 13:00 ",
          "13:00  - 14:00 ",
          "14:00  - 15:00 ",
          "15:00  - 16:00 ",
          "16:00  - 17:00 ",
          "17:00  - 18:00 ",
          "18:00  - 19:00 ",
          "19:00  - 20:00 ",
          "20:00  - 21:00 ",
          "21:00  - 22:00 "
      ];



      

      let currentDate = new Date(); //創建一個包含right now日期和時間的 Date 物件
      console.log(currentDate);
      // 預約表的表頭顯示日期
      function showCurrentDate() {
          const startDate = new Date(currentDate);
          const endDate = new Date(startDate);
          endDate.setDate(startDate.getDate() + 6); //一週結束日期的數字設為: 開始日期的日期數字+6天

          console.log(startDate);

          console.log(endDate);

          currentDateDisplay.textContent = `${startDate.toLocaleDateString()} - ${endDate.toLocaleDateString()}`;
      }

      function createSchedule() {

          schedule.innerHTML = "";

          const table = document.createElement("table");
          table.className = "schedule-table";

          // 建立表的第一橫列 (日期)
          const headerRow = document.createElement("tr");
          headerRow.innerHTML = "<th>時間</th>";
          for (let i = 0; i < 7; i++) {
              const dateCell = document.createElement("th");
              const day = new Date(currentDate); //取得今天的月份+日期
              //迴圈第一圈：今天的日期數字，第二圈日期數字：+1
              day.setDate(currentDate.getDate() + i);
              //用getMonth( )方法產出的月份格式要+1
              const formattedDate = `${day.getMonth() + 1}/${day.getDate()}`;
              dateCell.textContent = formattedDate;

              headerRow.appendChild(dateCell);
          }

          //插入第一行日期的title
          table.appendChild(headerRow);


          // 外層迴圈產生最左側的時間直行
          for (const timeSlot of timeSlots) {
              const row = document.createElement("tr");
              const timeCell = document.createElement("td");
              timeCell.textContent = timeSlot;
              row.appendChild(timeCell);
              

              for (let i = 0; i < 7; i++) {
                  const cell = document.createElement("td");
                  cell.classList.add("schedule-cell");
                  const day = new Date(currentDate);//取得今天日期時間
                  day.setDate(currentDate.getDate() + i);
                  //使用 data- 屬性來在元素上儲存自定義的資料
                  cell.dataset.date = day.toLocaleDateString();;
                  cell.dataset.time = timeSlot.substring(0,2); //取出開始時間點

                  for (let i=0; i <newDateResult.length; i++){

                      if (cell.dataset.date == newDateResult[i] && cell.dataset.time == newTimeResult[i]){
                          cell.classList.add("unable");                        
                      }
                      else{
                          cell.addEventListener("click", handleCellClick);                                                                         
                      }
                          row.appendChild(cell);
                  }
                                  
              }

              table.appendChild(row);
          }

          schedule.appendChild(table);
      }

     

      function handleCellClick() {
          const date = this.dataset.date;
          const time = this.dataset.time;
      
          if (confirm(`您想要預約 ${date} 的 ${time} 嗎？`)) {
              this.classList.add("selected");
              this.removeEventListener("click", handleCellClick);
              const time2 = parseInt(time.substring(0,2));
              const parts = date.split('/');
              const year = parseInt(parts[0]);
              const month = parseInt(parts[1]) - 1;
              const day = parseInt(parts[2]);
              const fullDateTime = new Date(year,month,day,time2);
              // alert(fullDateTime);

              axios.post("https://localhost:7183/api/Reservation/AddReservation", {
                        fk_BookerId:4,
                        ReservationStartTime:fullDateTime,
                        fk_ReservationSpeakerId:speakerId,
                        fk_BranchId:speaker.value.branchId
                    })
                    .then(res=>{
                      console.log(res.data);
                    
                    })
                    .catch(err=>{
                      console.log(err);
                     
                    })
                  
                 
          }
      }

      

      prevButton.addEventListener("click", () => {
          currentDate.setDate(currentDate.getDate() - 7);
          updateCalendar();
      });

      nextButton.addEventListener("click", () => {
          currentDate.setDate(currentDate.getDate() + 7);
          updateCalendar();
      });

      function updateCalendar() {
          showCurrentDate();
          createSchedule();
          
      }


})
     

</script>

<style>
      .main{
          font-family: Arial, sans-serif;
          text-align: center;
          
      }
      #schedule{
          display: flex;
          justify-content: center;
      }

      .schedule-table {
          border-collapse: collapse;
          margin: 20px auto;
      }

      #schedule .schedule-table th,
      #schedule .schedule-table td {
          border: 1px solid #111;
          padding: 10px;
          cursor: pointer;
      }

      #schedule .schedule-table td.selected {
          background-color: green;
          color: white;
      }
      .unable{
          background-color: rosybrown;
          pointer-events: none;
      }

      #currentDate{
        margin: 15px;
      }

</style>


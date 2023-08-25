<template>
  <div id="app">
    <h1>課程心得</h1>
    <!-- v-for迴圈每一個評論，生成評論卡 -->
    <div class="comment-card" v-for="comment in comments" :key="comment.id">
      <div class="comment-header">
        <!-- <img :src="comment.author.avatar" alt="Author Avatar" class="avatar" /> -->
        <div class="author-info">
          <h3>{{ comment.account }}</h3>
          <p>{{ formatDateTime(comment.creationTime) }}</p>
        </div>
      </div>
      <div class="comment-content">
        <p v-if="!comment.editing && !comment.expanded && comment.content.length > 150">
          {{ comment.content.slice(0, 150) }}...
        </p>
        <p v-else-if="!comment.editing">{{ comment.content }}</p>
        <textarea v-model="comment.editContent" v-if="comment.editing && canEdit(comment)" rows="4"></textarea>
      </div>
      <div class="comment-footer">
        <!-- 評分 -->
        <div class="rating">
          <span class="star" v-for="n in comment.rating" :key="n">★</span>
        </div>
        <button @click="allowEdit(comment)" v-if="canEdit(comment)">
        {{ comment.editing ? '取消' : '編輯' }}
      </button>

      <button @click="saveEditedComment(comment)" v-if="comment.editing && canEdit(comment)">
  儲存
</button>
      </div>
    </div>



<!-- Button trigger modal -->
<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">
  新增心得
</button>

<!-- Modal -->
<div class="modal fade modal-dialog modal-dialog-centered" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="exampleModalLabel">留下您的心得...</h1>
      </div>
        <!-- 用submit.prevent防止刷新整個頁面 -->
      <form @submit.prevent="addComment">
   
        <div class="modal-body">
            <!-- 新增評論的表單 -->
                <div class="comment-form">              
                   <!-- <p>{{comments.account}}</p> -->
                    <textarea v-model="newComment.content" placeholder="課程心得" required></textarea>

                    <star-rating @update:rating="setRating"></star-rating>              
                </div>
        </div>

        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">取消</button>
          <button type="submit" data-bs-dismiss="modal" class="btn btn-primary">送出</button>
        </div>
    </form>
    </div>
  </div>
</div>

    
  </div>
</template>

<script setup>
import { ref } from 'vue';
import axios from 'axios';
import StarRating from 'vue-star-rating'

const rating = ref(5); // 這裡初始化評分為 5
const fk_speakerId = 1;
const fk_memberId =5;
const comments = ref([]);
const setRating = (newRating) => {
  rating.value = newRating;
};

//從後端得到該講者的全部心得評論紀錄
const getAllComment = (fk_speakerId) =>{
  axios.get(`https://localhost:7183/api/Reservation/GetAllComment${fk_speakerId}`)
      .then(res=>{
        console.log(res.data);
        comments.value = res.data;
      })
      .catch(err=>{
        console.log(err);
      })
}

getAllComment(fk_speakerId)

const newComment = ref({
  fk_memberId: 5,
  account: '',
  content: '',
  creationTime:"",
  rating: ""
});

const allowEdit = (comment) => {
  if (!comment.editing) {
    //如果評論不處於編輯模式(editing)，那評論的編輯內容就會是資料庫抓出來的content
    comment.editContent = comment.content;
  }
  //如果已經處於編輯模式，那按下去就會無法編輯(取消編輯)
  comment.editing = !comment.editing;
};


const saveEditedComment = (comment) => {
  if (comment.editContent.trim()) {
    //檢查編輯內容是否為空
    comment.content = comment.editContent;
    //關閉編輯
    comment.editing = false;
  }
};

//新增評論
const addComment = () => {
    //確認使用者姓名和評論內容都有填寫
  if (newComment.value.fk_memberId && newComment.value.content) {
      const newCommentData = {
        fk_memberId: newComment.value.fk_memberId,
        content: newComment.value.content,
        fk_speakerId,
        rating: rating.value
      };
      
      try {
        comments.value.push({
        //用內建push方法向末端加一個評論
      id: "",     
      account: newComment.value.account,
      content: newComment.value.content,
      rating: rating.value,
      creationTime: Date.now(),
      expanded: false
    });

      // 發送 POST 請求到後端 API，並將新評論傳遞過去
      const response =  axios.post('https://localhost:7183/api/Reservation/AddComment', newCommentData);
                         

    //使用者新增完評論之後，要把值清空，讓他可以再次新增新的評論
      // newComment.value.account = '';
      newComment.value.content = '';
     
    } catch (error){
      console.error('addComment方法的錯誤：', error);
    }
  }
};


//檢查是否為本人要編輯
const canEdit = (comment) => {
  return comment.fk_memberId == fk_memberId; // 使用者 ID 與該筆評論的作者 ID 相符才允許編輯
};


const formatDateTime = (dateString) => {
    const date = new Date(dateString);
    
    const year = date.getFullYear();
    const month = date.getMonth() + 1;
    const day = date.getDate();

    const hours = date.getHours();
    const minutes = date.getMinutes();

    // 組裝成我要的格式
    const formattedDateTime = `${year}/${month}/${day} ${hours}:${minutes}`;
    return formattedDateTime;
}
</script>

<style scoped>
#app {
  font-family: Arial, sans-serif;
  text-align: center;
  padding: 20px;
}

.comment-card {
  border: 1px solid #ddd;
  border-radius: 8px;
  padding: 15px;
  margin-bottom: 20px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.comment-header {
  display: flex;
  align-items: center;
  margin-bottom: 10px;
}

.avatar {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  margin-right: 10px;
}

.author-info {
  display: flex;
  flex-direction: column;
  justify-content: center;
}

.comment-content {
  margin-bottom: 10px;
}

.comment-footer {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.rating {
  font-size: 20px;
}

.star {
  color: #FFD700;
}

button {
  background-color: #007BFF;
  color: rgb(126, 32, 32);
  border: none;
  padding: 5px 10px;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s;
}

button:hover {
  background-color: #0056b3;
}

.comment-card textarea {
  width: 100%;
  padding: 5px;
  border: 1px solid #ddd;
  border-radius: 5px;
}





</style>

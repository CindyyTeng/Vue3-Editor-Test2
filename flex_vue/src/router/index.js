import { createRouter, createWebHistory } from "vue-router";
import { useGetApiDataStore } from "../stores/useGetApiDataStore";
import { useProductRoute } from "../stores/useProductRoute";
import { storeToRefs } from "pinia";
import User from "../views/user/User.vue";
import Favorites from "../views/user/Favorites.vue";
import Login from "../views/user/Login.vue";
import ActivityInfo from "../views/activity/ActivityInfo.vue";
import ActivitySignUp from "../views/activity/ActivitySignUp.vue";
import ActivityIndex from "../views/activity/ActivityIndex.vue";
import ReservationIndex from "../views/reservation/ReservationIndex.vue";
import SpeakerInfo from "../views/reservation/SpeakerInfo.vue";
import Comment from "../views/reservation/Comment.vue";
import Community from "../views/activity/Community.vue";
import Community222 from '../views/activity/Community222.vue';
const webTitle = "FLEX - ";

// 路由設定
const routes = [
  {
    //http://loaclhost/
    path: "/",
    component: () => import("@/views/home/Home.vue"),
    meta: { title: `${webTitle}首頁` },
  },
  {
    //http://loaclhost/cart
    path: "/cart",
    component: () => import("@/views/home/Cart.vue"),
    meta: { title: `${webTitle}購物車` },
  },
  {
    //http://loaclhost/buy
    path: "/buy",
    component: () => import("@/views/home/Buy.vue"),
    meta: { title: `${webTitle}結帳` },
  },
  {
    //http://loaclhost/User
    path: "/user",
    component: User,
    meta: { title: `${webTitle}個人資料`, require: true },
  },
  {
    //http://loaclhost/Favorites
    path: "/favorites",
    component: Favorites,
    meta: { title: `${webTitle}收藏清單`, require: true },
  },
  {
    //http://loaclhost/Login
    path: "/login",
    component: Login,
  },
  {
    //http://loaclhost/orders
    path: "/orders",
    component: () => import("../views/orders/orderindex.vue"),
    meta: { title: `${webTitle}訂單` },
  },
  {
    //http://loaclhost/activityInfo
    path: "/activityInfo/:id",
    component: ActivityInfo,
    meta: { title: `${webTitle}活動` },
  },
  {
    //http://loaclhost/activitySignUp
    path: "/activitySignUp",
    component: ActivitySignUp,
    meta: { title: `${webTitle}活動報名` },
  },
  {
    path: "/activityIndex",
    component: ActivityIndex,
    meta: { title: `${webTitle}活動首頁` },
  },
  {
    path: "/reservationIndex",
    component: ReservationIndex,
    meta: { title: `${webTitle}預約諮詢首頁` },
  },
  {
    path: "/speakerInfo/:id",
    component: SpeakerInfo,
    meta: { title: `${webTitle}講師資訊` },
  },
  {
    path: "/comment",
    component: Comment,
    meta: { title: `${webTitle}評論` },
  },
  {
    path: "/community",
    component: Community,
    meta: { title: `${webTitle}討論區` },
  },
  {
    path: "/community222",
    component: Community222,
    meta: { title: `${webTitle}討論區` },
  },
  {
    //http://loaclhost/Login
    path: "/login",
    component: Login,
  },
  {
    //http://loaclhost/orders
    path: "/orders",
    component: () => import("../views/orders/orderindex.vue"),
    meta: { title: `${webTitle}訂單` },
  },
  {
    //http://loaclhost/activityInfo
    path: "/activityInfo",
    component: ActivityInfo,
    meta: { title: `${webTitle}活動` },
  },
  {
    //http://loaclhost/activitySignUp
    path: "/activitySignUp",
    component: ActivitySignUp,
    meta: { title: `${webTitle}活動報名` },
  },
  {
    //http://loaclhost/Login
    path: "/login",
    component: Login,
  },
  {
    //http://loaclhost/Men
    path: "/men",
    component: () => import("../views/product/ProductMenLayout.vue"),
    children: [
      {
        path: "",
        component: () => import("../views/product/ProductList.vue"),
        meta: { title: `${webTitle}男裝` },
      },
      {
        path: ":categoryName",
        component: () => import("../views/product/ProductList.vue"),
        meta: {},
        beforeEnter(to, from, next) {
          document.title = `${webTitle}男裝/${to.params.categoryName}`;
          next();
        },
      },
      {
        path: ":categoryName/:subCategoryName",
        component: () => import("../views/product/ProductList.vue"),
        meta: {},
        beforeEnter(to, from, next) {
          document.title = `${webTitle}男裝/${to.params.categoryName}/${to.params.subCategoryName}`;
          next();
        },
      },
      {
        // 當 /ProductMenLayout/:id/posts 匹配成功
        // Detial.vue 將被渲染到 ProductMenLayout 的 <router-view> 内部，替換card.vue
        path: "detail/:productId",
        component: () => import("../views/product/ProductDetail.vue"),
        meta: {},
        beforeEnter(to, from, next) {
          // const productStore = useProductRoute();
          // const productName = productStore.productName;
          document.title = `${webTitle}男裝`; //productName ${to.params.productId}
          next();
        },
      },
    ],
  },
  {
    //http://loaclhost/Women
    path: "/women",
    component: () => import("../views/product/ProductWomenLayout.vue"),
    children: [
      {
        path: "",
        component: () => import("../views/product/ProductList.vue"),
        meta: { title: `${webTitle}女裝` },
      },
      {
        path: ":categoryName",
        component: () => import("../views/product/ProductList.vue"),
        meta: {},
        beforeEnter(to, from, next) {
          document.title = `${webTitle}女裝/${to.params.categoryName}`;
          next();
        },
      },
      {
        path: ":categoryName/:subCategoryName",
        component: () => import("../views/product/ProductList.vue"),
        meta: {},
        beforeEnter(to, from, next) {
          document.title = `${webTitle}女裝/${to.params.categoryName}/${to.params.subCategoryName}`;
          next();
        },
      },
      {
        // 當 /ProductMenLayout/:id/posts 匹配成功
        // Detial.vue 將被渲染到 ProductMenLayout 的 <router-view> 内部，替換card.vue
        path: "detail/:productId",
        component: () => import("../views/product/ProductDetail.vue"),
        meta: {},
        beforeEnter(to, from, next) {
          document.title = `${webTitle}女裝`; //${to.params.productId}
          next();
        },
      },
    ],
  },
  {
    //http://loaclhost/Kid
    path: "/kid",
    component: () => import("../views/product/ProductKidLayout.vue"),
    children: [
      {
        path: "",
        component: () => import("../views/product/ProductList.vue"),
        meta: { title: `${webTitle}童裝` },
      },
      {
        path: ":categoryName",
        component: () => import("../views/product/ProductList.vue"),
        meta: {},
        beforeEnter(to, from, next) {
          document.title = `${webTitle}童裝/${to.params.categoryName}`;
          next();
        },
      },
      {
        path: ":categoryName/:subCategoryName",
        component: () => import("../views/product/ProductList.vue"),
        meta: {},
        beforeEnter(to, from, next) {
          document.title = `${webTitle}童裝/${to.params.categoryName}/${to.params.subCategoryName}`;
          next();
        },
      },
      {
        // 當 /ProductMenLayout/:id/posts 匹配成功
        // Detial.vue 將被渲染到 ProductMenLayout 的 <router-view> 内部，替換card.vue
        path: "detail/:productId",
        component: () => import("../views/product/ProductDetail.vue"),
        meta: {},
        beforeEnter(to, from, next) {
          document.title = `${webTitle}童裝`; //${to.params.productId}
          next();
        },
      },
    ],
  },
  {
    //http://loaclhost/CustomeShoes
    path: "/CustomeShoes",
    component: () => import("../views/CustomeShoes/CustomeShoesLayout.vue"),
    meta: { title: `${webTitle}客製化商品` },
    children: [
      {
        path: "",
        component: () => import("../views/CustomeShoes/CustomeShoesAll.vue"),
      },
      {
        path: ":shoescategoryName",
        component: () => import("../views/CustomeShoes/CustomeShoesAll.vue"),
      },
      {
        //http://loaclhost/Login
        path: "/login",
        component: Login,
      },
    ],
  },
  {
    //http://loaclhost/CustomeShoes/detail/shoesProductId
    path: "/CustomeShoes/detail/:shoesProductId",
    component: () => import("../views/CustomeShoes/ShoesDetail.vue"),
    meta: {},
    beforeEnter(to, from, next) {
      document.title = `${webTitle}商品詳細資訊${to.params.shoesProductId}`;
      next();
    },
  },
  {
    //http://loaclhost/CustomeShoes/detail/shoesProductId
    path: "/CustomeShoes/detail/Customization/:shoesProductId",
    component: () => import("../views/CustomeShoes/CustomerPage.vue"),
    meta: {},
    beforeEnter(to, from, next) {
      document.title = `${webTitle}商品客製化頁面${to.params.shoesProductId}`;
      next();
    },
  },

  {
    //http://loaclhost/CustomeShoes/Contact
    path: "/CustomeShoes/Contact",
    component: () => import("../views/CustomeShoes/Contact.vue"),
    meta: { title: `${webTitle}合作洽詢` },
    children: [
      {
        path: ":shoescategoryName",
        component: () => import("../views/CustomeShoes/CustomeShoesAll.vue"),
      },
      {
        //http://loaclhost/Login
        path: "/login",
        component: Login,
      },
    ],
  },

  {
    //http://loaclhost/CustomeShoes/Contact
    path: "/CustomeShoes/FAQ",
    component: () => import("../views/CustomeShoes/FAQ.vue"),
    meta: { title: `${webTitle}常見問題` },
    children: [
      {
        //http://loaclhost/Login
        path: "/login",
        component: Login,
      },
    ],
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes: routes,
  //  變數名稱跟屬性名稱一樣可直接省略成 routes
});

router.beforeEach((to, from, next) => {
  if (to.meta.title) {
    document.title = to.meta.title;
  }
  next();
});

router.beforeEach((to, from, next) => {
  const getApiData = useGetApiDataStore();
  const { loginSuccess } = getApiData;

  //檢查是否需要驗證，如果需要，則檢查是否已登入
  if (to.meta.require && !loginSuccess) {
    console.log("nologin");
    next({ path: "/login" });
  } else {
    //console.log('login');
    next();
  }
});

export default router;

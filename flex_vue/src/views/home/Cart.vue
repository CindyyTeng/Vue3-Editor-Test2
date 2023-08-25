<template>
  <navBar></navBar>
  <main>
    <div class="container">
      <div class="row gx-4">
        <!-- 購物車左側 -->
        <div class="left col-12 col-lg-8">
          <ul class="cart">
            <li v-for="cartItem in cartItems" :key="cartItem.cartItemId" class="cart-item">
              <a class="pd-img-wrapper" href="javascript:;">
                <img :src="imgBaseUrl + 'Public/Img/' + cartItem.product.imgPath" :title="cartItem.product.productName" />
              </a>
              <div class="item-info-wrapper me-auto">
                <a href="javascript:;" class="fw-bold">{{ cartItem.product.productName }}-{{
                  cartItem.product.salesCategoryNameStr }}-{{ cartItem.product.color }}</a>
                <div>尺寸 <a href="javascript:;"><span>{{ cartItem.product.size }}</span><i
                      class="bi bi-chevron-down"></i></a></div>
                <ul class="d-flex px-0">
                  <li class="me-2" v-for="matchDiscount in cartItem.product.matchDiscounts"
                    :key="matchDiscount.discountId">
                    <a href="javascript:;">{{ matchDiscount.discountName }}</a>
                  </li>
                </ul>
              </div>
              <i class="bi bi-trash3"></i>
              <div>
                <div class="d-flex">
                  <button @click="decrementCartItem(cartItem)"><i class="bi bi-dash-lg"></i></button>
                  <div>{{ cartItem.qty }}</div>
                  <button @click="incrementCartItem(cartItem)"><i class="bi bi-plus-lg"></i></button>
                </div>
              </div>
              <div class="px-4">
                <div v-if="cartItem.unitSubTotal !== null">{{ cartItem.unitSubTotal }}</div>
                <div>{{ cartItem.subTotal }}</div>
              </div>
            </li>
          </ul>
        </div>
        <!-- 購物車右側 -->
        <div class="right col-12 col-lg-4">
          <h3>摘要</h3>
          <div class="d-flex justify-content-between">
            <div>折扣後小計</div>
            <div>{{ originalTotalAmount }}</div>
          </div>
          <div class="d-flex justify-content-between">
            <div>折扣金額</div>
            <div>{{ discountCount }}</div>
          </div>
          <div class="d-flex justify-content-between">
            <div>運費</div>
            <div>{{ deliveryFee }}</div>
          </div>
          <div class="d-flex justify-content-between border border-start-0 border-end-0 py-2 ">
            <div>總計</div>
            <div>{{ subTotal }}</div>
          </div>
          <button class="btn btn-dark w-100 my-3 py-3 rounded-5">結帳</button>
        </div>
      </div>
    </div>
  </main>
  <homeFooter></homeFooter>
</template>
    
<script setup lang="ts">
import NavBar from '@/components/home/navBar.vue';
import HomeFooter from '@/components/home/footer.vue';
import axios from "axios";
import { ref, onMounted, computed } from "vue";
import { CartItem, ShoppingCartItem } from '@/types/type';

// 用vite獲得環境變數
const baseAddress: string = import.meta.env.VITE_API_BASEADDRESS;
const imgBaseUrl = ref(baseAddress);
const cartItems = ref<CartItem[]>([]);
const originalTotalAmount = ref<number>();
const subTotal = ref<number>();
const deliveryFee = ref<number>();


// 載入購物車
const loadCartItems = async () => {
  let url: string = `${baseAddress}api/Cart`;
  await axios
    .post<CartItem[]>(url)
    .then((response) => {
      cartItems.value = response.data;
      loadTotal();
      // console.log(cartItems.value);
    })
    .catch((error) => {
      alert(error);
    });
};

// 載入折扣後的結帳金額
const loadTotal = async () => {
  let url: string = `${baseAddress}api/Cart/Checkout`;
  await axios
    .post<number>(url)
    .then((response) => {
      originalTotalAmount.value = response.data.originalTotalAmount;
      subTotal.value = response.data.totalPrice;
      deliveryFee.value = response.data.deliveryFee;
      // console.log(subTotal.value);
    })
    .catch((error) => {
      alert(error);
    });
}

const discountCount = computed(() => {
  return subTotal.value - deliveryFee.value - originalTotalAmount.value;
});

onMounted(() => {
  loadCartItems();
});

// 定義callback函數類型
type Callback = () => void;

function processCallbacks(...callbacks: Callback[]) {
  callbacks.forEach(callback => callback());
}

const incrementCartItem = async (cartItem: CartItem) => {
  const shoppingCart = new ShoppingCartItem(cartItem)
  await shoppingCart.addOneItem(() => {
    processCallbacks(loadCartItems);
  });
};

const decrementCartItem = async (cartItem: CartItem) => {
  const shoppingCart = new ShoppingCartItem(cartItem);
  await shoppingCart.removeOneItem(() => {
    processCallbacks(loadCartItems);
  });
};

const getCartItemQty = (cartItem: CartItem) => {
  const shoppingCart = new ShoppingCartItem(cartItem);
  return shoppingCart.getCartItemQty();
};

</script>
    
<style scoped lang="scss">
main {
  margin-top: 40px;
  padding-bottom: 120px;

  .row {
    &>.left {
      &>.cart {
        &>.cart-item {
          display: flex;

          .pd-img-wrapper {
            width: 150px;
            height: 150px;
            overflow: hidden;
            display: flex;
            align-items: center;
            justify-content: center;

            &>img {
              width: 100%;
              height: auto;
              object-fit: cover;
            }
          }
        }
      }
    }

    &>.right {
      &>div {}
    }
  }
}
</style>
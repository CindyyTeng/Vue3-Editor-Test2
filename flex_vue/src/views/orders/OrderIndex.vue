<template>
  <OrdernavBar></OrdernavBar>
  <div id="searchorderout" class="container">
    <div id="srarchdate" class="col-3">
      <input type="text" v-model="begintime" @searchInputbegintime="inputbegintime" class="form-control datePicker"
        placeholder="輸入開始日期" />
      ~
      <input type="text" v-model="endtime" @searchInputendtime="inputendtime" class="form-control datePicker"
        placeholder="輸入結束日期" />
    </div>
    <div id="searchorder" class="col-3">
      <input type="text" v-model="keyword" @searchInput="inputhandler" class="form-control" placeholder="輸入名稱" />
      <button class="button" @click="keywordSearch">搜尋</button>
    </div>
  </div>
  <div id="cate" class="container">
    <button @click="
      setTypeValue('1');
    setostatusValue('');">
      商品
    </button>

    <button @click="
      setTypeValue('4');
    setostatusValue('');
    ">
      客製化
    </button>

    <button @click="
      setTypeValue('2');
    setostatusValue('1');
    ">
      活動
    </button>

    <button @click="
      setTypeValue('3');
    setostatusValue('1');
    ">
      課程
    </button>
  </div>
  <div class="container" v-if="Type == 1">
    <table class="table" style="width: 100%">
      <thead>
        <tr>
          <th></th>
          <th>訂單日期</th>
          <th>訂單編號</th>
          <th>總數量</th>
          <th>總金額</th>
          <th>付款方式</th>
          <th>發票編號</th>
          <th>訂單狀態</th>
        </tr>
      </thead>
      <tbody>
        <template v-for="item in GetOrders" :key="item.id">
          <tr>
            <td><button @click="toggleDetails(item.id)">收合/展開</button></td>
            <td>{{ formatOrderTime(item.ordertime) }}</td>
            <td>{{ item.id }}</td>
            <td>{{ item.total_quantity }}</td>
            <td>{{ item.total_price }}</td>
            <td>{{ item.pay_method }}</td>
            <td>{{ item.receipt }}</td>
            <td>{{ item.order_status }}</td>
          </tr>
          <tr>
            <td colspan="8">
              <table class="table" style="text-align: left; border: 2px solid black"
                v-show="expandedItems.includes(item.id)">
                <tr>
                  <td colspan="4" style="text-align: right">
                    <div style="text-align: right">
                      <button v-if="item.order_status_Id !== 7 &&
                        item.order_status_Id !== 9 &&
                        item.order_status_Id !== 8 &&
                        item.close !== true
                        " @click="setcancelProductIdValue(item.id)" class="btn btn-primary" style="margin-right: 30px">
                        申請取消
                      </button>
                      <button v-if="item.order_status_Id !== 7 &&
                        item.order_status_Id !== 9 &&
                        item.order_status_Id !== 8 &&
                        item.close !== true
                        " @click="setreturnIdValue(item.id)" class="btn btn-primary" :data-bs-toggle="item.order_status_Id === 6 ? 'modal' : null
    " :data-bs-target="item.order_status_Id === 6 ? '#exampleModal' : null
    ">
                        申請退貨
                      </button>
                      <button v-if="item.order_status_Id == 9" type="button" class="btn btn-secondary"
                        @click="setcancelreturnIdValue2(item.id)">
                        取消退貨
                      </button>
                    </div>
                  </td>
                </tr>
                <tr v-for="orderItem in item.orderItems" :key="orderItem.id">
                  <td>商品名稱：{{ orderItem.product_name }}</td>
                  <td>數量：{{ orderItem.quantity }}</td>
                  <td>價格：{{ orderItem.per_price }}</td>
                  <td>規格：{{ orderItem.items_description }}</td>
                </tr>
                <hr />
                <tr style="justify-content: center">
                  <td colspan="8">
                    <table class="" style="text-align: left" v-show="expandedItems.includes(item.id)">
                      <tr>
                        <td style="
                            padding: 0 200px 80px 20px;
                            border-right: 2px solid black;
                            width: 500px;
                          ">
                          <h3>收件資訊</h3>
                          <br />
                          <div>地址：{{ item.recipient_address }}</div>
                          <br />
                          <div>姓名:{{ item.receiver }}</div>
                          <br />
                          <div>電話:{{ item.cellphone }}</div>
                        </td>
                        <td id="orderItemDetail" style="padding-left: 30px; width: 800px">
                          <h6>運費</h6>
                          <div>{{ item.freight }}</div>
                          <h6>運費折扣</h6>
                          <h6>已使用優惠券</h6>
                          <div>{{ item.coupon_name }}</div>
                          <h6>優惠券折扣</h6>
                          <div>{{ item.coupon_discount }}</div>
                          <h6>訂單金額</h6>
                          <div>{{ item.total_price }}</div>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
        </template>
      </tbody>
    </table>
  </div>
  <div class="container" v-if="Type == 4">
    <table class="table" style="width: 100%">
      <thead>
        <tr>
          <th></th>
          <th>訂單日期</th>
          <th>訂單編號</th>
          <th>總數量</th>
          <th>總金額</th>
          <th>付款方式</th>
          <th>發票編號</th>
          <th>訂單狀態</th>
        </tr>
      </thead>
      <tbody>
        <template v-for="item in GetOrders" :key="item.id">
          <tr>
            <td><button @click="toggleDetails(item.id)">收合/展開</button></td>
            <td>{{ formatOrderTime(item.ordertime) }}</td>
            <td>{{ item.id }}</td>
            <td>{{ item.total_quantity }}</td>
            <td>{{ item.total_price }}</td>
            <td>{{ item.pay_method }}</td>
            <td>{{ item.receipt }}</td>
            <td>{{ item.order_status }}</td>
          </tr>
          <tr>
            <td colspan="8">
              <table class="table" style="text-align: left; border: 2px solid black"
                v-show="expandedItems.includes(item.id)">
                <tr>
                  <td colspan="4" style="text-align: right">
                    <div style="text-align: right">
                      <button v-if="item.order_status_Id !== 7 &&
                        item.order_status_Id !== 9 &&
                        item.order_status_Id !== 8 &&
                        item.order_status_Id !== 10 &&
                        item.close !== true
                        " @click="setcancelIdValue(item.id)" class="btn btn-primary" style="margin-right: 30px">
                        申請取消
                      </button>
                      <button v-if="item.order_status_Id !== 7 &&
                        item.order_status_Id !== 9 &&
                        item.order_status_Id !== 8 &&
                        item.order_status_Id !== 10 &&
                        item.close !== true
                        " @click="setchangeIdValue(item.id)" class="btn btn-primary">
                        申請換貨
                      </button>
                      <button v-if="item.order_status_Id == 10" type="button" class="btn btn-secondary"
                        @click="setcancelreturnIdValue3(item.id)">
                        取消換貨
                      </button>
                    </div>
                  </td>
                </tr>
                <tr v-for="orderItem in item.orderItems" :key="orderItem.id">
                  <td>商品名稱：{{ orderItem.product_name }}</td>
                  <td>數量：{{ orderItem.quantity }}</td>
                  <td>價格：{{ orderItem.per_price }}</td>
                  <td>規格：{{ orderItem.items_description }}</td>
                </tr>
                <hr />
                <tr style="justify-content: center">
                  <td colspan="8">
                    <table class="" style="text-align: left" v-show="expandedItems.includes(item.id)">
                      <tr>
                        <td style="
                            padding: 0 200px 80px 20px;
                            border-right: 2px solid black;
                            width: 500px;
                          ">
                          <h3>收件資訊</h3>
                          <div>地址：{{ item.recipient_address }}</div>
                          <br />
                          <div>姓名:{{ item.receiver }}</div>
                          <br />
                          <div>電話{{ item.cellphone }}</div>
                        </td>
                        <td id="orderItemDetail" style="padding-left: 30px; width: 800px">
                          <h6>運費</h6>
                          <div>{{ item.freight }}</div>
                          <h6>運費折扣</h6>
                          <h6>已使用優惠券</h6>
                          <div>{{ item.coupon_name }}</div>
                          <h6>優惠券折扣</h6>
                          <div>{{ item.coupon_discount }}</div>
                          <h6>訂單金額</h6>
                          <div>{{ item.total_price }}</div>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
        </template>
      </tbody>
    </table>
  </div>
  <div class="container" v-if="Type == 2">
    <table class="table" style="width: 100%">
      <div id="cateorder" class="container">
        <button @click="setostatusValue('1')">即將開始</button>

        <button @click="setostatusValue('7')">已取消</button>

        <button @click="setostatusValue('6')">已結束</button>
      </div>
      <thead>
        <h3>我報名的活動</h3>
      </thead>
      <tbody>
        <template v-for="item in GetOrders" :key="item.id">
          <tr v-for="orderItem in item.orderItems" :key="orderItem.id">
            <td style="text-align: left">
              <div>活動名稱：{{ orderItem.product_name }}</div>
              <div>活動時間:{{ formatOrderTime(item.close_time) }}</div>
              <div>活動地點：{{ item.recipient_address }}</div>
              <div>活動講師:{{ item.receiver }}</div>
              <div>費用：{{ orderItem.per_price }}</div>
            </td>
            <td><button @click="toggleDetails(item.id)">收合/展開</button></td>
          </tr>
          <tr>
            <td colspan="8">
              <table class="table" style="text-align: left; border: 2px solid black"
                v-show="expandedItems.includes(item.id)">
                <tr style="justify-content: center">
                  <td colspan="8">
                    <table class="" style="text-align: left" v-show="expandedItems.includes(item.id)">
                      <tr>
                        <td style="padding: 10px 200px 20px 20px; width: 500px">
                          <h3>訂單詳請</h3>
                          <div>
                            購買時間：{{ formatOrderTime(item.ordertime) }}
                          </div>
                          <div>訂單編號:{{ item.id }}</div>
                          <div>發票編號{{ item.receipt }}</div>
                          <button v-if="item.order_status_Id !== 7 &&
                            item.order_status_Id !== 6
                            " @click="setcancelIdValue(item.id)" class="btn btn-success">
                            申請取消
                          </button>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
        </template>
      </tbody>
    </table>
  </div>
  <div class="container" v-if="Type == 3">
    <table class="table" style="width: 100%">
      <div id="cateorder" class="container">
        <button @click="setostatusValue('1')">即將開始</button>

        <button @click="setostatusValue('7')">已取消</button>

        <button @click="setostatusValue('6')">已結束</button>
      </div>
      <thead>
        <h3>我報名的課程</h3>
      </thead>
      <tbody>
        <template v-for="item in GetOrders" :key="item.id">
          <tr v-for="orderItem in item.orderItems" :key="orderItem.id">
            <td style="text-align: left">
              <div>課程名稱：{{ orderItem.product_name }}</div>
              <div>課程時間:{{ formatOrderTime(item.close_time) }}</div>
              <div>課程地點：{{ item.recipient_address }}</div>
              <div>課程講師:{{ item.receiver }}</div>
            </td>
            <td><button @click="toggleDetails(item.id)">收合/展開</button></td>
          </tr>
          <tr>
            <td colspan="8">
              <table class="table" style="text-align: left; border: 2px solid black"
                v-show="expandedItems.includes(item.id)">
                <tr style="justify-content: center">
                  <td colspan="8">
                    <table class="" style="text-align: left" v-show="expandedItems.includes(item.id)">
                      <tr>
                        <td style="padding: 10px 200px 20px 20px; width: 500px">
                          <h3>訂單詳請</h3>
                          <div>
                            購買時間：{{ formatOrderTime(item.ordertime) }}
                          </div>
                          <div>訂單編號:{{ item.id }}</div>
                          <button v-if="item.order_status_Id !== 7 &&
                            item.order_status_Id !== 6
                            " @click="setcancelcourseIdValue(item.id)" class="btn btn-success">
                            申請取消
                          </button>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
        </template>
      </tbody>
    </table>
  </div>

  <template v-for="item in GetOrders" :key="item.id">
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">輸入退款資訊</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <div class="form-group">
              <label class="form-label">退款帳號:</label>
              <input type="text" class="form-control" v-model="returnaccount" maxlength="16" />
            </div>
            <div class="form-group">
              <label class="form-label">退貨原因:</label>
              <select class="form-control" v-model="returnreason">
                <option v-for="reason in reReason" :key="reason.id" :value="reason.id">
                  {{ reason.退貨理由 }}
                </option>
              </select>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" @click="setcancelreturnIdValue()">
              關閉
            </button>
            <button type="button" class="btn btn-primary" data-bs-dismiss="modal" @click="setreturndetalValue()">
              確定
            </button>
          </div>
        </div>
      </div>
    </div>
  </template>
  <!-- <div class="modal fade" id="insertModal" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" role="dialog"
    aria-labelledby="modalTitleId" aria-hidden="true">
    <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-sm" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="modalTitleId">輸入退款資訊</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <div class="form-group">
            <label class="form-label">退款帳號:</label>
            <input type="text" class="form-control" v-model="returnaccount" />
          </div>
          <div class="form-group">
            <label class="form-label">退貨原因:</label>
            <input type="text" class="form-control" v-model="returnreason" />
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-info" data-bs-dismiss="modal">關閉</button>
          <button type="button" class="btn btn-primary" @click="insert">確定</button>
        </div>
      </div>
    </div>
  </div> -->
</template>
<script setup>
import OrdernavBar from "@/components/Order/OrdernavBar.vue";
import { ref, onMounted } from "vue";
import axios from "axios";
import flatpickr from "flatpickr";
import "flatpickr/dist/flatpickr.min.css";

const baseAddress = import.meta.env.VITE_API_BASEADDRESS;
const GetOrders = ref([]);
const Type = ref("");
const keyword = ref("");
const begintime = ref("");
const endtime = ref("");
const expandedItems = ref([]);
const cancelId = ref("");
const retrunId = ref("");
const ostatus = ref("");
const returnaccount = ref("");
const returnreason = ref("");
const returnOrderId = ref("");
const request = ref([]);
const reReason = ref([]);

const loadGetOrders = async () => {
  const begintimeValue = begintime.value;
  const endtimeValue = endtime.value;
  if (begintimeValue > endtimeValue) {
    alert("時間錯誤：開始時間不能大於結束時間");
    return;
  }
  await axios
    .get(
      `${baseAddress}api/Orders/GetOrders?begintime=${begintime.value}&endtime=${endtime.value}&keyword=${keyword.value}&typeId=${Type.value}&ostatusId=${ostatus.value}`
    )
    .then((response) => {
      console.log(response.data);
      GetOrders.value = response.data;
    })
    .catch((error) => {
      alert(error);
    });
};
const CancelOrders = async () => {
  await axios
    .put(`https://localhost:7183/api/Orders/cancel?id=${cancelId.value}`)
    .then((response) => {
      //console.log(response.data);
      alert(response.data);
      loadGetOrders();
    })
    .catch((error) => {
      alert(error);
    });
};
const Cancelcourse = async () => {
  await axios
    .put(`https://localhost:7183/api/Orders/cancelcourse?id=${cancelId.value}`)
    .then((response) => {
      //console.log(response.data);
      alert(response.data);
      loadGetOrders();
    })
    .catch((error) => {
      alert(error);
    });
};
const CancelProductOrders = async () => {
  await axios
    .put(`https://localhost:7183/api/Orders/cancelProduct?id=${cancelId.value}`)
    .then((response) => {
      //console.log(response.data);
      alert(response.data);
      loadGetOrders();
    })
    .catch((error) => {
      alert(error);
    });
};
const setcancelIdValue = (paramValue) => {
  cancelId.value = paramValue;
  CancelOrders();
};
const setcancelcourseIdValue = (paramValue) => {
  cancelId.value = paramValue;
  Cancelcourse();
};
const setcancelProductIdValue = (paramValue) => {
  cancelId.value = paramValue;
  CancelProductOrders();
};
const ReturnOrders = async () => {
  await axios
    .put(`https://localhost:7183/api/Orders/return?orderid=${retrunId.value}`)
    .then((response) => {
      //console.log(response.data);
      alert(response.data);
      loadGetOrders();
    })
    .catch((error) => {
      alert(error);
    });
};
const setreturnIdValue = (paramValue) => {
  retrunId.value = paramValue;
  ReturnOrders();
};
const CancelReturnOrders = async () => {
  await axios
    .put(
      `https://localhost:7183/api/Orders/cancelreturn?orderid=${retrunId.value}`
    )
    .then((response) => {
      //console.log(response.data);
      alert(response.data);
      loadGetOrders();
    })
    .catch((error) => {
      alert(error);
    });
};
const CancelChangeOrders = async () => {
  await axios
    .put(
      `https://localhost:7183/api/Orders/cancelChange?orderid=${retrunId.value}`
    )
    .then((response) => {
      //console.log(response.data);
      alert(response.data);
      loadGetOrders();
    })
    .catch((error) => {
      alert(error);
    });
};
const SetOrdersclose = async () => {
  await axios
    .put(
      `https://localhost:7183/api/Orders/setclose?orderid=${retrunId.value}`
    )
    .then((response) => {
      //console.log(response.data);
      loadGetOrders();
    })
    .catch((error) => {
      alert(error);
    });
};
const setcancelreturnIdValue = async (paramValue) => {
  returnOrderId.value = paramValue;
  await CancelReturnAndCloseOrders();
};
const setcancelreturnIdValue2 = async (paramValue) => {
  retrunId.value = paramValue;
  await CancelReturnAndCloseOrders();
};
const setcancelreturnIdValue3 = async (paramValue) => {
  retrunId.value = paramValue;
  await CancelChangeOrders();
  await SetOrdersclose();
};
const CancelReturnAndCloseOrders = async () => {
  await CancelReturnOrders();
  await SetOrdersclose();
};
const Returndetail = async () => {
  const requestData = {
    退貨轉帳帳號: returnaccount.value,
    退貨理由: returnreason.value,
  };
  await axios
    .post(
      `https://localhost:7183/api/Orders/NewReturn?orderid=${retrunId.value}`,
      requestData
    )
    .then((response) => {
      alert("退款資訊已提交");
      loadGetOrders();
      returnaccount.value = "";
      returnreason.value = "";
    })
    .catch((error) => {
      alert(error);
    });
};
const setreturndetalValue = (paramValue) => {
  returnOrderId.value = paramValue;
  Returndetail();
};
const fetchReturnReasons = async () => {
  await axios
    .get("https://localhost:7183/api/Orders/ReturnReasons")
    .then((response) => {
      reReason.value = response.data;
      console.log(reReason.value);
    })
    .catch((error) => {
      alert(error);
    });
};
const activityclose = async () => {
  await axios
    .put(`https://localhost:7183/api/Orders/activitycolse`)
    .then((response) => {
      //console.log(response.data);
      //loadGetOrders();
    })
    .catch((error) => {
      alert(error);
    });
};
const ChangeOrders = async () => {
  await axios
    .put(`https://localhost:7183/api/Orders/Change?orderid=${retrunId.value}`)
    .then((response) => {
      //console.log(response.data);
      alert(response.data);
      loadGetOrders();
    })
    .catch((error) => {
      alert(error);
    });
};
const setchangeIdValue = (paramValue) => {
  retrunId.value = paramValue;
  ChangeOrders();
};
// const ReturnReason = async () => {
//   if (this.selectedReason === null) {
//     alert('请选择退货原因');
//     return;
//   }
//   await axios
//     .put(`https://localhost:7183/api/Orders/ReturnReason?id=${selectedReason.value}`)
//     .then((response) => {
//       alert(response.data);
//       //loadGetOrders();
//     })
//     .catch((error) => {
//       alert(error);
//     });
// };
const formatOrderTime = (ordertime) => {
  const dateTimeObject = new Date(ordertime);
  const year = dateTimeObject.getFullYear();
  const month = (dateTimeObject.getMonth() + 1).toString().padStart(2, "0");
  const dates = dateTimeObject.getDate().toString().padStart(2, "0");
  const hours = dateTimeObject.getHours().toString().padStart(2, "0");
  const minutes = dateTimeObject.getMinutes().toString().padStart(2, "0");
  return year + "-" + month + "-" + dates + " " + hours + ":" + minutes + "";
};
const setTypeValue = async (paramValue) => {
  Type.value = paramValue;
  //loadGetOrders();
};
const setostatusValue = async (paramValue) => {
  ostatus.value = paramValue;
  loadGetOrders();
};

const emit = defineEmits(["searchInput"]);
const inputhandler = () => {
  emit1("searchInput", keyword.value);
  loadGetOrders();
};

const inputbegintime = () => {
  emit2("searchInputbegintime", begintime.value);
  loadGetOrders();
};

const inputendtime = () => {
  emit3("searchInputendtime", endtime.value);
  loadGetOrders();
};

const keywordSearch = () => {
  loadGetOrders();
};

const toggleDetails = (itemId) => {
  if (expandedItems.value.includes(itemId)) {
    const index = expandedItems.value.indexOf(itemId);
    expandedItems.value.splice(index, 1);
  } else {
    expandedItems.value.push(itemId);
  }
};

onMounted(() => {
  Type.value = 1;
  flatpickr(".datePicker", {
    enableTime: false,
    maxDate: "today",
    dateFormat: "Y-m-d",
  });
  activityclose();
  loadGetOrders();
  fetchReturnReasons();
});
</script>
<style scoped>
#orderItemDetail>h6 {
  color: red;
}

.table>thead>tr>th {
  background-color: maroon;
  color: white;
  text-align: center;
}

.table>tbody>tr>td {
  text-align: center;
}

#cate>button {
  text-align: center;
  padding-right: 20px;
  margin-right: 80px;
  font-size: 30px;
}

#cate {
  background-color: antiquewhite;
  text-align: center;
  width: 60%;
}

#cateorder {
  background-color: antiquewhite;
  text-align: center;
  width: 60%;
}

#cateorder>button {
  text-align: center;
  padding-right: 20px;
  margin-right: 80px;
  font-size: 30px;
}

#searchorder {
  text-align: center;
  width: 60%;
  display: flex;
  justify-content: center;
  align-items: center;
}

#searchorder>input {
  margin: 20px;
  padding: 10px;
  width: 300px;
}

#searchorder>button {
  margin: 20px;
  padding: 10px;
  width: 60px;
  position: relative;
}

#srarchdate {
  text-align: center;
  width: 60%;
  display: flex;
  justify-content: center;
  align-items: center;
}

#srarchdate>input {
  margin: 20px;
  padding: 10px;
  width: 300px;
}
</style>

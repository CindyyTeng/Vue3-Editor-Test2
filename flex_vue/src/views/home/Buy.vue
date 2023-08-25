<template>
  <main>
    <div class="container">
      <div class="row gx-4">
        <div class="buy-info col-12 col-lg-7">
          <form action="" method="post">
            <!-- 寄送資訊 -->
            <div id="step-1-area" class="step-area">
              <h2>輸入你的聯絡資訊:</h2>
              <div class="input-wrapper">
                <input
                  type="text"
                  name="ContactInfo.ContactName"
                  id="contact-name"
                  placeholder="姓名"
                  v-if="cart"
                  v-model="cart.checkoutData.contactInfo.contactName"
                />
                <span></span>
              </div>
              <div class="input-wrapper">
                <input
                  type="text"
                  name="ContactInfo.PostalCode"
                  id="postal-code"
                  placeholder="郵遞區號"
                  v-if="cart"
                  v-model="cart.checkoutData.contactInfo.postalCode"
                />
                <span></span>
              </div>
              <div class="input-wrapper">
                <input
                  type="text"
                  name="ContactInfo.Address"
                  id="address"
                  placeholder="地址"
                  v-if="cart"
                  v-model="cart.checkoutData.contactInfo.address"
                />
                <span></span>
                <div class="address-area">
                  <button id="address-btn" type="button">變更地址</button>
                  <ul id="address-box">
                    <li><h2>已儲存地址</h2></li>
                    <li
                      class="address-option"
                      v-if="
                        addresses.commonAddress != '' &&
                        addresses.commonAddress != null
                      "
                    >
                      {{ addresses.commonAddress }}
                    </li>
                    <li
                      class="address-option"
                      v-if="
                        addresses.alternateAddress1 != '' &&
                        addresses.alternateAddress1 != null
                      "
                    >
                      {{ addresses.alternateAddress1 }}
                    </li>
                    <li
                      class="address-option"
                      v-if="
                        addresses.alternateAddress2 != '' &&
                        addresses.alternateAddress2 != null
                      "
                    >
                      {{ addresses.alternateAddress2 }}
                    </li>
                  </ul>
                </div>
              </div>
              <div class="input-wrapper">
                <input
                  type="email"
                  name="ContactInfo.Email"
                  id="email"
                  placeholder="電子郵件"
                  v-if="cart"
                  v-model="cart.checkoutData.contactInfo.email"
                />
                <span></span>
              </div>
              <div class="input-wrapper">
                <input
                  type="tel"
                  name="ContactInfo.Phone"
                  id="phone"
                  placeholder="電話號碼"
                  v-if="cart"
                  v-model="cart.checkoutData.contactInfo.phone"
                />
                <span></span>
              </div>
              <button
                type="button"
                id="next-step-btn-1"
                class="change-step-btn next-step-btn"
                target-step="1"
              >
                繼續
              </button>
            </div>
            <!-- 帳單 -->
            <div id="step-2-area" class="step-area">
              <h2>輸入你的帳單地址:</h2>
              <label class="same-address-label buy-label">
                <input
                  type="checkbox"
                  id="bill-same-address"
                  class="buy-checkbox"
                  checked
                />
                帳單地址同送貨地址
              </label>
              <div id="different-address-area">
                <div class="input-wrapper">
                  <input
                    type="text"
                    name="BillingAddress.Name"
                    id="bill-name"
                    placeholder="姓名"
                  />
                  <span></span>
                </div>
                <div class="input-wrapper">
                  <input
                    type="text"
                    name="BillingAddress.PostalCode"
                    id="bill-postal-code"
                    placeholder="郵遞區號"
                  />
                  <span></span>
                </div>
                <div class="input-wrapper">
                  <input
                    type="text"
                    name="BillingAddress.Address"
                    id="bill-address"
                    placeholder="地址"
                  />
                  <span></span>
                </div>
                <div class="input-wrapper">
                  <input
                    type="tel"
                    name="BillingAddress.Phone"
                    id="bill-phone"
                    placeholder="電話號碼"
                  />
                  <span></span>
                </div>
              </div>
              <button
                type="button"
                id="next-step-btn-2"
                class="change-step-btn next-step-btn"
                target-step="2"
              >
                繼續
              </button>
            </div>
            <!-- 付款 -->
            <div id="step-3-area" class="step-area">
              <h2>使用優惠券?</h2>
              <a href="javascript:;" class="choose-coupon">選擇優惠券</a>
              <p class="show-coupon-info"></p>
              <input type="hidden" name="coupon-id" />
              <h2>付款方式</h2>
              <input type="hidden" name="payment-method" id="payment-method" />
              <div class="pay-by-select">
                <div class="pay-by-btn active" data-value="credit-card">
                  <i class="bi bi-credit-card"></i>
                  信用卡或金融簽帳卡
                </div>
              </div>
              <h2>詳細付款資訊:</h2>
              <div class="pay-info-area">
                <div class="input-wrapper">
                  <input
                    type="text"
                    name="PaymentInfo.CardName"
                    id="card-name"
                    placeholder="Name on card"
                    v-if="cart"
                    v-model="cart.checkoutData.paymentInfo.cardName"
                  />
                  <span></span>
                </div>
                <div class="input-wrapper">
                  <input
                    type="tel"
                    name="PaymentInfo.CardNumber"
                    id="card-number"
                    placeholder="Card Number"
                    v-if="cart"
                    v-model="cart.checkoutData.paymentInfo.cardNumber"
                  />
                  <span></span>
                </div>
                <div class="row row-cols-2">
                  <div class="input-wrapper">
                    <input
                      type="text"
                      name="PaymentInfo.Expiration"
                      id="expiration"
                      placeholder="MM/YY"
                      v-if="cart"
                      v-model="cart.checkoutData.paymentInfo.expiration"
                    />
                    <span></span>
                  </div>
                  <div class="input-wrapper">
                    <input
                      type="tel"
                      name="PaymentInfo.CVV"
                      id="cvv"
                      placeholder="CVV"
                      v-if="cart"
                      v-model="cart.checkoutData.paymentInfo.cvv"
                    />
                    <span></span>
                  </div>
                </div>
                <label class="confirm-terms-label buy-label">
                  <input
                    type="checkbox"
                    id="confirm-terms"
                    class="buy-checkbox"
                  />
                  確認你同意 Flex 付款的 <a href="javascript:;">條款與條件</a>
                </label>
                <button type="submit" id="send-order" class="next-step-btn">
                  下訂單
                </button>
              </div>
            </div>
          </form>
          <div class="sent-info-area">
            <div class="step-block">
              <h2>寄送資訊</h2>
              <div class="show-info">
                <!-- 姓名 -->
                <p>{{ cart?.checkoutData.contactInfo.contactName }}</p>
                <!-- 地址 -->
                <p>{{ cart?.checkoutData.contactInfo.address }}</p>
                <!-- Email -->
                <p>{{ cart?.checkoutData.contactInfo.email }}</p>
                <!-- 電話 -->
                <p>{{ cart?.checkoutData.contactInfo.phone }}</p>
                <button class="change-step-btn info-edit-btn" target-step="0">
                  編輯
                </button>
              </div>
            </div>
            <div class="step-block">
              <h2>帳單</h2>
              <div class="show-info">
                <!-- 姓名 -->
                <p>{{ cart?.checkoutData.billingAddress.name }}</p>
                <!-- 地址 -->
                <p>{{ cart?.checkoutData.billingAddress.address }}</p>
                <!-- 電話 -->
                <p>{{ cart?.checkoutData.billingAddress.phone }}</p>
                <button class="change-step-btn info-edit-btn" target-step="1">
                  編輯
                </button>
              </div>
            </div>
            <div class="step-block">
              <h2>付款</h2>
            </div>
          </div>
        </div>
        <div class="buy-summary col-12 col-lg-5">
          <div>
            <h2>訂單摘要</h2>
            <div>
              <div>運費</div>
              <div></div>
            </div>
            <div>
              <div>總計</div>
              <div></div>
            </div>
          </div>
          <div>
            <h2>訂單詳情</h2>
            <ul>
              <li>
                <div class="item-img-wrapper"></div>
                <div class="item-info"></div>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </main>
  <div id="coupon-area-bg">
    <div id="coupon-area">
      <div class="d-flex p-5">
        <h2 class="me-auto">優惠券</h2>
        <p>可選擇一張</p>
      </div>
      <ul class="coupon-list">
        <li class="coupon-item"></li>
      </ul>
    </div>
  </div>
</template>
    
<script setup lang='ts'>
import axios from "axios";
import { Input } from "postcss";
import { ref, onMounted, onUpdated, computed } from "vue";
import { ShoppingCart, Member } from "@/types/type";
import { storeToRefs } from "pinia";
import { useGetApiDataStore } from "@/stores/useGetApiDataStore.js";
// 用vite獲得環境變數
const baseAddress: string = import.meta.env.VITE_API_BASEADDRESS;
const getApiStore = useGetApiDataStore();
const { memberInfo } = storeToRefs(getApiStore);
const memberId = getApiStore.getMemberId;
const cart = ref<ShoppingCart>();
const member = ref<Member>();
const addresses = ref({
  commonAddress: "",
  alternateAddress1: "",
  alternateAddress2: "",
});

//
const loadCart = async (callback?: () => Promise<void>): Promise<void> => {
  let url: string = `${baseAddress}api/Cart/Checkout`;
  await axios
    .post<ShoppingCart>(url)
    .then((response) => {
      cart.value = response.data;
      console.log(response.data);
      callback?.();
    })
    .catch((error) => {
      alert(error);
    });
};

const loadMember = async (): Promise<void> => {
  let url: string = `${baseAddress}api/Users/1`;
  await axios
    .get<Member>(url)
    .then((response) => {
      member.value = response.data;
      if (cart.value?.checkoutData?.contactInfo) {
        cart.value.checkoutData.contactInfo.contactName = response.data.name;
        cart.value.checkoutData.contactInfo.address =
          response.data.commonAddress;
        cart.value.checkoutData.contactInfo.email = response.data.email;
        cart.value.checkoutData.contactInfo.phone = response.data.mobile;
        cart.value.checkoutData.contactInfo.postalCode = "";
      }

      addresses.value.commonAddress = response.data.commonAddress;
      addresses.value.alternateAddress1 = response.data.alternateAddress1;
      addresses.value.alternateAddress2 = response.data.alternateAddress2;
      console.log(cart.value);
      console.log(addresses.value);

      console.log(response.data);
    })
    .catch((error) => {
      alert(error);
    });
};

// 判斷input的title是否秀出來
function updateTitleVisibility(
  input: HTMLInputElement,
  title: HTMLElement,
  originalPlaceholder: string
): void {
  title.innerHTML = originalPlaceholder;

  if (input.value !== "") {
    title.classList.add("d-block");
    title.classList.remove("d-none");
  } else {
    title.classList.remove("d-block");
    title.classList.add("d-none");
  }
}

// 處理input的聚焦及失焦事件
function attachInputEventHandlers(
  input: HTMLInputElement,
  title: HTMLElement,
  originalPlaceholder: string
): void {
  input.addEventListener("focus", function () {
    input.removeAttribute("placeholder");
    title.classList.add("d-block");
    title.classList.remove("d-none");
  });

  input.addEventListener("blur", function () {
    input.setAttribute("placeholder", originalPlaceholder);
    updateTitleVisibility(input, title, originalPlaceholder);
  });
}

function updateBillingAddressFromContactInfo() {
  const checkbox = document.querySelector(
    "#bill-same-address"
  ) as HTMLInputElement;

  if (checkbox.checked && cart.value?.checkoutData?.contactInfo) {
    cart.value.checkoutData.billingAddress.name =
      cart.value.checkoutData.contactInfo.contactName;
    cart.value.checkoutData.billingAddress.postalCode =
      cart.value.checkoutData.contactInfo.postalCode;
    cart.value.checkoutData.billingAddress.address =
      cart.value.checkoutData.contactInfo.address;
    cart.value.checkoutData.billingAddress.phone =
      cart.value.checkoutData.contactInfo.phone;
  } else {
    if (cart.value?.checkoutData?.contactInfo) {
      const billName = document.querySelector("#bill-name") as HTMLInputElement;
      const billPostalCode = document.querySelector(
        "#bill-postal-code"
      ) as HTMLInputElement;
      const billAddress = document.querySelector(
        "#bill-address"
      ) as HTMLInputElement;
      const billPhone = document.querySelector(
        "#bill-phone"
      ) as HTMLInputElement;
      cart.value.checkoutData.billingAddress.name = billName.value;
      cart.value.checkoutData.billingAddress.postalCode = billPostalCode.value;
      cart.value.checkoutData.billingAddress.address = billAddress.value;
      cart.value.checkoutData.billingAddress.phone = billPhone.value;
    }
  }
}
function showAddressBoxEventHandler() {
  const addressBox = document.querySelector("#address-box")!;
  addressBox.classList.add("active");
}
function hideAddressBoxEventHandler() {
  const addressBox = document.querySelector("#address-box")!;
  addressBox.classList.remove("active");
}
function toggleAddressBoxEventHandler() {
  const addressBox = document.querySelector("#address-box")!;
  addressBox.classList.toggle("active");
}
class FlexCheckoutProcess {
  private _step = 0;
  private _stepAreas = document.querySelectorAll(".step-area");
  private _showInfos = document.querySelectorAll(".show-info");
  private _stepBlock = document.querySelectorAll(".step-block");

  constructor() {
    // 初始化
    this.setupStep1();
    this.setupStep2();
    this.setupStep3();
    this.initBtnClickHandler();
  }

  get step(): number {
    return this._step;
  }
  set step(nextStep: number) {
    if (nextStep < 0) {
      this._step = 0;
    } else if (nextStep > 2) {
      this._step = 2;
    } else {
      this._step = nextStep;
    }
  }
  process(): void {
    for (let i = 0; i < this._stepAreas.length; i++) {
      const isStep = i === this.step;
      this._stepAreas[i].classList.toggle("d-block", isStep);
      this._stepAreas[i].classList.toggle("d-none", !isStep);
      this._stepBlock[i].classList.toggle("active", isStep);

      if (i < this._stepAreas.length - 1) {
        const shouldShowInfo = i < this.step;
        this._showInfos[i].classList.toggle("d-block", shouldShowInfo);
        this._showInfos[i].classList.toggle("d-none", !shouldShowInfo);
      }
      window.scrollTo({ top: 0, behavior: "smooth" });
    }
  }
  private initBtnClickHandler(): void {
    const changeStepBtns = document.querySelectorAll(
      ".change-step-btn"
    ) as NodeListOf<HTMLElement>;
    changeStepBtns.forEach((btn) => {
      btn.addEventListener("click", () => {
        this.setStep(btn);
      });
    });
  }
  private setStep(btn: HTMLElement): void {
    let stepNum = parseInt(btn.getAttribute("target-step")!);
    this.step = stepNum;
    this.process();
  }
  private setupStep1(): void {
    const nextStepBtn1 = document.querySelector(
      "#next-step-btn-1"
    ) as HTMLButtonElement;
    const step1Inputs = document.querySelectorAll(
      "#step-1-area .input-wrapper input"
    ) as NodeListOf<HTMLInputElement>;
    this.checkInputsAndToggle(step1Inputs, nextStepBtn1, true);
    this.setupInputListeners(step1Inputs, nextStepBtn1, true);
  }

  private setupStep2(): void {
    const nextStepBtn2 = document.querySelector(
      "#next-step-btn-2"
    ) as HTMLButtonElement;
    const billSameAddress = document.querySelector(
      "#bill-same-address"
    ) as HTMLInputElement;
    const differentAddressArea = document.querySelector(
      "#different-address-area"
    ) as HTMLElement;
    const step2Inputs = document.querySelectorAll(
      "#step-2-area .input-wrapper input"
    ) as NodeListOf<HTMLInputElement>;

    const updateStep2Elements = (): void => {
      this.checkInputsAndToggle(
        step2Inputs,
        nextStepBtn2,
        !billSameAddress.checked
      );
      differentAddressArea.classList.toggle(
        "d-block",
        !billSameAddress.checked
      );
      differentAddressArea.classList.toggle("d-none", billSameAddress.checked);
    };

    updateStep2Elements();
    billSameAddress.addEventListener("change", updateStep2Elements);
    this.setupInputListeners(
      step2Inputs,
      nextStepBtn2,
      billSameAddress.checked
    );
  }

  setupStep3(): void {
    const sendOrderBtn = document.querySelector(
      "#send-order"
    ) as HTMLButtonElement;
    const confirmTerms = document.querySelector(
      "#confirm-terms"
    ) as HTMLInputElement;
    const step3Inputs = document.querySelectorAll(
      "#step-3-area .pay-info-area .input-wrapper input"
    ) as NodeListOf<HTMLInputElement>;

    const updateStep3Elements = (): void => {
      // 檢查是否所有 input 都有值
      const allInputsFilled = Array.from(step3Inputs).every(
        (input) => input.value.trim() !== ""
      );

      // 根據 confirmTerms 的狀態和 input 是否都有值，決定是否啟用按鈕
      sendOrderBtn.disabled = !(confirmTerms.checked && allInputsFilled);
    };

    confirmTerms.addEventListener("change", updateStep3Elements);
    updateStep3Elements(); // 初始化時執行一次
    step3Inputs.forEach((input) => {
      input.addEventListener("keyup", updateStep3Elements);
      input.addEventListener("change", updateStep3Elements);
    });
  }

  private checkInputsAndToggle(
    inputs: NodeListOf<HTMLInputElement>,
    btn: HTMLButtonElement,
    condition: boolean,
    defaultBtnDisabled: boolean = false
  ): void {
    const anyInputEmpty = Array.from(inputs).some(
      (input) => input.value.trim() === ""
    );
    btn.disabled = condition ? anyInputEmpty : defaultBtnDisabled;
  }

  private setupInputListeners(
    inputs: NodeListOf<HTMLInputElement>,
    btn: HTMLButtonElement,
    condition: boolean,
    defaultBtnDisabled: boolean = false
  ): void {
    inputs.forEach((input) => {
      input.addEventListener("keyup", () => {
        this.checkInputsAndToggle(inputs, btn, condition, defaultBtnDisabled);
      });
      input.addEventListener("change", () => {
        this.checkInputsAndToggle(inputs, btn, condition, defaultBtnDisabled);
      });
    });
  }
}

onMounted(() => {
  loadCart(loadMember);
  const nextStepBtn2 = document.querySelector("#next-step-btn-2");
  nextStepBtn2?.addEventListener("click", updateBillingAddressFromContactInfo);

  let flexCheckout = new FlexCheckoutProcess();
  flexCheckout.process();
  const addressBtn = document.querySelector("#address-btn");
  addressBtn?.addEventListener("click", (event) => {
    event.stopPropagation();
    toggleAddressBoxEventHandler();
  });
});

onUpdated(() => {
  const textInputs = document.querySelectorAll(
    ".input-wrapper > input"
  ) as NodeListOf<HTMLInputElement>;
  const textTitles = document.querySelectorAll(".input-wrapper > span");
  textInputs.forEach((input, index) => {
    const originalPlaceholder = input.getAttribute("placeholder")!;
    const title = textTitles[index] as HTMLElement;
    updateTitleVisibility(input, title, originalPlaceholder);
    attachInputEventHandlers(input, title, originalPlaceholder);
  });

  const addressOption = document.querySelectorAll(".address-option");
  const addressInput = document.querySelector("#address") as HTMLInputElement;
  addressOption.forEach((option) => {
    option.addEventListener("click", () => {
      if (cart.value?.checkoutData?.contactInfo) {
        cart.value.checkoutData.contactInfo.address = option.innerHTML;
      }
      hideAddressBoxEventHandler();
    });
  });
});
</script>
    
<style scoped lang="scss">
main {
  width: 70%;
  margin: auto;
  margin-top: 100px;

  .container {
    .row {
      .buy-info {
        form {
          h2 {
            font-size: 25px;
            margin-bottom: 30px;
          }

          .input-wrapper {
            position: relative;
            margin-bottom: 30px;

            & > span {
              background-color: #fff;
              position: absolute;
              left: 20px;
              top: -10px;
              padding: 0 10px;
              font-size: 14px;
              color: #999;
            }

            & > input {
              padding: 20px;
              width: 100%;
              height: 100%;
              border: 1.5px solid #999;
              border-radius: 7px;

              &.readonly {
                cursor: auto;
              }

              &:hover {
                border-color: #333;

                + span {
                  color: #333;
                }
              }

              &:focus {
                border-color: #333;

                + span {
                  color: #333;
                }
              }
            }

            .address-area {
              button {
                margin: 10px;
                padding: 10px 25px;
                background-color: #fff;
                border: 1.5px solid #ccc;
                border-radius: 50px;

                &:hover {
                  background-color: #eee;
                }
              }
              ul {
                background: #fff;
                border: 2px solid rgba($color: #000000, $alpha: 0);
                border-radius: 10px;
                overflow: hidden;
                width: 100%;
                max-height: 0;
                font-size: 18px;
                padding: 0;
                transition: max-height 0.6s;
                & > li:first-child {
                  margin-top: 15px;
                }
                & > li:last-child {
                  margin-bottom: 30px;
                }
                &.active {
                  border: 2px solid #999;
                  max-height: 500px;
                  &:hover {
                    border: 2px solid #000;
                  }
                }

                li {
                  padding: 20px 15px;
                  width: 100%;
                  h2 {
                    padding: 0;
                    margin: 0;
                  }
                  &.address-option {
                    cursor: pointer;
                    padding: 20px 45px;
                    border-radius: 10px;
                    &:hover {
                      background: #ccc;
                    }
                  }
                }
              }
            }
          }

          .buy-checkbox {
            transform: scale(1.7);
            margin-right: 20px;
            cursor: pointer;

            &:checked {
              background-color: #333;
            }
          }

          .buy-label {
            cursor: pointer;
            font-size: 20px;
            margin-bottom: 50px;

            & > a {
              text-decoration: underline;

              &:hover {
                color: #ccc;
              }
            }
          }

          .next-step-btn {
            width: 100%;
            background-color: #333;
            color: #fff;
            border-radius: 50px;
            padding: 15px 0;
            font-size: 20px;

            &:disabled {
              color: #999;
              background-color: #eee;
            }
          }

          .choose-coupon {
            text-decoration: underline;
            font-size: 18px;
          }

          .show-coupon-info {
            height: 18px;
            font-size: 18px;
            margin-bottom: 30px;
          }

          .pay-by-select {
            margin-bottom: 50px;

            .pay-by-btn {
              border: 1.5px #999 solid;
              padding: 20px;
              border-radius: 7px;
              font-size: 20px;
              cursor: pointer;

              &.active {
                border: 2.5px solid #333;
              }
            }
          }
        }

        .sent-info-area {
          margin-top: 30px;

          .step-block {
            padding: 30px 0;
            border-top: 1px solid #ccc;

            &.active > h2 {
              color: #333;
            }

            h2 {
              font-size: 28px;
              font-weight: 500;
              color: #999;
            }

            .show-info {
              position: relative;

              & > p {
                margin: 0;
                color: #999;
              }

              .info-edit-btn {
                position: absolute;
                right: 0;
                top: -50px;
                font-size: 20px;
                padding: 10px 30px;
                border: 1.5px solid #ccc;
                border-radius: 50px;

                &:hover {
                  background-color: #eee;
                }
              }
            }
          }
        }
      }
    }
  }
}
#coupon-area-bg {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  z-index: 9999;
  background-color: rgba($color: #000, $alpha: 0.5);
  #coupon-area {
    position: absolute;
    background-color: rgba($color: #fff, $alpha: 1);
    width: 700px;
    left: calc((100% - 700px) / 2);
    top: 100px;
    height: 700px;
    overflow-y: scroll;
  }
}
</style>
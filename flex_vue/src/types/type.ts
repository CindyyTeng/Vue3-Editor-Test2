import axios from "axios";
export class MatchDiscount {
    constructor(
      public discountId: number,
      public discountName: string,
      public discountDescription: string,
      public discountType: number,
      public discountValue: number,
      public conditionType: number,
      public conditionValue: number,
      public discountOrder: number
    ) {}
  }
  
  export class CartItemProduct {
    constructor(
      public productSaleId: string,
      public productName: string,
      public unitPrice: number,
      public salesPrice: number,
      public size: string,
      public color: string,
      public matchDiscounts: MatchDiscount[],
      public imgPath: string,
      public salesCategoryName: string,
      public salesCategoryNameStr: string,
      public tagsValue: string
    ) {}
  }
  
  export class AppliedDiscount {
    constructor(
      public id: number,
      public rule: any, // 需要確定 rule 的型別
      public products: CartItemProduct[],
      public amount: number
    ) {}
  }
  
  class Coupon {
    constructor(
      public id: number,
      public name: string,
      public sendingId: number
    ) {}
  }
  
  export class CartItem {
    constructor(
      public cartItemId: number,
      public cartId: number,
      public productId: number,
      public qty: number,
      public product: CartItemProduct,
      public subTotal: number,
      public unitSubTotal: number
    ) {}
  }
  
  export class ShoppingCart {
    constructor(
      public cartItems: CartItem[],
      public appliedDiscounts: AppliedDiscount[],
      public coupon: Coupon,
      public originalTotalAmount: number,
      public deliveryFee: number,
      public couponValue: number,
      public totalPrice: number,
      public checkoutData: CheckoutData
    ) {}
  }
  
 export class Member {
    constructor(
      public memberId: number,
      public fk_Level: number,
      public levelName: string,
      public name: string,
      public email: string,
      public mobile: string,
      public gender: boolean,
      public birthday: string,
      public commonAddress: string,
      public alternateAddress1: string,
      public alternateAddress2: string,
      public isSubscribeNews: boolean
    ) {}
  }
  
  export class CheckoutData {
    constructor(
      public contactInfo: ContactInfo,
      public billingAddress: BillingAddress,
      public paymentInfo: PaymentInfo
    ) {}
  }
  
  export class ContactInfo {
    constructor(
      public contactName: string,
      public postalCode: string,
      public address: string,
      public email: string,
      public phone: string
    ) {}
  }
  
  export class BillingAddress {
    constructor(
      public name: string,
      public postalCode: string,
      public address: string,
      public phone: string
    ) {}
  }
  
  export class PaymentInfo {
    constructor(
        public paymentMethod: string,
        public cardName: string,
        public cardNumber: string,
        public expiration: string,
        public cvv: string,
        public confirmTerms: boolean,
        public couponId?: number,
    ) {}
  }


// 購物車物件
export class ShoppingCartItem {
    private item: CartItem;

    constructor(cartitem: CartItem) {
        this.item = cartitem;
    }
    async addOneItem(callback?: () => void): Promise<void> {
        this.addItem(1, callback);
    }

    async addItem(qty: number, callback?: () => void): Promise<void> {
        this.item.qty += qty;
        this.updateItemQty(callback);
    }
    async removeOneItem(callback?: () => void): Promise<void> {
        this.removeItem(1, callback);
    }

    async removeItem(qty: number, callback?: () => void): Promise<void> {
        if (this.item.qty >= qty) {
            this.item.qty = Math.max(this.item.qty - qty, 0);
            if (this.item.qty > 0) {
                this.updateItemQty(callback);
            }
            if (this.item.qty <= 0) {
                const result = confirm("是否刪除此商品？");
                if (result) {
                    this.updateItemQty(callback);
                }
                else {
                    this.item.qty = 1;
                    this.updateItemQty(callback);
                }
            }
        }
    }

    getCartItemQty(): number {
        return this.item.qty;
    }

    private updateItemQty = async (callback?: () => void) => {
        let url: string = `${import.meta.env.VITE_API_BASEADDRESS}api/Cart/UpdateItem`;
        await axios
            .put(url, this.item)
            .then((response) => {
                if (callback) {
                    callback();
                }
            })
            .catch((error) => {
                alert(error);
                if (callback) {
                    callback();
                }
            });
    };
}
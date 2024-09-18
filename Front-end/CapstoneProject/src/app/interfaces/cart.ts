import { CartItem } from "./cartItem";

export interface Cart {
  cartItems: CartItem[];
  cartTotal?: number;
}

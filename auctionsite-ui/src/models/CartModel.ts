import CartItem from "./CartItem"

export default interface CartModel {
    id: number
    items: CartItem[]
}
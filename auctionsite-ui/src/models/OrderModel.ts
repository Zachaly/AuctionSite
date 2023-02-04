import OrderItem from "./OrderItem";

export default interface OrderModel {
    id: number,
    created: string,
    name: string,
    address: string,
    city: string,
    postalCode: string,
    phoneNumber: string,
    paymentId: string,
    items: OrderItem[]
}
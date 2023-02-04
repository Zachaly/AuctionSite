export default interface AddOrderRequest {
    name: string,
    address: string,
    city: string,
    postalCode: string,
    phoneNumber: string,
    email: string,
    paymentId: string,
    userId: string,
    cartId: number
}
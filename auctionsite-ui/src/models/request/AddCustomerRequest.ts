export default interface AddCustomerRequest {
    firstName: string,
    lastName: string,
    email: string,
    cardName: string,
    cardNumber: string,
    expirationYear: string,
    expirationMonth: string,
    cvc: string
}
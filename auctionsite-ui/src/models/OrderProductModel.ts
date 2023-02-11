import { RealizationStatus } from "./enum/RealizationStatus"

export default interface OrderProductModel {
    id: number
    stockId: number
    stockName: string
    quantity: number
    city: string
    postalCode: string
    email: string
    phoneNumber: string
    address: string
    name: string
    paymentId?: string
    creationDate: string
    status: RealizationStatus
}
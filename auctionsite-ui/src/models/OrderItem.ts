import { RealizationStatus } from "./enum/RealizationStatus";

export default interface OrderItem {
    orderStockId: number,
    productName: string,
    productId: number,
    stockName: string,
    quantity: number,
    price: number,
    status: RealizationStatus
}
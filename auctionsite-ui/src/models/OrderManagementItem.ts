import { RealizationStatus } from "./enum/RealizationStatus"

export default interface OrderManagementItem {
    orderStockId: number
    stockName: string
    quantity: number
    status: RealizationStatus
    created: string
}
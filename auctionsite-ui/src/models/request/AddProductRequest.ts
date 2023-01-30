import AddStockRequest from "./AddStockRequest";

export default interface AddProductRequest {
    userId: string,
    name: string,
    description: string,
    stockName: string,
    price: number,
    stocks: AddStockRequest[]
}
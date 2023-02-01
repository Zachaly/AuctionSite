import Stock from "./Stock";

export default interface ProductModel {
    id: number,
    userId: string,
    userName: string,
    name: string,
    description: string,
    price: string,
    stockName: string,
    stocks: Stock[],
    imageIds: number[],
    created: string
}
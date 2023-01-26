import AddProductOptionRequest from "./AddOptionRequest";

export default interface AddProductRequest {
    userId: string,
    name: string,
    description: string,
    optionName: string,
    price: number,
    options: AddProductOptionRequest[]
}
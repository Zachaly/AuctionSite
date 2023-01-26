import ProductOption from "./ProductOption";

export default interface ProductModel {
    id: number,
    userId: string,
    userName: string,
    name: string,
    description: string,
    price: string,
    optionName: string,
    options: ProductOption[]
}
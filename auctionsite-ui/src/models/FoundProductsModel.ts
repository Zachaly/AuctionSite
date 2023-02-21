import ProductListItem from "./ProductListItem";

export default interface FoundProductsModel {
    products: ProductListItem[],
    pageCount: number
}
import ListStock from "./ListStock";

export default interface ListModel {
    id: number,
    name: string,
    items: ListStock[]
}
import ResponseModel from "./ResponseModel";

export default interface DataResponseModel<T> extends ResponseModel {
    data?: T
}
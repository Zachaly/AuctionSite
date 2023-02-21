import { HttpParams } from "@angular/common/http"

export interface SearchProductsRequest {
    pageIndex?: number,
    pageSize?: number,
    categoryId?: number,
    name?: string
}

export const SearchRequestToParams = (request: SearchProductsRequest) : HttpParams => {
    let params = new HttpParams()

    if (request.pageIndex) {
        params = params.append('pageIndex', request.pageIndex)
    }
    if (request.pageSize) {
        params = params.append('pageSize', request.pageSize)
    }
    if (request.categoryId) {
        params = params.append('categoryId', request.categoryId)
    }
    if (request.name) {
        params = params.append('name', request.name)
    }

    return params
}
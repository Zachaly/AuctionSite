import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import DataResponseModel from 'src/models/DataResponseModel';
import FoundProductsModel from 'src/models/FoundProductsModel';
import ProductListItem from 'src/models/ProductListItem';
import ProductModel from 'src/models/ProductModel';
import AddProductRequest from 'src/models/request/AddProductRequest';
import PagedRequest from 'src/models/request/PagedRequest';
import { SearchProductsRequest, SearchRequestToParams } from 'src/models/request/SearchProductsRequest';
import UpdateProductRequest from 'src/models/request/UpdateProductRequest';
import UploadProductImagesRequest from 'src/models/request/UploadProductImagesRequest';
import ResponseModel from 'src/models/ResponseModel';
import { AuthService } from './auth.service';

const API_URL = 'https://localhost:5001/api/product'

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient, private authService: AuthService) { }

  httpOptions = () => ({
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.authService.userData.authToken}`
    })
  })

  addProduct(product: AddProductRequest): Observable<ResponseModel> {
    return this.http.post<ResponseModel>(API_URL, product, this.httpOptions())
  }

  getProducts(request: PagedRequest): Observable<DataResponseModel<ProductListItem[]>> {
    let params = new HttpParams()
    if (request.pageIndex) {
      params = params.append('PageIndex', request.pageIndex)
    }

    if (request.pageSize) {
      params = params.append('PageSize', request.pageSize)
    }

    if (request.userId) {
      params = params.append('UserId', request.userId)
    }

    if (request.categoryId) {
      params = params.append('categoryId', request.categoryId)
    }

    return this.http.get<DataResponseModel<ProductListItem[]>>(API_URL, {
      params
    })
  }

  getProduct(id: number): Observable<DataResponseModel<ProductModel>> {
    return this.http.get<DataResponseModel<ProductModel>>(`${API_URL}/${id}`)
  }

  uploadImages(request: UploadProductImagesRequest): Observable<any> {
    const form = new FormData()
    form.append('ProductId', request.productId.toString())

    for (let i = 0; i < request.images.length; i++) {
      form.append('Images', request.images.item(i)!)
    }

    return this.http.post(`${API_URL}/image`, form, {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${this.authService.userData.authToken}`
      })
    })
  }

  getPageCount(pageSize: number, userId?: string): Observable<DataResponseModel<number>> {
    let params = new HttpParams()
    params = params.append('pageSize', pageSize)
    if (userId) {
      params = params.append('userId', userId)
    }
    return this.http.get<DataResponseModel<number>>(`${API_URL}/page-count`, {
      params
    })
  }

  getCategoryPageCount(pageSize: number, categoryId: number): Observable<DataResponseModel<number>> {
    let params = new HttpParams()
    params = params.append('pageSize', pageSize)
    params = params.append('categoryId', categoryId)

    return this.http.get<DataResponseModel<number>>(`${API_URL}/page-count`, {
      params
    })
  }

  updateProduct(request: UpdateProductRequest): Observable<ResponseModel> {
    return this.http.put<ResponseModel>(API_URL, request, this.httpOptions())
  }

  deleteImage(id: number): Observable<any> {
    return this.http.delete(`${API_URL}/image/${id}`, this.httpOptions())
  }

  searchProducts(request: SearchProductsRequest): Observable<DataResponseModel<FoundProductsModel>> {
    const params = SearchRequestToParams(request)
    return this.http.get<DataResponseModel<FoundProductsModel>>(`${API_URL}/search`, { params })
  }

}

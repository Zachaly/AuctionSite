import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import DataResponseModel from 'src/models/DataResponseModel';
import ProductListItem from 'src/models/ProductListItem';
import ProductModel from 'src/models/ProductModel';
import AddProductRequest from 'src/models/request/AddProductRequest';
import PagedRequest from 'src/models/request/PagedRequest';
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

  getPageCount(pageSize: number): Observable<DataResponseModel<number>> {
    let params = new HttpParams()
    params = params.append('pageSize', pageSize)
    return this.http.get<DataResponseModel<number>>(`${API_URL}/page-count`, {
      params
    })
  }
}

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

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  apiUrl: string = 'https://localhost:5001/api/product'

  constructor(private http: HttpClient, private authService: AuthService) {
  }

  httpOptions = () => ({
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.authService.userData.authToken}`
    })
  })

  addProduct(product: AddProductRequest) : Observable<ResponseModel> {
    return this.http.post<ResponseModel>(this.apiUrl, product, this.httpOptions())
  }

  getProducts(request: PagedRequest) : Observable<DataResponseModel<ProductListItem[]>>{
    const params = new HttpParams()
    if(request.pageIndex){
      params.append('pageIndex', request.pageIndex)
    }

    if(request.pageSize){
      params.append('pageSize', request.pageSize)
    }

    return this.http.get<DataResponseModel<ProductListItem[]>>(this.apiUrl, {
      params
    })
  }

  getProduct(id: number) : Observable<DataResponseModel<ProductModel>> {
    return this.http.get<DataResponseModel<ProductModel>>(`${this.apiUrl}/${id}`)
  }

  uploadImages(request: UploadProductImagesRequest) : Observable<any> {
    const form = new FormData()
    form.append('ProductId', request.productId.toString())

    for(let i = 0; i < request.images.length; i++){
      form.append('Images', request.images.item(i)!)
    }

    return this.http.post(`${this.apiUrl}/image`, form, {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${this.authService.userData.authToken}`
      })
    })
  }
}

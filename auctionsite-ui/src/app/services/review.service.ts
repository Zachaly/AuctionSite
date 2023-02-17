import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import DataResponseModel from 'src/models/DataResponseModel';
import AddProductReviewRequest from 'src/models/request/AddProductReviewRequest';
import UpdateProductReviewRequest from 'src/models/request/UpdateProductReviewRequest';
import ProductReviewListModel from 'src/models/ReviewListModel';
import { AuthService } from './auth.service';

const API_URL = 'https://localhost:5001/api/product-review'

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  httpOptions = () => ({
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.authService.userData.authToken}`
    })
  })

  constructor(private http: HttpClient, private authService: AuthService) { }

  getProductReviews(productId: number) : Observable<DataResponseModel<ProductReviewListModel[]>>{
    return this.http.get<DataResponseModel<ProductReviewListModel[]>>(`${API_URL}/${productId}`)
  }

  addProductReview(request: AddProductReviewRequest) : Observable<any> {
    return this.http.post(API_URL, request, this.httpOptions())
  }

  updateProductReview(request: UpdateProductReviewRequest) : Observable<any> {
    return this.http.put(API_URL, request, this.httpOptions())
  }

  deleteProductReview(id: number) : Observable<any> {
    return this.http.delete(`${API_URL}/${id}`, this.httpOptions())
  }
}

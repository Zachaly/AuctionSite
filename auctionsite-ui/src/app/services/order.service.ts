import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import DataResponseModel from 'src/models/DataResponseModel';
import OrderListItem from 'src/models/OrderListItem';
import OrderModel from 'src/models/OrderModel';
import AddOrderRequest from 'src/models/request/AddOrderRequest';
import { AuthService } from './auth.service';

const API_URL = 'https://localhost:5001/api/order'

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  httpOptions = () => ({
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.authService.userData.authToken}`
    })
  })

  constructor(private http: HttpClient, private authService: AuthService) { }

  postOrder(request: AddOrderRequest): Observable<any> {
    return this.http.post(API_URL, request, this.httpOptions())
  }

  getOrders(userId: string): Observable<DataResponseModel<OrderListItem[]>> {
    return this.http.get<DataResponseModel<OrderListItem[]>>(`${API_URL}/user/${userId}`, {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${this.authService.userData.authToken}`
      })
    })
  }

  getOrder(id: number) : Observable<DataResponseModel<OrderModel>>{
    return this.http.get<DataResponseModel<OrderModel>>(`${API_URL}/${id}`, {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${this.authService.userData.authToken}`
      })
    })
  }
}

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import DataResponseModel from 'src/models/DataResponseModel';
import AddCustomerRequest from 'src/models/request/AddCustomerRequest';
import AddPaymentRequest from 'src/models/request/AddPaymentRequest';
import { AuthService } from './auth.service';

const API_URL = 'https://localhost:5001/api/stripe'

@Injectable({
  providedIn: 'root'
})
export class StripeService {

  httpOptions = () => ({
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.authService.userData.authToken}`
    })
  })

  constructor(private http: HttpClient, private authService: AuthService) { }

  postCustomer(request: AddCustomerRequest): Observable<DataResponseModel<string>> {
    return this.http.post<DataResponseModel<string>>(`${API_URL}/customer`, request, this.httpOptions())
  }

  postPayment(request: AddPaymentRequest): Observable<DataResponseModel<string>> {
    return this.http.post<DataResponseModel<string>>(`${API_URL}/payment`, request, this.httpOptions())
  }
}

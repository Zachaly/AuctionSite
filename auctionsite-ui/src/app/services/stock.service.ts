import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import AddStockRequest from 'src/models/request/AddStockRequest';
import UpdateStockRequest from 'src/models/request/UpdateStockRequest';
import { AuthService } from './auth.service';

const API_URL = 'https://localhost:5001/api/stock'

@Injectable({
  providedIn: 'root'
})
export class StockService {

  httpOptions = () => ({
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.authService.userData.authToken}`
    })
  })


  constructor(private authService: AuthService, private http: HttpClient) { }

  updateStock(request: UpdateStockRequest): Observable<any> {
    return this.http.put(API_URL, request, this.httpOptions())
  }

  addStock(request: AddStockRequest): Observable<any> {
    return this.http.post(API_URL, request, this.httpOptions())
  }

  deleteStock(id: number): Observable<any> {
    return this.http.delete(`${API_URL}/${id}`, this.httpOptions())
  }
}

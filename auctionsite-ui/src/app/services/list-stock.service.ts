import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import AddListStockRequest from 'src/models/request/AddListStockRequest';
import { AuthService } from './auth.service';

const API_URL = 'https://localhost:5001/api/list-stock'

@Injectable({
  providedIn: 'root'
})
export class ListStockService {

  private httpOptions = () => ({
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.authService.userData.authToken}`
    })
  })

  constructor(private http: HttpClient, private authService: AuthService) { }

  postListStock(request: AddListStockRequest) : Observable<any>{
    return this.http.post(API_URL, request, this.httpOptions())
  }

  deleteListStock(id: number) : Observable<any> {
    return this.http.delete(`${API_URL}/${id}`, this.httpOptions())
  }
}

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import CartModel from 'src/models/CartModel';
import DataResponseModel from 'src/models/DataResponseModel';
import AddCartItemRequest from 'src/models/request/AddCartItemRequest';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl = 'https:localhost:5001/api/cart'

  authToken = () => `Bearer ${this.authService.userData.authToken}`
  httpOptions = () => ({
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': this.authToken()
    })
  })

  cartCount: number = 0
  cartCountSubject: Subject<any> = new Subject()



  constructor(private http: HttpClient, private authService: AuthService) {
    authService.onToggleUser().subscribe(res => {
      this.addCart(res.userId).subscribe(() => {
        this.getCartCount(res.userId).subscribe(res => this.changeCartCount(res.data))
      })
    })
  }

  addCart(userId: string): Observable<any> {
    return this.http.put(this.apiUrl, { userId }, this.httpOptions())
  }

  getCartCount(userId: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/count/${userId}`, {
      headers: new HttpHeaders({
        'Authorization': this.authToken()
      })
    })
  }

  changeCartCount(count: number) {
    this.cartCount = count
    this.cartCountSubject.next(this.cartCount)
  }

  onChangeCount(): Observable<any> {
    return this.cartCountSubject.asObservable()
  }

  addCartItem(request: AddCartItemRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/item`, request, this.httpOptions())
  }

  getCart(userId: string): Observable<DataResponseModel<CartModel>> {
    return this.http.get<DataResponseModel<CartModel>>(`${this.apiUrl}/${userId}`, {
      headers: {
        'Authorization': this.authToken()
      }
    })
  }

  removeFromCart(itemId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/item/${itemId}`, {
      headers: {
        'Authorization': this.authToken()
      }
    })
  }
}

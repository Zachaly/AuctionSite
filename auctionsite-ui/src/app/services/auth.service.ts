import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import RegisterModel from 'src/models/request/RegisterModel';
import UserModel from 'src/models/UserModel';
import LoginModel from 'src/models/request/LoginModel';
import DataResponseModel from 'src/models/DataResponseModel';
import { Observable, Subject } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

const AUTH_TOKEN_KEY = 'auth-token'

const API_URL = 'https://localhost:5001/api/auth'

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  userData: UserModel = {
    userName: '',
    userId: '',
    authToken: ''
  }
  authorized: boolean = false
  authSubject: Subject<any> = new Subject()
  userSubject: Subject<UserModel> = new Subject()

  constructor(private http: HttpClient) { }

  register(model: RegisterModel, onSuccess: Function, onError: Function) {
    this.http.post(`${API_URL}/register`, model, httpOptions).subscribe({
      next: res => onSuccess(res),
      error: err => onError(err)
    })
  }

  login(credentials: LoginModel, onSuccess: Function, onError: Function) {
    this.http.post<DataResponseModel<UserModel>>(`${API_URL}/login`, credentials, httpOptions)
      .subscribe({
        next: (res) => {
          if (res.data) {
            this.userData = res.data
            this.userSubject.next(this.userData)
            this.toggleAuth()
            onSuccess()
          }
        },
        error: err => onError(err)
      })
  }

  logout() {
    this.userData = {
      userId: '',
      userName: '',
      authToken: ''
    }
    this.userSubject.next(this.userData)
    localStorage.setItem(AUTH_TOKEN_KEY, '')
    this.toggleAuth()
  }

  private toggleAuth() {
    this.authorized = !this.authorized
    this.authSubject.next(this.authorized)
  }

  onToggleAuth(): Observable<any> {
    return this.authSubject.asObservable()
  }

  onToggleUser(): Observable<UserModel> {
    return this.userSubject.asObservable()
  }

  readUserData() {
    if (localStorage.getItem(AUTH_TOKEN_KEY)) {
      this.http.get<DataResponseModel<UserModel>>(`${API_URL}/user`, {
        headers: new HttpHeaders({
          'Authorization': `Bearer ${localStorage.getItem(AUTH_TOKEN_KEY)}`
        })
      }).subscribe(res => {
        if (res.success) {
          this.userData = res.data ?? this.userData
          this.userSubject.next(this.userData)
          this.toggleAuth()
        }
      })
    }
  }

  loadUserDataFromApi() {
    this.http.get<DataResponseModel<UserModel>>(`${API_URL}/user`, {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${this.userData.authToken}`
      })
    }).subscribe(res => {
      if (res.success) {
        this.userData = res.data ?? this.userData
        this.userSubject.next(this.userData)
      }
    })
  }

  saveAuthToken() {
    localStorage.setItem(AUTH_TOKEN_KEY, this.userData.authToken)
  }
}

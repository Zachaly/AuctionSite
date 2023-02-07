import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import DataResponseModel from 'src/models/DataResponseModel';
import ListListItemModel from 'src/models/ListListItemModel';
import ListModel from 'src/models/ListModel';
import AddListRequest from 'src/models/request/AddListRequest';
import { AuthService } from './auth.service';

const API_URL = "https://localhost:5001/api/list"

@Injectable({
  providedIn: 'root'
})
export class ListService {

  private httpOptions = () => ({
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.authService.userData.authToken}`
    })
  })

  private authorizationOption = () => ({
    headers: new HttpHeaders({
      'Authorization': `Bearer ${this.authService.userData.authToken}`
    })
  })

  constructor(private http: HttpClient, private authService: AuthService) { }

  getUserLists(userId: string): Observable<DataResponseModel<ListListItemModel[]>> {
    return this.http.get<DataResponseModel<ListListItemModel[]>>(`${API_URL}/user/${userId}`, this.authorizationOption())
  }

  postList(request: AddListRequest): Observable<any> {
    return this.http.post(API_URL, request, this.httpOptions())
  }

  deleteList(id: number): Observable<any> {
    return this.http.delete(`${API_URL}/${id}`, this.authorizationOption())
  }

  getListById(id: number): Observable<DataResponseModel<ListModel>> {
    return this.http.get<DataResponseModel<ListModel>>(`${API_URL}/${id}`, this.authorizationOption())
  }
}

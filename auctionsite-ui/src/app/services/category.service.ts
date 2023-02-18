import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import Category from 'src/models/Category';
import DataResponseModel from 'src/models/DataResponseModel';

const API_URL = 'https://localhost:5001/api/category'

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { }

  getCategories() : Observable<DataResponseModel<Category[]>> {
    return this.http.get<DataResponseModel<Category[]>>(API_URL)
  }
}

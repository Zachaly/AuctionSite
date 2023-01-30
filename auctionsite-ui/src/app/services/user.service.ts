import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import UserProfileModel from 'src/models/UserProfileModel';
import DataResponseModel from 'src/models/DataResponseModel';



@Injectable({
  providedIn: 'root'
})
export class UserService {
  apiUrl: string = 'https://localhost:5001/api/user'

  constructor(private http: HttpClient, private authService: AuthService) {
  }

  getUser(id: string): Observable<DataResponseModel<UserProfileModel>> {
    return this.http.get<DataResponseModel<UserProfileModel>>(`${this.apiUrl}/${id}`)
  }

  httpOptions = () => ({
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.authService.userData.authToken}`
    })
  })

  updateProfile(model: UserProfileModel): Observable<any> {
    return this.http.put(this.apiUrl, model, this.httpOptions())
  }

  updateProfilePicture(userId: string, file?: File): Observable<any> {
    const form = new FormData()
    form.append('UserId', userId)
    form.append('File', file!)

    return this.http.put(`${this.apiUrl}/profile-picture`, form, {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${this.authService.userData.authToken}`
      })
    })
  }
}

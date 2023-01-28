import { Component, Input } from '@angular/core';
import RegisterModel from 'src/models/request/RegisterModel';
import { Gender } from 'src/models/enum/Gender';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent {
  @Input() model: RegisterModel = {
    username: '',
    email: '',
    password: ''
  }
  @Input() confirmPassword: string = ''
  gender = Gender
  confirmPasswordError: string[] = []

  validationErrors = { 
    Email: [],
    Username: [],
    Password: [],
    FirstName: [],
    LastName: [],
    City: [],
    Country: [],
    Address: [],
    PostalCode: [],
    PhoneNumber: []
  }

  constructor(private authService: AuthService, private router: Router) { }

  submit() {
    if (this.confirmPassword !== this.model.password) {
      this.confirmPasswordError.push('Passwords do not match!')
      
      return
    }
    this.authService.register(this.model, () => this.router.navigateByUrl('/login'), (err: HttpErrorResponse) => {
      if(err.error.validationErrors){
        this.validationErrors = err.error.validationErrors
      }
      console.log(this.validationErrors)
    })
  }
}

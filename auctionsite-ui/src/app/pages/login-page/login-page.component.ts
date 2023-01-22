import { Component, Input } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import LoginModel from 'src/models/LoginModel';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
  @Input() model: LoginModel = {
    email: '',
    password: ''
  }

  constructor(private authService: AuthService, private router: Router){

  }

  submit(){
    this.authService.login(this.model, () => this.router.navigateByUrl('/'), (err: any) => console.log(err))
  }
}

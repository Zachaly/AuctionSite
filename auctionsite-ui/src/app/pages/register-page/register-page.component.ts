import { Component, Input } from '@angular/core';
import RegisterModel from 'src/models/RegisterModel';
import { Gender } from 'src/models/enum/Gender';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

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

  constructor(private authService: AuthService, private router: Router) { }

  submit() {
    if (this.confirmPassword !== this.model.password) {
      alert('Password do not match')
      this.model.password = ''
      this.confirmPassword = ''
      return
    }
    this.authService.register(this.model, () => this.router.navigateByUrl('/login'), (err: any) => console.log('err'))
  }
}

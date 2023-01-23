import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import UserModel from 'src/models/UserModel';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  authorized: boolean = false
  user: UserModel = { authToken: '', userId: '', userName: '' }
  constructor(private authService: AuthService) {
    authService.onToggleAuth().subscribe(value => this.authorized = value)
    authService.onToggleUser().subscribe(value => this.user = value)
  }

  logout() {
    this.authService.logout()
  }
}

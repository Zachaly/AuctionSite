import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import UserModel from 'src/models/UserModel';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  authorized: boolean = false
  user: UserModel = { authToken: '', userId: '', userName: '' }
  cartCount: number = 0
  constructor(private authService: AuthService, private cartService: CartService) {
    authService.onToggleAuth().subscribe(value => this.authorized = value)
    authService.onToggleUser().subscribe(value => this.user = value)
    cartService.onChangeCount().subscribe(value => this.cartCount = value)
  }

  logout() {
    this.authService.logout()
  }
}

import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  authorized: boolean = false
  username: string = ''
  constructor(private authService: AuthService){
    authService.onToggleAuth().subscribe(value => this.authorized = value)
    authService.onToggleUser().subscribe(value => this.username = value.userName)
  }

  logout(){
    this.authService.logout()
  }
}

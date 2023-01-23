import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';
import { Gender } from 'src/models/enum/Gender';
import UserProfileModel from 'src/models/UserProfileModel';

@Component({
  selector: 'app-update-profile-page',
  templateUrl: './update-profile-page.component.html',
  styleUrls: ['./update-profile-page.component.css']
})
export class UpdateProfilePageComponent implements OnInit {
  user: UserProfileModel = { id: '', userName: '' }
  gender = Gender

  constructor(private userService: UserService, private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    if (!this.authService.authorized) {
      this.router.navigateByUrl('/login')
      return
    }

    this.userService.getUser(this.authService.userData.userId).subscribe(value => this.user = value.data ?? this.user)
  }

  submit(){
    this.userService.updateProfile(this.user).subscribe(res => this.router.navigateByUrl('/profile/' + this.authService.userData.userId))
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import UserProfileModel from 'src/models/UserProfileModel';
import { genderToString } from 'src/models/enum/Gender';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css']
})
export class ProfilePageComponent implements OnInit {
  profile: UserProfileModel = { id: '', userName: ''}
  genderToString = genderToString
  userId: string = ''

  constructor(private route: ActivatedRoute, private userService: UserService, private router: Router, private authService: AuthService) {
    this.userId = authService.userData.userId
   }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.userService.getUser(params['userId']).subscribe(user => this.profile = user.data ?? this.profile)
    })
  }

  goToUpdate(){
    this.router.navigateByUrl('/update-profile')
  }
}

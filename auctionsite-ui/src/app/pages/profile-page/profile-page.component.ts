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
  profile: UserProfileModel = { id: '', userName: '' }
  genderToString = genderToString
  userId: string = ''
  selectedPicture: File | null = null


  constructor(private route: ActivatedRoute, private userService: UserService, private router: Router, private authService: AuthService) {
    authService.onToggleUser().subscribe(value => this.userId = value.userId)
  }

  ngOnInit(): void {
    this.userId = this.authService.userData.userId
    this.route.params.subscribe(params => {
      this.userService.getUser(params['userId']).subscribe(user => this.profile = user.data ?? this.profile)
    })
  }

  goToUpdate() {
    this.router.navigateByUrl('/update-profile')
  }

  updatePicture(){
    this.userService.updateProfilePicture(this.authService.userData.userId, this.selectedPicture ?? undefined).subscribe({
      next: () => alert('Picture updated!')
    })
  }

  selectPicture(e: Event): void {
    this.selectedPicture = (e.target as HTMLInputElement).files![0]
    console.log(this.selectedPicture)
  }
}

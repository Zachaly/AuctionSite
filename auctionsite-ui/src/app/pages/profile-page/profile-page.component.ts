import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import UserProfileModel from 'src/models/UserProfileModel';
import { genderToString } from 'src/models/enum/Gender';
import { AuthService } from 'src/app/services/auth.service';
import ProductListItem from 'src/models/ProductListItem';
import { ProductService } from 'src/app/services/product.service';

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
  products: ProductListItem[] = []
  pageCount = 0

  constructor(private route: ActivatedRoute, private userService: UserService,
    private router: Router, private authService: AuthService,
    private productService: ProductService) {
    authService.onToggleUser().subscribe(value => this.userId = value.userId)
  }

  ngOnInit(): void {
    this.userId = this.authService.userData.userId
    this.route.params.subscribe(params => {
      this.userService.getUser(params['userId']).subscribe(user => {
        this.profile = user.data ?? this.profile
        this.productService.getPageCount(10, this.profile.id).subscribe(res => this.pageCount = res.data!)
        this.changePage(0)
      })
    })
  }

  goToUpdate() {
    this.router.navigateByUrl('/update-profile')
  }

  changePage(index: number) {
    this.productService.getProducts({ pageSize: 10, pageIndex: index, userId: this.profile.id })
      .subscribe(res => this.products = res.data!)
  }

  updatePicture() {
    this.userService.updateProfilePicture(this.authService.userData.userId, this.selectedPicture ?? undefined).subscribe({
      next: () => alert('Picture updated!')
    })
  }

  selectPicture(e: Event): void {
    this.selectedPicture = (e.target as HTMLInputElement).files![0]
  }
}

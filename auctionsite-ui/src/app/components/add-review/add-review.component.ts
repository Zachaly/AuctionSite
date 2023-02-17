import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { ReviewService } from 'src/app/services/review.service';
import AddProductReviewRequest from 'src/models/request/AddProductReviewRequest';

@Component({
  selector: 'app-add-review',
  templateUrl: './add-review.component.html',
  styleUrls: ['./add-review.component.css']
})
export class AddReviewComponent implements OnInit {
  @Input() productId: number = 0
  authorized: boolean = false
  request: AddProductReviewRequest = {
    userId: '',
    content: '',
    productId: 0,
    score: 1
  }

  @Output() submit: EventEmitter<AddProductReviewRequest> = new EventEmitter()

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.authorized = this.authService.authorized
    this.request.userId = this.authService.userData.userId

    this.authService.userSubject.subscribe(res => {
      this.authorized = res.userId !== ''
      this.request.userId = res.userId
    })
  }

  onSubmit(){
    this.request.productId = this.productId
    this.submit.emit(this.request)
  }
}

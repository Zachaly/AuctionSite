import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import UpdateProductReviewRequest from 'src/models/request/UpdateProductReviewRequest';
import ProductReviewListModel from 'src/models/ReviewListModel';

@Component({
  selector: 'app-update-review',
  templateUrl: './update-review.component.html',
  styleUrls: ['./update-review.component.css']
})
export class UpdateReviewComponent implements OnInit {
  @Input() review: ProductReviewListModel = {
    id: 0,
    content: '',
    userId: '',
    score: 0,
    userName: ''
  }

  @Output() submit: EventEmitter<UpdateProductReviewRequest> = new EventEmitter()
  @Output() delete: EventEmitter<number> = new EventEmitter()

  authorized = false

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.authorized = this.authService.authorized

    this.authService.userSubject.subscribe(res => {
      this.authorized = res.userId !== ''
    })
  }

  onSubmit() {
    const request: UpdateProductReviewRequest = {
      id: this.review.id,
      content: this.review.content,
      score: this.review.score
    }
    this.submit.emit(request)
  }

  onDelete() {
    this.delete.emit(this.review.id)
  }
}

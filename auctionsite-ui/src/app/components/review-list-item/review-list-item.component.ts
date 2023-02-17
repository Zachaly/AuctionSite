import { Component, Input } from '@angular/core';
import ProductReviewListModel from 'src/models/ReviewListModel';

@Component({
  selector: 'app-review-list-item',
  templateUrl: './review-list-item.component.html',
  styleUrls: ['./review-list-item.component.css']
})
export class ReviewListItemComponent {
  @Input() review: ProductReviewListModel = {
    id: 0,
    content: '',
    userId: '',
    userName: '',
    score: 0
  }
}

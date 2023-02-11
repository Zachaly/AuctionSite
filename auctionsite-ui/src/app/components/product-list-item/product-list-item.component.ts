import { Component, Input } from '@angular/core';
import ProductListItem from 'src/models/ProductListItem';

@Component({
  selector: 'app-product-list-item',
  templateUrl: './product-list-item.component.html',
  styleUrls: ['./product-list-item.component.css']
})
export class ProductListItemComponent {
  @Input() product: ProductListItem = { id: 0, name: '', imageId: 0 }
}

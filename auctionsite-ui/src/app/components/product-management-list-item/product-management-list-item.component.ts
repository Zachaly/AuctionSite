import { Component, Input } from '@angular/core';
import ProductListItem from 'src/models/ProductListItem';

@Component({
  selector: 'app-product-management-list-item',
  templateUrl: './product-management-list-item.component.html',
  styleUrls: ['./product-management-list-item.component.css']
})
export class ProductManagementListItemComponent {
  @Input() product: ProductListItem = { id: 0, name: '', imageId: 0 }
}

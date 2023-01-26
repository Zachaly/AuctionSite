import { Component, Input } from '@angular/core';
import AddProductOptionRequest from 'src/models/request/AddOptionRequest';

@Component({
  selector: 'app-added-product-option',
  templateUrl: './added-product-option.component.html',
  styleUrls: ['./added-product-option.component.css']
})
export class AddedProductOptionComponent {
  @Input() option: AddProductOptionRequest = { value: '', quantity: 0 }
}

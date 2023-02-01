import { Component, EventEmitter, Input, Output } from '@angular/core';
import CartItem from 'src/models/CartItem';

@Component({
  selector: 'app-cart-item',
  templateUrl: './cart-item.component.html',
  styleUrls: ['./cart-item.component.css']
})
export class CartItemComponent {
  @Input() item: CartItem = { productId: 0, stockOnHoldId: 0, value: '', quantity: 0, productName: '', price: 0 }
  @Output() remove: EventEmitter<any> = new EventEmitter()

  buttonClick(){
    this.remove.emit()
  }
}

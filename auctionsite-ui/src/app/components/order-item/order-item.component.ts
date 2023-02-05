import { Component, Input } from '@angular/core';
import OrderItem from 'src/models/OrderItem';

@Component({
  selector: 'app-order-item',
  templateUrl: './order-item.component.html',
  styleUrls: ['./order-item.component.css']
})
export class OrderItemComponent {
  @Input() item: OrderItem = {
    productId: 0,
    productName: '',
    stockName: '',
    orderStockId: 0,
    quantity: 0,
    price: 0
  }

  countPrice = () => this.item.quantity * this.item.price
}

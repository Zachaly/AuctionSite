import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StatusColor } from 'src/models/enum/RealizationStatus';
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
    price: 0,
    status: 0
  }

  @Output() moveStatus: EventEmitter<any> = new EventEmitter()

  countPrice = () => this.item.quantity * this.item.price
  colorClass = () => StatusColor(this.item.status)

  moveStatusClick() {
    this.moveStatus.emit()
    this.item.status++
  }
}

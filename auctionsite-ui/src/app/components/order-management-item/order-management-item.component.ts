import { Component, EventEmitter, Input, Output } from '@angular/core';
import { RealizationStatus, RealizationStatusToString, StatusColor } from 'src/models/enum/RealizationStatus';
import OrderManagementItem from 'src/models/OrderManagementItem';

@Component({
  selector: 'app-order-management-item',
  templateUrl: './order-management-item.component.html',
  styleUrls: ['./order-management-item.component.css']
})
export class OrderManagementItemComponent {
  @Input() order: OrderManagementItem = {
    orderStockId: 0,
    stockName: '',
    quantity: 0,
    status: RealizationStatus.Delivered,
    created: ''
  }

  @Output() moveStatus: EventEmitter<any> = new EventEmitter()

  statusToString = RealizationStatusToString

  colorClass = () => StatusColor(this.order.status)

  moveStatusClick(){
    this.moveStatus.emit()
  }
}

import { Component, EventEmitter, Input, Output } from '@angular/core';
import UpdateStockRequest from 'src/models/request/UpdateStockRequest';
import Stock from 'src/models/Stock';

@Component({
  selector: 'app-update-stock-item',
  templateUrl: './update-stock-item.component.html',
  styleUrls: ['./update-stock-item.component.css']
})
export class UpdateStockItemComponent {
  @Input() stock: Stock = {
    id: 0,
    value: '',
    quantity: 0
  }

  @Output() update: EventEmitter<UpdateStockRequest> = new EventEmitter()
  @Output() delete: EventEmitter<any> = new EventEmitter()

  updateClick(){
    const request: UpdateStockRequest = {
      id: this.stock.id!,
      value: this.stock.value,
      quantity: this.stock.quantity
    }

    this.update.emit(request)
  }

  deleteClick(){
    this.delete.emit()
  }
}

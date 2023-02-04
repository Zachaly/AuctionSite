import { Component, EventEmitter, Input, Output } from '@angular/core';
import AddOrderRequest from 'src/models/request/AddOrderRequest';

@Component({
  selector: 'app-add-order',
  templateUrl: './add-order.component.html',
  styleUrls: ['./add-order.component.css']
})
export class AddOrderComponent {
  @Input() order: AddOrderRequest = {
    address: '',
    cartId: 0,
    userId: '',
    name: '',
    city: '',
    postalCode: '',
    phoneNumber: '',
    email: '',
    paymentId: ''
  }

  @Output() addOrderInfo: EventEmitter<any> = new EventEmitter()

  submit(){
    this.addOrderInfo.emit()
  }
}

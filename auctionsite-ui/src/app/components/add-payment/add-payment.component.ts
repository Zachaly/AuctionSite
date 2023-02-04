import { Component, EventEmitter, Input, Output } from '@angular/core';
import AddCustomerRequest from 'src/models/request/AddCustomerRequest';
import AddPaymentRequest from 'src/models/request/AddPaymentRequest';

@Component({
  selector: 'app-add-payment',
  templateUrl: './add-payment.component.html',
  styleUrls: ['./add-payment.component.css']
})
export class AddPaymentComponent {
  @Input() payment: AddPaymentRequest = {
    amount: 0,
    customerId: '',
    email: ''
  }

  @Input() customer: AddCustomerRequest = {
    firstName: '',
    lastName: '',
    email: '',
    expirationMonth: '',
    expirationYear: '',
    cardName: '',
    cardNumber: '',
    cvc: ''
  }

  @Output() addPayment: EventEmitter<any> = new EventEmitter()

  submit(){
    this.addPayment.emit()
  }
}

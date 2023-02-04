import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import { OrderService } from 'src/app/services/order.service';
import { StripeService } from 'src/app/services/stripe.service';
import { UserService } from 'src/app/services/user.service';
import AddCustomerRequest from 'src/models/request/AddCustomerRequest';
import AddOrderRequest from 'src/models/request/AddOrderRequest';
import AddPaymentRequest from 'src/models/request/AddPaymentRequest';

@Component({
  selector: 'app-add-order-page',
  templateUrl: './add-order-page.component.html',
  styleUrls: ['./add-order-page.component.css']
})
export class AddOrderPageComponent implements OnInit {

  order: AddOrderRequest = {
    address: '',
    userId: '',
    cartId: 0,
    city: '',
    name: '',
    postalCode: '',
    phoneNumber: '',
    email: '',
    paymentId: ''
  }

  paying = false

  customer: AddCustomerRequest = {
    firstName: '',
    lastName: '',
    email: '',
    expirationMonth: '',
    expirationYear: '',
    cardName: '',
    cardNumber: '',
    cvc: ''
  }

  payment: AddPaymentRequest = {
    customerId: '',
    amount: 0,
    email: ''
  }

  constructor(private route: ActivatedRoute, private cartService: CartService,
    private authService: AuthService, private stripeService: StripeService,
    private userService: UserService, private orderService: OrderService,
    private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe(param => {
      this.order.cartId = param['cartId']

      if (this.authService.userData.userId) {
        this.loadInfo()
      }
      this.authService.authSubject.subscribe(() => this.loadInfo())
    })
  }

  loadInfo() {
    this.cartService.getCart(this.authService.userData.userId).subscribe(res => {
      let price = 0
      res.data!.items.forEach(i => {
        price += i.price * i.quantity
      })
      price *= 100
      this.payment.amount = price
    })

    this.userService.getUser(this.authService.userData.userId).subscribe(res => {
      this.order.address = res.data?.address ?? ''
      this.order.city = res.data?.city ?? ''
      this.order.name = `${res.data?.firstName} ${res.data?.lastName}`
      this.order.phoneNumber = res.data?.phoneNumber ?? ''
      this.order.postalCode = res.data?.postalCode ?? ''
      this.order.userId = this.authService.userData.userId
    })
  }

  goToPayment() {
    this.paying = true
    this.customer.email = this.order.email
    let name = this.order.name.split(' ')
    this.customer.firstName = name[0]
    this.customer.lastName = name[1]
  }

  addPayment() {
    this.stripeService.postCustomer(this.customer).subscribe(res => {
      this.payment.customerId = res.data!
      this.payment.email = this.customer.email
      this.stripeService.postPayment(this.payment).subscribe(res => {
        this.order.paymentId = res.data!
        this.orderService.postOrder(this.order).subscribe(res => {
          alert("Order added")
          this.router.navigateByUrl('/')
        })
      })
    })
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderService } from 'src/app/services/order.service';
import OrderItem from 'src/models/OrderItem';
import OrderModel from 'src/models/OrderModel';

@Component({
  selector: 'app-order-page',
  templateUrl: './order-page.component.html',
  styleUrls: ['./order-page.component.css']
})
export class OrderPageComponent implements OnInit {
  order: OrderModel = {
    id: 0,
    name: '',
    address: '',
    created: '',
    city: '',
    postalCode: '',
    phoneNumber: '',
    paymentId: '',
    items: []
  }
  constructor(private route: ActivatedRoute, private orderService: OrderService) { }
  ngOnInit(): void {
    this.route.params.subscribe(param => {
      this.orderService.getOrder(param['id'])
        .subscribe(res => this.order = res.data!)
    })
  }

  countPrice = (item: OrderItem) => item.quantity * item.price
}

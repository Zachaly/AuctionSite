import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { OrderService } from 'src/app/services/order.service';
import OrderListItem from 'src/models/OrderListItem';

@Component({
  selector: 'app-order-list-page',
  templateUrl: './order-list-page.component.html',
  styleUrls: ['./order-list-page.component.css']
})
export class OrderListPageComponent implements OnInit {
  orders: OrderListItem[] = []

  constructor(private orderService: OrderService, private authService: AuthService) { }
  ngOnInit(): void {
    if (!this.authService.userData.userId) {
      this.authService.authSubject.subscribe(res => {
        this.loadOrders()
      })
    } else {
      this.loadOrders()
    }
  }

  loadOrders() {
    this.orderService.getOrders(this.authService.userData.userId)
      .subscribe(res => this.orders = res.data!)
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderService } from 'src/app/services/order.service';
import OrderManagementItem from 'src/models/OrderManagementItem';

@Component({
  selector: 'app-product-management-page',
  templateUrl: './product-management-page.component.html',
  styleUrls: ['./product-management-page.component.css']
})
export class ProductManagementPageComponent implements OnInit {
  orders: OrderManagementItem[] = []

  constructor(private orderService: OrderService, private route: ActivatedRoute) {

  }

  ngOnInit(): void {
    this.route.params.subscribe(param => {
      this.orderService.getProductOrders(param['productId'])
        .subscribe(res => this.orders = res.data!)
    })
  }

  moveStatus(id: number) {
    this.orderService.moveRealizationStatus(id).subscribe()
  }
}

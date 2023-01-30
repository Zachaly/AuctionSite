import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from 'src/app/services/product.service';
import ProductModel from 'src/models/ProductModel';
import Stock from 'src/models/Stock';

@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrls: ['./product-page.component.css']
})
export class ProductPageComponent implements OnInit {
  product: ProductModel = {
    name: '',
    id: 0,
    userId: '',
    stockName: '',
    price: '',
    userName: '',
    description: '',
    stocks: [],
    imageIds: []
  }

  @Input() currentStock: Stock = { id: 0, value: '', quantity: 0 }
  currentImage?: number = 0

  constructor(private route: ActivatedRoute, private productService: ProductService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.productService.getProduct(params['id']).subscribe(res => {
        this.product = res.data ?? this.product
        this.currentImage = this.product.imageIds[0]
      })
    })
  }

  setImage(id: number) {
    this.currentImage = id
  }
}

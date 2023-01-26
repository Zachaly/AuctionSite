import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/services/product.service';
import ProductListItem from 'src/models/ProductListItem';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {
  products: ProductListItem[] = []

  constructor(private productService: ProductService) { }
  ngOnInit(): void {
    this.productService.getProducts({}).subscribe(res => this.products = res.data ?? [])
  }
}

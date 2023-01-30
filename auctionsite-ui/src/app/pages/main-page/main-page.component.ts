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
  pageCount: number = 0
  private pageSize = 15

  constructor(private productService: ProductService) { }
  ngOnInit(): void {
    this.productService.getPageCount(this.pageSize)
      .subscribe(res => {
        this.pageCount = res.data!
        this.getProducts(0)
      })
  }

  private getProducts(pageIndex: number) {
    this.productService.getProducts({ pageIndex, pageSize: this.pageSize })
      .subscribe(res => {
        this.products = res.data ?? []
      })
  }

  changePage(pageIndex: number) {
    this.getProducts(pageIndex)
  }
}

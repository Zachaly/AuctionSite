import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from 'src/app/services/product.service';
import ProductListItem from 'src/models/ProductListItem';

@Component({
  selector: 'app-category-page',
  templateUrl: './category-page.component.html',
  styleUrls: ['./category-page.component.css']
})
export class CategoryPageComponent implements OnInit {

  products: ProductListItem[] = []
  pageCount: number = 0
  private pageSize = 15
  categoryId: number = 0

  constructor(private productService: ProductService, private route: ActivatedRoute) { }
  ngOnInit(): void {
    this.route.params.subscribe(param => {
      this.categoryId = param['id']

      this.productService.getCategoryPageCount(this.pageSize, this.categoryId)
        .subscribe(res => {
          this.pageCount = res.data!
          this.getProducts(0)
        })
    })
  }

  private getProducts(pageIndex: number) {
    this.productService.getProducts({ pageIndex, pageSize: this.pageSize, categoryId: this.categoryId })
      .subscribe(res => {
        this.products = res.data ?? []
      })
  }

  changePage(pageIndex: number) {
    this.getProducts(pageIndex)
  }
}

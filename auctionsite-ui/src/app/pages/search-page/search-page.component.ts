import { Component } from '@angular/core';
import { ProductService } from 'src/app/services/product.service';
import ProductListItem from 'src/models/ProductListItem';
import { SearchProductsRequest } from 'src/models/request/SearchProductsRequest';

@Component({
  selector: 'app-search-page',
  templateUrl: './search-page.component.html',
  styleUrls: ['./search-page.component.css']
})
export class SearchPageComponent {
  searchRequest: SearchProductsRequest = { pageIndex: 0, pageSize: 10 }

  products: ProductListItem[] = []

  pageCount = 0

  constructor(private productService: ProductService) {

  }

  submit() {
    this.searchRequest.pageIndex = 0
    this.loadProducts()
  }

  changePage(index: number) {
    this.searchRequest.pageIndex = index
    this.loadProducts()
  }

  loadProducts() {
    this.productService.searchProducts(this.searchRequest).subscribe(res => {
      this.products = res.data!.products
      this.pageCount = res.data!.pageCount
    })
  }

  selectCategory(id: number) {
    this.searchRequest.categoryId = id != 0 ? id : undefined
  }
}

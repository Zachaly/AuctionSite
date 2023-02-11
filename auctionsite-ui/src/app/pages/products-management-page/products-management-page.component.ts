import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { ProductService } from 'src/app/services/product.service';
import ProductListItem from 'src/models/ProductListItem';

@Component({
  selector: 'app-products-management-page',
  templateUrl: './products-management-page.component.html',
  styleUrls: ['./products-management-page.component.css']
})
export class ProductsManagementPageComponent implements OnInit {
  products: ProductListItem[] = []
  currentPage: number = 0
  pageCount: number = 0

  constructor(private authService: AuthService, private productService: ProductService) { }

  ngOnInit(): void {
    if (this.authService.userData.userId) {
      this.loadProducts()
    }
    else {
      this.authService.userSubject.subscribe(() => this.loadProducts())
    }
  }

  loadProducts = () => {
    this.productService.getPageCount(20, this.authService.userData.userId).subscribe(res => this.pageCount = res.data!)
    this.productService.getProducts({ pageIndex: this.currentPage, pageSize: 20, userId: this.authService.userData.userId })
    .subscribe(res => this.products = res.data!)
  }

  changePage(index: number) {
    this.currentPage = index
    this.loadProducts()
  }
}

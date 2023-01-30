import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { ProductService } from 'src/app/services/product.service';
import AddStockRequest from 'src/models/request/AddStockRequest';
import AddProductRequest from 'src/models/request/AddProductRequest';
import UploadProductImagesRequest from 'src/models/request/UploadProductImagesRequest';

@Component({
  selector: 'app-add-product-page',
  templateUrl: './add-product-page.component.html',
  styleUrls: ['./add-product-page.component.css']
})
export class AddProductPageComponent implements OnInit {
  product: AddProductRequest = {
    userId: '',
    name: '',
    description: '',
    stockName: '',
    price: 0,
    stocks: []
  }

  currentStock: AddStockRequest = {
    value: '',
    quantity: 0
  }

  validationErrors = {
    Name: [],
    Description: [],
    StockName: [],
    Price: [],
    Stocks: []
  }

  images: FileList | null = null

  constructor(private authService: AuthService, private router: Router, private productService: ProductService) { }

  ngOnInit(): void {
    if (!this.authService.authorized) {
      this.router.navigateByUrl('/login')
    }

    this.product.userId = this.authService.userData.userId
  }

  submit() {
    if (this.product.stocks.length < 1) {
      alert('Product needs at least one stock!')
      return
    }

    this.productService.addProduct(this.product).subscribe({
      next: res => {
        if(!this.images){
          alert('Product added')
          this.router.navigateByUrl('/')
        }
        const imageRequest: UploadProductImagesRequest = {
          productId: res.newEntityId!,
          images: this.images!
        }

        this.productService.uploadImages(imageRequest).subscribe(() => {
          alert('Product added')
          this.router.navigateByUrl('/')
        })
      },
      error: (err: HttpErrorResponse) => {
        if (err.error.validationErrors) {
          this.validationErrors = err.error.validationErrors
        }
      }
    })
  }

  addStock() {
    this.product.stocks.push(this.currentStock)
    this.currentStock = {
      value: '',
      quantity: 0
    }
  }

  selectImages(e: Event): void {
    this.images = (e.target as HTMLInputElement).files
  }
}

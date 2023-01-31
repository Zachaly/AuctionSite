import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
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
  selectedQuantity: number = 0

  constructor(private route: ActivatedRoute, private productService: ProductService, private cartService: CartService, private authService: AuthService) { }

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

  isAuthorized = () => this.authService.authorized

  addToCart() {
    if (this.currentStock.id == 0) {
      alert("Select stock!")
      return
    }

    if (this.selectedQuantity < 1) {
      alert("Select quantity!")
      return
    }
    this.cartService.addCartItem({
      userId: this.authService.userData.userId,
      stockId: this.currentStock.id!,
      quantity: this.selectedQuantity
    }).subscribe({
      next: () => {
        alert('Product added to cart')
        this.cartService.getCartCount(this.authService.userData.userId)
          .subscribe(res => {
            this.cartService.changeCartCount(res.data)
          })
      }
    })
  }
}

import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import { ProductService } from 'src/app/services/product.service';
import { ReviewService } from 'src/app/services/review.service';
import ProductModel from 'src/models/ProductModel';
import AddProductReviewRequest from 'src/models/request/AddProductReviewRequest';
import UpdateProductReviewRequest from 'src/models/request/UpdateProductReviewRequest';
import ProductReviewListModel from 'src/models/ReviewListModel';
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
    imageIds: [],
    created: ''
  }

  reviews: ProductReviewListModel[] = []

  userReview?: ProductReviewListModel = undefined

  @Input() currentStock: Stock = { id: 0, value: '', quantity: 0 }
  currentImage?: number = 0
  selectedQuantity: number = 0

  addingToList = false

  constructor(private route: ActivatedRoute, private productService: ProductService,
    private cartService: CartService, private authService: AuthService,
    private reviewService: ReviewService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.productService.getProduct(params['id']).subscribe(res => {
        this.product = res.data ?? this.product
        this.currentImage = this.product.imageIds[0]
        this.loadReviews()
      })
    })
  }

  setImage(id: number) {
    this.currentImage = id
  }

  isAuthorized = () => this.authService.authorized
  userId = () => this.authService.userData.userId

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

  addReview(request: AddProductReviewRequest) {
    console.log(request)
    request.userId = this.authService.userData.userId
    this.reviewService.addProductReview(request).subscribe(() => this.loadReviews())
  }

  loadReviews() {
    this.reviewService.getProductReviews(this.product.id)
      .subscribe(res => {
        this.reviews = res.data!
        this.userReview = this.reviews.find(x => x.userId == this.userId())
      })
  }

  deleteReview(id: number) {
    this.reviewService.deleteProductReview(id).subscribe(() => this.loadReviews())
  }

  updateReview(request: UpdateProductReviewRequest) {
    if(!request.id){
      return
    }
    this.reviewService.updateProductReview(request).subscribe()
  }
}

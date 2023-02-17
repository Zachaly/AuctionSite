import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderService } from 'src/app/services/order.service';
import { ProductService } from 'src/app/services/product.service';
import { StockService } from 'src/app/services/stock.service';
import OrderManagementItem from 'src/models/OrderManagementItem';
import ProductModel from 'src/models/ProductModel';
import AddStockRequest from 'src/models/request/AddStockRequest';
import UpdateProductRequest from 'src/models/request/UpdateProductRequest';
import UpdateStockRequest from 'src/models/request/UpdateStockRequest';
import UploadProductImagesRequest from 'src/models/request/UploadProductImagesRequest';
import Stock from 'src/models/Stock';

@Component({
  selector: 'app-product-management-page',
  templateUrl: './product-management-page.component.html',
  styleUrls: ['./product-management-page.component.css']
})
export class ProductManagementPageComponent implements OnInit {
  orders: OrderManagementItem[] = []

  updateProduct: UpdateProductRequest = {
    id: 0,
    name: '',
    description: '',
    stockName: '',
    price: 0
  }

  validationErrors = {
    Name: [],
    Description: [],
    StockName: [],
    Price: [],
  }

  newStock: AddStockRequest = {
    quantity: 0,
    value: ''
  }

  stocks: Stock[] = []

  images: number[] = []

  imageFiles: FileList | null = null

  constructor(private orderService: OrderService, private route: ActivatedRoute,
    private productService: ProductService, private stockService: StockService) {

  }

  ngOnInit(): void {
    this.route.params.subscribe(param => {
      this.orderService.getProductOrders(param['productId'])
        .subscribe(res => this.orders = res.data!)

      this.loadProduct(param['productId'])
    })
  }

  moveStatus(id: number) {
    this.orderService.moveRealizationStatus(id).subscribe()
  }

  loadProduct(id: number) {
    this.productService.getProduct(id)
      .subscribe(res => {
        const data = res.data!

        this.updateProduct = {
          id: data.id,
          name: data.name,
          description: data.description,
          stockName: data.stockName,
          price: parseFloat(data.price.replace(',', '.'))
        }
        this.newStock.productId = data.id

        this.stocks = data.stocks

        this.images = data.imageIds
      })
  }

  addStock() {
    if (this.newStock.quantity <= 0) {
      return
    }

    this.stockService.addStock(this.newStock).subscribe(() => {
      this.loadProduct(this.updateProduct.id)
      this.newStock.value = ''
      this.newStock.quantity = 0
    })
  }

  deleteStock(id: number) {
    this.stockService.deleteStock(id).subscribe(() => this.stocks = this.stocks.filter(stock => stock.id !== id))
  }

  updateStock(stock: UpdateStockRequest) {
    this.stockService.updateStock(stock).subscribe(() => this.loadProduct(this.updateProduct.id))
  }

  submit() {
    this.productService.updateProduct(this.updateProduct).subscribe({
      next: res => {
        if (!this.imageFiles) {
          return
        }

        const imageRequest: UploadProductImagesRequest = {
          productId: this.updateProduct.id,
          images: this.imageFiles!
        }

        this.productService.uploadImages(imageRequest).subscribe(() => this.loadProduct(this.updateProduct.id))
      },
      error: (err: HttpErrorResponse) => {
        if (err.error.validationErrors) {
          this.validationErrors = err.error.validationErrors
        }
      }
    })
  }

  deleteImage(id: number) {
    if (confirm("Do you want to delete image?")) {
      this.productService.deleteImage(id).subscribe(() => this.images = this.images.filter(x => x !== id))
    }
  }

  selectImages(e: Event): void {
    this.imageFiles = (e.target as HTMLInputElement).files
  }
}

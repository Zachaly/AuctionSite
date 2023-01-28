import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { ProductService } from 'src/app/services/product.service';
import AddProductOptionRequest from 'src/models/request/AddOptionRequest';
import AddProductRequest from 'src/models/request/AddProductRequest';

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
    optionName: '',
    price: 0,
    options: []
  }

  currentOption: AddProductOptionRequest = {
    value: '',
    quantity: 0
  }

  validationErrors = {
    Name: [],
    Description: [],
    OptionName: [],
    Price: [],
    Options: []
  }

  constructor(private authService: AuthService, private router: Router, private productService: ProductService) { }

  ngOnInit(): void {
    if(!this.authService.authorized){
      this.router.navigateByUrl('/login')
    }

    this.product.userId = this.authService.userData.userId
  }

  submit(){
    if(this.product.options.length < 1){
      alert('Product needs at least one option!')
      return
    }

    this.productService.addProduct(this.product).subscribe({
      next: res => {
        alert('Product added')
        this.router.navigateByUrl('/')
      },
      error: (err: HttpErrorResponse) => {
        if(err.error.validationErrors){
          console.log(err.error.validationErrors)
          this.validationErrors = err.error.validationErrors
        }
      }
    })
  }

  addOption(){
    this.product.options.push(this.currentOption)
    this.currentOption = {
      value: '',
      quantity: 0
    }
  }
}

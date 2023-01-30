import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from 'src/app/services/product.service';
import ProductModel from 'src/models/ProductModel';
import ProductOption from 'src/models/ProductOption';

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
    optionName: '',
    price: '',
    userName: '',
    description: '',
    options: [],
    imageIds: []
  }

  @Input() currentOption: ProductOption = { id: 0, value: '', quantity: 0 }
  currentImage?: number = 0

  constructor(private route: ActivatedRoute, private productService: ProductService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.productService.getProduct(params['id']).subscribe(res => { 
        this.product = res.data ?? this.product
        this.currentImage = this.product.imageIds[0]
       })
    })
  }

  compareOptions(optOne: ProductOption, optTwo: ProductOption) : boolean{
    return optOne == optTwo
  }

  setImage(id: number){
    this.currentImage = id
    console.log(this.currentImage)
  }
}

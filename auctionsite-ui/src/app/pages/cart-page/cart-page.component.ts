import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import CartModel from 'src/models/CartModel';

@Component({
  selector: 'app-cart-page',
  templateUrl: './cart-page.component.html',
  styleUrls: ['./cart-page.component.css']
})
export class CartPageComponent implements OnInit {
  cart: CartModel = { id: 0, items: [] }

  constructor(private cartService: CartService, private authService: AuthService, private router: Router) { }
  ngOnInit(): void {
    if (this.authService.userData.userId) {
      this.getCart(this.authService.userData.userId)
      return
    }

    this.authService.onToggleUser().subscribe(value => {
      if (value.userId) {
        this.getCart(value.userId)
      }
    })
  }

  getCart(userId: string) {
    this.cartService.getCart(userId).subscribe(res => this.cart = res.data ?? this.cart)
  }

  removeFromCart(itemId: number) {
    this.cartService.removeFromCart(itemId)
      .subscribe(() => {
        this.cart.items = this.cart.items.filter(x => x.stockOnHoldId !== itemId)
        this.cartService.getCartCount(this.authService.userData.userId)
          .subscribe(res => this.cartService.changeCartCount(res.data))
      })
  }

  clearCart() {
    this.cart.items.forEach(item => {
      this.cartService.removeFromCart(item.stockOnHoldId).subscribe()
    })
    this.cart.items = []
  }

  countPrice(): number {
    let price = 0
    this.cart.items.forEach(item => price += item.price * item.quantity)
    return price
  }

  makeOrder(): void {
    this.router.navigateByUrl(`/add-order/${this.cart.id}`)
  }
}

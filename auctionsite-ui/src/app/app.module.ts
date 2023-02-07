import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http'

import { AppComponent } from './app.component';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { FormsModule } from '@angular/forms';
import { ProfilePageComponent } from './pages/profile-page/profile-page.component';
import { UpdateProfilePageComponent } from './pages/update-profile-page/update-profile-page.component';
import { ProfileListItemComponent } from './components/profile-list-item/profile-list-item.component';
import { AddProductPageComponent } from './pages/add-product-page/add-product-page.component';
import { AddedStockComponent as AddedStockComponent } from './components/added-stock/added-stock.component';
import { ProductListItemComponent } from './components/product-list-item/product-list-item.component';
import { ProductPageComponent } from './pages/product-page/product-page.component';
import { ErrorListComponent } from './components/error-list/error-list.component';
import { ImageComponent } from './components/image/image.component';
import { PageBarComponent } from './components/page-bar/page-bar.component';
import { CartPageComponent } from './pages/cart-page/cart-page.component';
import { CartItemComponent } from './components/cart-item/cart-item.component';
import { AddOrderPageComponent } from './pages/add-order-page/add-order-page.component';
import { OrderPageComponent } from './pages/order-page/order-page.component';
import { OrderListPageComponent } from './pages/order-list-page/order-list-page.component';
import { AddOrderComponent } from './components/add-order/add-order.component';
import { AddPaymentComponent } from './components/add-payment/add-payment.component';
import { OrderItemComponent } from './components/order-item/order-item.component';
import { UserListsPageComponent } from './pages/user-lists-page/user-lists-page.component';
import { AddStockToListComponent } from './components/add-stock-to-list/add-stock-to-list.component';
import { ListPageComponent } from './pages/list-page/list-page.component';

const route = (path: string, component: any) => (
  {
    path,
    component
  }
)

const routes: Routes = [
  {
    path: '',
    component: MainPageComponent
  },
  {
    path: 'login',
    component: LoginPageComponent
  },
  {
    path: 'register',
    component: RegisterPageComponent
  },
  {
    path: 'profile/:userId',
    component: ProfilePageComponent
  },
  {
    path: 'update-profile',
    component: UpdateProfilePageComponent
  },
  {
    path: 'add-product',
    component: AddProductPageComponent
  },
  {
    path: 'product/:id',
    component: ProductPageComponent
  },
  {
    path: 'cart',
    component: CartPageComponent
  },
  route('add-order/:cartId', AddOrderPageComponent),
  route('orders', OrderListPageComponent),
  route('order/:id', OrderPageComponent),
  route('lists', UserListsPageComponent),
  route('list/:id', ListPageComponent)
]

@NgModule({
  declarations: [
    AppComponent,
    MainPageComponent,
    NavbarComponent,
    LoginPageComponent,
    RegisterPageComponent,
    ProfilePageComponent,
    UpdateProfilePageComponent,
    ProfileListItemComponent,
    AddProductPageComponent,
    AddedStockComponent,
    ProductListItemComponent,
    ProductPageComponent,
    ErrorListComponent,
    ImageComponent,
    PageBarComponent,
    CartPageComponent,
    CartItemComponent,
    AddOrderPageComponent,
    OrderPageComponent,
    OrderListPageComponent,
    AddOrderComponent,
    AddPaymentComponent,
    OrderItemComponent,
    UserListsPageComponent,
    AddStockToListComponent,
    ListPageComponent,
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes),
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

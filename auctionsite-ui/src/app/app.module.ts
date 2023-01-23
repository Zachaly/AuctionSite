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
  }
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
    ProfileListItemComponent
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

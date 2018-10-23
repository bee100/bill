import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { TestController } from '../Proxies/Services/Test.service';
import { AppRoutingModule } from './/app-routing.module';
import { LoginComponent } from './login.component';
import { OverviewComponent } from './overview.component';
import { AuthGuardService } from '../shared/services/authGuardService';
import { AuthService } from '../shared/services/authService';
import { JwtHelperService } from '@auth0/angular-jwt/src/jwthelper.service';
import { TokenInterceptor } from '../shared/token.interceptor';
import { MainLayoutComponent } from './main.layout.component';
import { AccountController } from '../Proxies/Services/Account.service';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    OverviewComponent,
    MainLayoutComponent
  ],
  imports: [
      BrowserModule,
      HttpClientModule,
      AppRoutingModule,
      FormsModule,
      ReactiveFormsModule
  ],
  providers: [
    TestController,
    AccountController,
    AuthGuardService,
    AuthService,
    JwtHelperService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

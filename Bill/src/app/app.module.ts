import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { TestController } from '../Proxies/Services/Test.service';
import { AppRoutingModule } from './/app-routing.module';
import { LoginComponent } from './login.component';
import { OverviewComponent } from './overview.component';
import { AuthGuardService } from '../shared/services/authGuardService';
import { AuthService } from '../shared/services/authService';
import { JwtHelperService } from '@auth0/angular-jwt/src/jwthelper.service';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    OverviewComponent
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
    AuthGuardService,
    AuthService,
    JwtHelperService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

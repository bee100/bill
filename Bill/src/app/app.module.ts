import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http'; 

import { AppComponent } from './app.component';
import { TestController } from '../Proxies/Services/Test.service';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
      BrowserModule,
      HttpClientModule
  ],
  providers: [
    TestController
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

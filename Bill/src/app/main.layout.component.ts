import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { AccountController } from '../Proxies/Services/Account.service';

@Component({
  templateUrl: './main.layout.component.html',
  selector:'main-layout'
})
export class MainLayoutComponent implements OnInit {

  constructor(private _accountService: AccountController, ) { }

  ngOnInit() {
    this._accountService.get().subscribe(result => {
      console.log(result);
    });
  }

  logout() {
    localStorage.removeItem('token');
    window.location.reload();
  }
}

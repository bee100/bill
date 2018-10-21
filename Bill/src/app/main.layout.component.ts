import { Component } from '@angular/core';
import { OnInit } from '@angular/core';

@Component({
  templateUrl: './main.layout.component.html',
  selector:'main-layout'
})
export class MainLayoutComponent implements OnInit {

  constructor( ) { }

  ngOnInit() {
  }

  logout() {
    localStorage.removeItem('token');
    window.location.reload();
  }
}

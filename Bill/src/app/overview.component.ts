import { Component } from '@angular/core';
import { OnInit } from '@angular/core';

@Component({
  templateUrl: './overview.component.html',
})
export class OverviewComponent implements OnInit {

  constructor(
  ) { }

  ngOnInit() {
    console.log("overview");
  }
}

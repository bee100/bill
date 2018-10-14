import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { TestController } from 'src/Proxies/Services/Test.service';
import { PersonDto } from '../Proxies/Entities/PersonDto';

@Component({
  templateUrl: './overview.component.html',
})
export class OverviewComponent implements OnInit {

  private values: PersonDto[];

  constructor(
   private _testService: TestController
  ) { }

  ngOnInit() {
    console.log("overview");
  }

  getValues() {
    this._testService.get().subscribe(result => {
      this.values = result;
    })
  }
}

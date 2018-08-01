import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { TestController } from '../Proxies/Services/Test.service';
import { PersonDto } from '../Proxies/Entities/PersonDto';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  apiValues: PersonDto[] = [];

  constructor(
    private _testService: TestController
  ) { }

  ngOnInit() {
    this._testService.get().subscribe(result => {
          this.apiValues = result;
      })
    }
}

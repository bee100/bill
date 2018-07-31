import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { TestDto } from '../Proxies/Entities/testDto';
import { TestController } from '../Proxies/Services/Test.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  apiValues: string[] = [];
  test: TestDto = new TestDto("Loading");

  constructor(
    private _testService: TestController
  ) { }

  ngOnInit() {
    this._testService.get().subscribe(result => {
      this.apiValues = result;
      this.apiValues.push("angulaar");
      })
    }
}

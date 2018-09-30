import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { TestController } from '../Proxies/Services/Test.service';
import { PersonDto } from '../Proxies/Entities/PersonDto';
import { LoginCredentialsDto } from '../Proxies/Entities/LoginCredentialsDto';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  error: string;

  constructor(
    private _testService: TestController
  ) { }

  ngOnInit() {
    this.loginForm = new FormGroup({
      username: new FormControl("", Validators.required),
      password: new FormControl("", Validators.required),
    })
  }

  login() {
    let credentials = new LoginCredentialsDto();
    credentials.username = this.loginForm.controls['username'].value;
    credentials.password = this.loginForm.controls['password'].value;

    this._testService.login(credentials).subscribe(result => {
      this.error = null;
      localStorage.setItem('token', result["token"]);
    }, error => {
      console.log(error);
      this.error = error.error;
    });
  }

  createAccount() {
      this._testService.create("Frank").subscribe(result => {
      console.log(result);
    });
  }
}

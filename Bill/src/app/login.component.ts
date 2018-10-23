import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { PersonDto } from '../Proxies/Entities/PersonDto';
import { LoginCredentialsDto } from '../Proxies/Entities/LoginCredentialsDto';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../shared/services/authService';
import { AccountController } from '../Proxies/Services/Account.service';

@Component({
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  error: string;

  constructor(
    private _accountService: AccountController,
    public router: Router,
    public auth: AuthService
  ) { }

  ngOnInit() {
    if (this.auth.isAuthenticated)
    {
      this.router.navigate(['/']);
    }

    this.loginForm = new FormGroup(
      {
        username: new FormControl("", Validators.required),
        password: new FormControl("", Validators.required),
      });
  }

  login() {
    let credentials = new LoginCredentialsDto();
    credentials.username = this.loginForm.controls['username'].value;
    credentials.password = this.loginForm.controls['password'].value;

    this._accountService.login(credentials).subscribe(result => {
      this.error = null;
      localStorage.setItem('token', result["token"]);
      this.router.navigate(['']);
    }, error => {
      console.log(error);
      this.error = error.error;
    });
  }

  createAccount() {
    this._accountService.create("Frank").subscribe(result => {
      console.log(result);
    });
  }
}

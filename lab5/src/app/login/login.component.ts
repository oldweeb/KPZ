import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Position } from '../models/IUser';
import { AuthService } from '../services/AuthService';
import { TokenStorageService } from '../services/TokenStorageService';

const urls = {
  0: 'professor',
  1: 'professor',
  2: 'student',
  3: 'admin'
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form = {
    email: '',
    password: ''
  };
  isLoggedIn = false;
  loginFailed = false;
  errorMessage = '';

  constructor(private authService: AuthService, private tokenService: TokenStorageService, private router: Router) {
  }

  ngOnInit(): void {
    if (this.tokenService.getPosition() !== null) {
      this.router.navigateByUrl(urls[this.tokenService.getPosition()!]);
    }

    if (this.tokenService.getToken()) {
      this.isLoggedIn = true;
    }
  }

  onSubmit(): void {
    const { email, password } = this.form;
    this.loginFailed = false;
    this.authService.login(email, password).subscribe(
      response => {
        this.isLoggedIn = true;
        this.loginFailed = false;
        this.tokenService.saveUser(response.token, response.position);
        const position: Position = parseInt(response.position);
        this.router.navigateByUrl(urls[position]);
      },
      ({ error }) => {
        console.log(error);
        this.loginFailed = true;
        this.errorMessage = error;
      }
    )
  }
}

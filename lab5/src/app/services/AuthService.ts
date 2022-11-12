import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const AUTH_URL = 'https://localhost:7223/Auth/login';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private httpClient: HttpClient) {
  }

  login(email: string, password: string): Observable<any> {
    return this.httpClient.post(AUTH_URL, { email, password });
  }
}
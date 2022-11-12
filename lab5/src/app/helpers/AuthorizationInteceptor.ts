import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HTTP_INTERCEPTORS } from "@angular/common/http";
import { Injectable, Provider } from "@angular/core";
import { Observable } from "rxjs";
import { TokenStorageService } from "../services/TokenStorageService";

@Injectable()
export class AuthorizationInterceptor implements HttpInterceptor {
  constructor(private tokenService: TokenStorageService) {
  }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.tokenService.getToken();
    if (token !== null) {
      request = request.clone({ headers: request.headers.set('Authorization', `Bearer ${token}`) });
    }

    return next.handle(request);
  }
}

export const authInterceptorProviders: Provider[] = [
  { provide: HTTP_INTERCEPTORS, useClass: AuthorizationInterceptor, multi: true }
];
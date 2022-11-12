import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HTTP_INTERCEPTORS } from "@angular/common/http";
import { Injectable, Provider } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()
export class ContentTypeInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (typeof (req.body) === 'object' && !!req.body) {
      req = req.clone({ headers: req.headers.set('Content-Type', 'application/json') });
    }

    return next.handle(req);
  }
}

export const contentTypeInterceptorProviders: Provider[] = [
  { provide: HTTP_INTERCEPTORS, useClass: ContentTypeInterceptor, multi: true }
];
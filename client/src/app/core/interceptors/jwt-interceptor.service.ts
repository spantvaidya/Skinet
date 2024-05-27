import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AccountService } from '../../account/account.service';
import { Observable, take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JwtInterceptorService implements HttpInterceptor {
  token?: string;

  constructor(private accountService: AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.accountService.currentUser$.pipe(take(1)).subscribe({next: user => this.token = user?.token})
    
    if (this.token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.token}`
        }
      })
    }
    return next.handle(request);
  }
}

import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, delay, finalize } from 'rxjs';
import { BusyService } from '../services/busy.service';

@Injectable({
  providedIn: 'root'
})
export class LoadingInterceptorService implements HttpInterceptor {

  constructor(private busyService: BusyService) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.url.includes('emailExists') ||
      req.method === 'POST' && req.url.includes('orders') ||
      req.method === 'DELETE'
    ) {
      return next.handle(req);
    }
    this.busyService.busy();
    return next.handle(req).pipe(
      delay(100),
      finalize(() => this.busyService.idle())
    )
  }
}

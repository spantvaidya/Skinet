import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ErrorInterceptorService implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error) {
          console.log(error);
          if (error.status === 400) {
            if (error.error.errors) {
              throw error.error;
            }
            else {
              this.toastr.error(error.error.errorMessage, error.status.toString());
            }
          }

          if (error.status === 401) {
            this.toastr.error(error.error.errorMessage, error.status.toString());
          } 
          
          if (error.status === 404) {
            this.toastr.error(error.error.errorMessage, error.status.toString());
          }

          if (error.status === 500) {
            this.toastr.error(error.error.errorMessage, error.status.toString());
          }

          if (error.status === 404) {
            this.router.navigateByUrl('/not-found');
          }

          if (error.status === 500) {
            const navigationExtra : NavigationExtras = {state: {error: error.error}};
            this.router.navigateByUrl('/server-error', navigationExtra);
          }
        }
        return throwError(() => new Error(error.error.errorMessage))
      })
    )
  }
}

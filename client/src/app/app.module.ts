import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule, provideHttpClient } from '@angular/common/http';
import { CoreModule } from './core/core.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'
import { HomeModule } from './home/home.module';
import { ErrorInterceptorService } from './core/interceptors/error-interceptor.service';
import { LoadingInterceptorService } from './core/interceptors/loading-interceptor.service';
import { JwtInterceptorService } from './core/interceptors/jwt-interceptor.service';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NgbModule,
    HttpClientModule,
    FontAwesomeModule,
    CoreModule,
    HomeModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptorService, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptorService, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptorService, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

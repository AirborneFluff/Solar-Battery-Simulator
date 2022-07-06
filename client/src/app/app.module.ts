import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgChartsModule } from 'ng2-charts';
import { LoginComponent } from './login/login.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NavbarComponent } from './navbar/navbar.component';
import { NgxNavbarModule } from 'ngx-bootstrap-navbar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component'
import { JwtInterceptor } from './_interceptors/jwt.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    TextInputComponent,
    NavbarComponent,
    HomeComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgChartsModule,
    ReactiveFormsModule,
    NgxNavbarModule,
    HttpClientModule,
    BrowserAnimationsModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: JwtInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }

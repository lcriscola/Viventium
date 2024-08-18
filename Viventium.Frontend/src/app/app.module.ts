import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DemoComponent } from './demo/demo.component';
import { FormsModule } from '@angular/forms';
import { provideHttpClient, withInterceptorsFromDi, withNoXsrfProtection } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    DemoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [
      provideHttpClient(withInterceptorsFromDi(), withNoXsrfProtection())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent, NavMenuComponent } from './layout';
import { AppRoutingModule } from './app-routing.module';
import {
  EmployeesComponent,
  PendingRequestsComponent,
  MyHolidaysComponent
} from './components';
import { Helpers, AdminGuard } from './helpers';
import { EmployeeService } from './services';
import { AppConfig } from './config/config';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    EmployeesComponent,
    PendingRequestsComponent,
    MyHolidaysComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AppRoutingModule
  ],
  providers: [
    Helpers,
    AdminGuard,
    AppConfig,
    EmployeeService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

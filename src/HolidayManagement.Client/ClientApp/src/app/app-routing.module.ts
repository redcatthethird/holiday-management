import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminGuard } from './helpers';
import {
    EmployeesComponent,
    PendingRequestsComponent,
    MyHolidaysComponent
} from './components';

const routes: Routes = [
  { path: '', redirectTo: 'my-holidays', pathMatch: 'full', },
  { path: 'my-holidays', component: MyHolidaysComponent },
  { path: 'pending-requests', component: PendingRequestsComponent, canActivate: [AdminGuard] },
  { path: 'employees', component: EmployeesComponent, canActivate: [AdminGuard] },
  //{ path: 'employees', component: EmployeesComponent, canActivate: [AdminGuard] }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
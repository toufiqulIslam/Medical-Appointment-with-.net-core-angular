import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { appointmentComponent } from './appointment/appointment.component';
import { AddEditAppointmentComponent } from './appointment/add-edit-appointment/add-edit-appointment.component';

const routes: Routes = [
  { path: 'appointment', component: appointmentComponent },
  { path: 'appointment/:id', component: AddEditAppointmentComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
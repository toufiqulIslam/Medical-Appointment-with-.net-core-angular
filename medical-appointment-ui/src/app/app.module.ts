import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { HeaderComponent } from './components/header/header.component';
import { ButtonComponent } from './components/button/button.component';
import { appointmentComponent } from './appointment/appointment.component';
import { AddEditAppointmentComponent } from './appointment/add-edit-appointment/add-edit-appointment.component';
import { ShowAppointmentComponent  } from './appointment/show-appointment/show-appointment.component';
import { AppointmentService } from './apiservice.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    ButtonComponent,
    appointmentComponent,
    appointmentComponent,
    ShowAppointmentComponent,
    AddEditAppointmentComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    ReactiveFormsModule
  ],
  providers: [AppointmentService],
  bootstrap: [AppComponent]
})
export class AppModule { }

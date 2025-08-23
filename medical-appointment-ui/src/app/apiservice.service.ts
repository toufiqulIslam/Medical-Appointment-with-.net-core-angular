import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Appointment } from '../app/models/appointment.model';
import { Patient } from '../app/models/patient.model';
import { Doctor } from '../app/models/doctor.model';

@Injectable({ providedIn: 'root' })
export class AppointmentService {
  private apiUrl = 'https://localhost:7055/api';

  constructor(private http: HttpClient) {}

  // Appointments
  getAppointments(page:number, pageSize:number, search:string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/appointments?page=${page}&pageSize=${pageSize}&search=${search}`);
  }
  getAppointment(id:number): Observable<Appointment> {
    return this.http.get<Appointment>(`${this.apiUrl}/appointments/${id}`);
  }
  createAppointment(data: Appointment): Observable<Appointment> {
    return this.http.post<Appointment>(`${this.apiUrl}/appointments`, data);
  }
  updateAppointment(id:number,data: Appointment): Observable<Appointment> {
    return this.http.put<Appointment>(`${this.apiUrl}/appointments/${id}`, data);
  }
  deleteAppointment(id:number) {
    return this.http.delete(`${this.apiUrl}/appointments/${id}`);
  }
  downloadPdf(id:number) {
    return this.http.get(`${this.apiUrl}/appointments/${id}/pdf`, { responseType: 'blob' });
  }
  sendEmail(id:number,email:string) {
    return this.http.post(`${this.apiUrl}/appointments/${id}/email?email=${email}`, {});
  }

  // Patients
  getPatients(): Observable<Patient[]> {
    return this.http.get<Patient[]>(`${this.apiUrl}/patients`);
  }

  // Doctors
  getDoctors(): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(`${this.apiUrl}/doctors`);
  }
}

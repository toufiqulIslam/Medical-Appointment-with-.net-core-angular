import { Prescription } from './prescription.model';
import { Patient } from './patient.model';
import { Doctor } from './doctor.model';

export interface Appointment {
  appointmentId?: number;
  patientId: number;
  doctorId: number;
  appointmentDate: string;
  visitType: string;
  diagnosis: string;
  notes: string;
  prescriptions: Prescription[];

  patient?: Patient;
  doctor?: Doctor;
}

import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AppointmentService } from 'src/app/apiservice.service';
import { Patient } from '../../models/patient.model';
import { Doctor } from '../../models/doctor.model';

@Component({
  selector: 'app-appointment-form',
  templateUrl: './add-edit-appointment.component.html',
  styleUrls: ['./add-edit-appointment.component.css']
})
export class AddEditAppointmentComponent implements OnInit {
  form!: FormGroup;
  id: number | null = null;
  patients: Patient[] = [];
  doctors: Doctor[] = [];

  constructor(
    private fb: FormBuilder,
    private api: AppointmentService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      patientId: ['', Validators.required],
      doctorId: ['', Validators.required],
      appointmentDate: ['', Validators.required],
      visitType: ['First', Validators.required], // 'First' | 'Follow-up'
      diagnosis: [''],
      notes: [''],
      prescriptions: this.fb.array([])
    });

    const param = this.route.snapshot.paramMap.get('id');
    this.id = param ? +param : null;

    if (this.id) {
    this.api.getAppointment(this.id).subscribe(res => {
      // ✅ fix appointmentDate
      res.appointmentDate = (res.appointmentDate || '').substring(0, 10);

      // normalize prescriptions
      (res.prescriptions || []).forEach((p: any) => {
        p.startDate = (p.startDate || '').substring(0, 10);
        p.endDate = (p.endDate || '').substring(0, 10);
      });

      this.form.patchValue(res);

      (res.prescriptions || []).forEach((p: any) => this.addPrescription(p));
    });
  }


    this.api.getPatients().subscribe(p => this.patients = p);
    this.api.getDoctors().subscribe(d => this.doctors = d);
  }

  get prescriptions(): FormArray {
    return this.form.get('prescriptions') as FormArray;
  }

  addPrescription(p?: any) {
    this.prescriptions.push(this.fb.group({
      medicine: [p?.medicine || '', Validators.required],
      dosage: [p?.dosage || '', Validators.required],
      startDate: [p?.startDate || '', Validators.required],
      endDate: [p?.endDate || '', Validators.required],
      notes: [p?.notes || '']
    }));
  }

  removePrescription(i: number) {
    this.prescriptions.removeAt(i);
  }

  private toYMD(d: any): string {
    if (!d) return '';
    const dt = new Date(d);
    const mm = String(dt.getMonth() + 1).padStart(2, '0');
    const dd = String(dt.getDate()).padStart(2, '0');
    return `${dt.getFullYear()}-${mm}-${dd}`;
  }

  submit() {
    const value = { ...this.form.value };
    value.appointmentDate = this.toYMD(value.appointmentDate);
    value.prescriptions = (value.prescriptions || []).map((p: any) => ({
      ...p,
      startDate: this.toYMD(p.startDate),
      endDate: this.toYMD(p.endDate)
    }));

    if (this.id) {
      this.api.updateAppointment(this.id, value)
        .subscribe(() => this.router.navigate(['/appointment']));
    } else {
      this.api.createAppointment(value)
        .subscribe(() => this.router.navigate(['/appointment']));
    }
  }
}

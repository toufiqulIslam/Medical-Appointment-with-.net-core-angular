import { Component, OnInit } from '@angular/core';
import { AppointmentService } from 'src/app/apiservice.service';

@Component({
  selector: 'app-show-appointment',
  templateUrl: './show-appointment.component.html',
  styleUrls: ['./show-appointment.component.css']
})
export class ShowAppointmentComponent implements OnInit {
  appointments: any[] = [];
  page = 1;
  pageSize = 5;
  total = 0;
  search = '';
  filterType = '';

  constructor(public appointmentService: AppointmentService) {}

  ngOnInit(): void {
    this.loadAppointments();
  }

  loadAppointments() {
    this.appointmentService.getAppointments(this.page, this.pageSize, this.search)
      .subscribe(res => {
        this.appointments = this.filterType
          ? res.data.filter((a: any) => a.visitType === this.filterType)
          : res.data;
        this.total = res.total;
      });
  }

  delete(id: number) {
    if (confirm("Delete this appointment?")) {
      this.appointmentService.deleteAppointment(id).subscribe(() => this.loadAppointments());
    }
  }

  searchAppointments() {
    this.page = 1;
    this.loadAppointments();
  }

  downloadPdf(id: number) {
    this.appointmentService.downloadPdf(id).subscribe(blob => {
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = 'PrescriptionReport.pdf';
      a.click();
    });
  }

  emailPatient(id: number) {
  this.appointmentService.sendEmail(id).subscribe({
    next: () => alert('✅ Email sent successfully!'),
    error: () => alert('❌ Failed to send email.')
  });
}

}
namespace MedicalAppointmentAPI.DTOs {
    public class AppointmentDto {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string VisitType { get; set; }
        public string Diagnosis { get; set; }
        public string Notes { get; set; }
        public List<PrescriptionDto> Prescriptions { get; set; }
    }
}
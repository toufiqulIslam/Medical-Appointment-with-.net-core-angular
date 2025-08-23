namespace MedicalAppointmentAPI.Models {
    public class Appointment {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string VisitType { get; set; }
        public string Diagnosis { get; set; }
        public string Notes { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
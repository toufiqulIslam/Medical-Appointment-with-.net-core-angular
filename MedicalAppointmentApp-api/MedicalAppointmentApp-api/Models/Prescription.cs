namespace MedicalAppointmentAPI.Models {
    public class Prescription {
        public int PrescriptionId { get; set; }
        public int AppointmentId { get; set; }
        public string Medicine { get; set; }
        public string Dosage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
        public Appointment Appointment { get; set; }
    }
}
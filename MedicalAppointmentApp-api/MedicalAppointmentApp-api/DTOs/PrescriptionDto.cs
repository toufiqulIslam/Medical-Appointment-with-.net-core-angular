namespace MedicalAppointmentAPI.DTOs {
    public class PrescriptionDto {
        public int PrescriptionId { get; set; }
        public string Medicine { get; set; }
        public string Dosage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
    }
}
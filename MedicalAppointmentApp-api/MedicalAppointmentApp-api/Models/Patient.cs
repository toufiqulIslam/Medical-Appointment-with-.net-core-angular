namespace MedicalAppointmentAPI.Models {
    public class Patient {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
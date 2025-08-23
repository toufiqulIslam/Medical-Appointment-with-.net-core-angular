using iTextSharp.text;
using iTextSharp.text.pdf;
using MedicalAppointmentAPI.Models;

namespace MedicalAppointmentAPI.Services {
    public class PdfService {
        public byte[] GeneratePdf(Appointment appointment) {
            using var ms = new MemoryStream();
            Document doc = new Document(PageSize.A4, 50, 50, 25, 25);
            PdfWriter.GetInstance(doc, ms);
            doc.Open();

            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            doc.Add(new Paragraph("Prescription Report", boldFont));
            doc.Add(new Paragraph($"Patient: {appointment.Patient.Name}"));
            doc.Add(new Paragraph($"Doctor: {appointment.Doctor.Name}"));
            doc.Add(new Paragraph($"Date: {appointment.AppointmentDate:d}"));
            doc.Add(new Paragraph($"Visit Type: {appointment.VisitType}"));
            doc.Add(new Paragraph($"Diagnosis: {appointment.Diagnosis}"));
            doc.Add(new Paragraph(" "));

            PdfPTable table = new PdfPTable(4) { WidthPercentage = 100 };
            table.AddCell("Medicine");
            table.AddCell("Dosage");
            table.AddCell("Start Date");
            table.AddCell("End Date");
            foreach (var p in appointment.Prescriptions) {
                table.AddCell(p.Medicine);
                table.AddCell(p.Dosage);
                table.AddCell(p.StartDate.ToShortDateString());
                table.AddCell(p.EndDate.ToShortDateString());
            }

            doc.Add(table);
            doc.Close();
            return ms.ToArray();
        }
    }
}
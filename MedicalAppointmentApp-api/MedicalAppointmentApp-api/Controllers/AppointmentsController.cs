using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalAppointmentAPI.Data;
using MedicalAppointmentAPI.DTOs;
using MedicalAppointmentAPI.Models;
using MedicalAppointmentAPI.Services;

namespace MedicalAppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly MedicalContext _context;
        private readonly PdfService _pdfService;
        private readonly EmailService _emailService;

        public AppointmentsController(MedicalContext context, PdfService pdfService, EmailService emailService)
        {
            _context = context;
            _pdfService = pdfService;
            _emailService = emailService;
        }

        // GET: api/appointments?page=1&pageSize=10&search=John
        [HttpGet]
        public async Task<IActionResult> GetAppointments(int page = 1, int pageSize = 10, string search = "")
        {
            var query = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Prescriptions)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(a =>
                    a.Patient.Name.Contains(search) ||
                    a.Doctor.Name.Contains(search));
            }

            var total = await query.CountAsync();

            var data = await query
                .OrderByDescending(a => a.AppointmentDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new
                {
                    a.AppointmentId,
                    a.AppointmentDate,
                    a.VisitType,
                    a.Diagnosis,
                    a.Notes,
                    Patient = new { a.Patient.PatientId, a.Patient.Name },
                    Doctor = new { a.Doctor.DoctorId, a.Doctor.Name },
                    Prescriptions = a.Prescriptions.Select(p => new {
                        p.PrescriptionId,
                        p.Medicine,
                        p.Dosage,
                        p.StartDate,
                        p.EndDate,
                        p.Notes
                    })
                })
                .ToListAsync();

            return Ok(new { total, data });
        }


        // GET api/appointments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Prescriptions)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null) return NotFound();
            return Ok(appointment);
        }

        // POST api/appointments
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentDto dto)
        {
            var appointment = new Appointment
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentDate = dto.AppointmentDate,
                VisitType = dto.VisitType,
                Diagnosis = dto.Diagnosis,
                Notes = dto.Notes,
                Prescriptions = dto.Prescriptions.Select(p => new Prescription
                {
                    Medicine = p.Medicine,
                    Dosage = p.Dosage,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Notes = p.Notes
                }).ToList()
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return Ok(appointment);
        }

        // PUT api/appointments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentDto dto)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Prescriptions)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null) return NotFound();

            appointment.AppointmentDate = dto.AppointmentDate;
            appointment.VisitType = dto.VisitType;
            appointment.Diagnosis = dto.Diagnosis;
            appointment.Notes = dto.Notes;

            // Replace prescriptions
            appointment.Prescriptions.Clear();
            appointment.Prescriptions = dto.Prescriptions.Select(p => new Prescription
            {
                Medicine = p.Medicine,
                Dosage = p.Dosage,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Notes = p.Notes
            }).ToList();

            await _context.SaveChangesAsync();
            return Ok(appointment);
        }

        // DELETE api/appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return NotFound();

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET api/appointments/{id}/pdf
        [HttpGet("{id}/pdf")]
        public IActionResult DownloadPdf(int id)
        {
            var appointment = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Prescriptions)
                .FirstOrDefault(a => a.AppointmentId == id);

            if (appointment == null) return NotFound();

            var pdfBytes = _pdfService.GeneratePdf(appointment);
            return File(pdfBytes, "application/pdf", "PrescriptionReport.pdf");
        }

        // POST api/appointments/{id}/email?email=abc@example.com
        [HttpPost("{id}/email")]
        public async Task<IActionResult> SendEmail(int id, [FromQuery] string email)
        {
            var appointment = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Prescriptions)
                .FirstOrDefault(a => a.AppointmentId == id);

            if (appointment == null) return NotFound();

            var pdfBytes = _pdfService.GeneratePdf(appointment);
            await _emailService.SendEmailAsync(email, "Prescription Report", "Please find attached your prescription report.", pdfBytes);

            return Ok("Email sent successfully.");
        }
    }
}

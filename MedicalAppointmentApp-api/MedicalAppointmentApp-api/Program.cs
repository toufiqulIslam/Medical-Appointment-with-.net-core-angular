using MedicalAppointmentAPI.Data;
using MedicalAppointmentAPI.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<MedicalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controllers

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });


// Services
builder.Services.AddScoped<PdfService>();
builder.Services.AddScoped<EmailService>();

// CORS
builder.Services.AddCors(p => p.AddPolicy("AllowAll",
    b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Medical Appointment API v1");
    c.RoutePrefix = "swagger";  // visit http://localhost:5000/swagger
});

// CORS
app.UseCors("AllowAll");

// Controllers
app.MapControllers();

// Run
app.Run();

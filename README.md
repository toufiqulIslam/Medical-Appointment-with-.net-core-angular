🩺 Medical Appointment System

The **Medical Appointment System** is a lightweight full-stack application that makes it easy to manage patients, doctors, appointments, and prescriptions. Built with **ASP.NET Core** on the backend and **Angular** on the frontend, it brings together a clean API, a responsive UI, and helpful extras like PDF reports and email notifications. Whether you’re booking a new consultation, checking follow-ups, or printing prescriptions, this project shows how modern .NET and Angular can work hand-in-hand to create a smooth medical workflow.

---

## 🛠 Tech Stack

On the backend, the system uses **ASP.NET Core 8**, **Entity Framework Core**, and **SQL Server** to store and query appointment data, wrapped with Swagger for easy API exploration. The frontend is built in **Angular 13+**, using TypeScript, RxJS, and forms for data binding. Together, they form a robust client–server app with CORS support, PDF generation, and email delivery built in.

---

## ⚙️ Setup Instructions

### 🔹 Backend
1. Navigate to the backend folder:

   cd MedicalAppointmentAPI


2. Update your connection string in `appsettings.json`:


   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=MedicalDB;Trusted_Connection=True;MultipleActiveResultSets=true"
   }

3. Run migrations to build the database:

4. Start the API:

### 🔹 Frontend

1. Navigate to the Angular project:

   cd medical-appointment-ui

2. Install packages:

 
   npm install
  
3. Run the development server:

   ng serve 

## ✨ What You Get

* A searchable, filterable **appointments grid**
* A dynamic **form with patient & doctor dropdowns**
* Inline editing for **prescriptions**
* One-click **PDF export** and **email send**
* Clean separation of backend and frontend for easy scaling

---

Start the backend, run the frontend, and within minutes you’ll have a fully functional medical appointment tracker running locally. 🚀



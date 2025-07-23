using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalSystemTask_OOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hospital hospital = new Hospital();
            bool exit = false;

            while (!exit) // Main menu loop
            {
                Console.Clear();
                Console.WriteLine("\n Hospital System Menu ");
                Console.WriteLine("1. Add Doctor");
                Console.WriteLine("2. Add Patient");
                Console.WriteLine("3. Book Appointment");
                Console.WriteLine("4. View All Appointments");
                Console.WriteLine("5. Search Appointments by Patient Name");
                Console.WriteLine("6. Search Appointments by Date");
                Console.WriteLine("7. Show Doctors by Specialization");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice) // Switch case for menu options
                {
                    case "1": AddDoctor(hospital); break;
                    case "2": AddPatient(hospital); break;
                    case "3": BookAppointment(hospital); break;
                    case "4": hospital.DisplayAllAppointments(); Console.ReadLine(); break;
                    case "5":
                        Console.Write("Enter patient name: ");
                        hospital.SearchAppointmentsByPatientName(Console.ReadLine());
                        Console.ReadLine(); break;
                    case "6":
                        Console.Write("Enter appointment date (yyyy-mm-dd): ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                            hospital.SearchAppointmentsByDate(date);
                        else
                            Console.WriteLine("Invalid date format.");
                        Console.ReadLine(); break;
                    case "7":
                        Console.Write("Enter specialization: ");
                        hospital.ShowAvailableDoctors(Console.ReadLine());
                        Console.ReadLine(); break;
                    case "0": exit = true; break;
                    default: Console.WriteLine("Invalid option. Try again."); break;
                }
            }
        }

        static void AddDoctor(Hospital hospital) // Method to add a doctor
        {
            Console.Clear();
            Console.WriteLine("Add Doctor");

            int id;
            while (true) // Loop to ensure unique doctor ID
            {
                id = GetValidInt("Enter doctor ID: ");
                if (hospital.DoctorIdExists(id))
                    Console.WriteLine("Doctor with this ID already exists.");
                else break;
            }

            string name;
            while (true) // Loop to ensure valid doctor name
            {
                Console.Write("Enter name: ");
                name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name) && name.Length >= 3)
                    break;
                Console.WriteLine("Name must be at least 3 characters.");
            }

            int age;
            while (true) // Loop to ensure valid doctor age
            {
                age = GetValidInt("Enter age: ");
                if (age >= 0 && age <= 120)
                    break;
                Console.WriteLine("Age must be between 0 and 120.");
            }

            string spec;
            while (true) // Loop to ensure valid specialization input
            {
                Console.Write("Enter specialization: ");
                spec = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(spec))
                    break;
                Console.WriteLine("Specialization cannot be empty.");
            }

            hospital.AddDoctor(new Doctor { Id = id, Name = name, Age = age, Specialization = spec });
            Console.WriteLine("Doctor added successfully.");
            Console.ReadLine();
        }

        static void AddPatient(Hospital hospital) // Method to add a patient
        {
            Console.Clear();
            Console.WriteLine("Add Patient");

            int id;
            while (true) // Loop to ensure unique patient ID
            {
                id = GetValidInt("Enter patient ID: ");
                if (hospital.PatientIdExists(id))
                    Console.WriteLine("Patient with this ID already exists.");
                else break;
            }

            string name;
            while (true) // Loop to ensure valid patient name
            {
                Console.Write("Enter name: ");
                name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name) && name.Length >= 3)
                    break;
                Console.WriteLine("Name must be at least 3 characters.");
            }

            int age;
            while (true) // Loop to ensure valid patient age
            {
                age = GetValidInt("Enter age: ");
                if (age >= 0 && age <= 120)
                    break;
                Console.WriteLine("Age must be between 0 and 120.");
            }

            Console.Write("Enter phone number: ");
            string phone = Console.ReadLine();

            hospital.AddPatient(new Patient { Id = id, Name = name, Age = age, PhoneNumber = phone });
            Console.WriteLine("Patient added successfully.");
            Console.ReadLine();
        }

        static void BookAppointment(Hospital hospital) // Method to book an appointment
        {
            Console.Clear();
            Console.WriteLine("Book Appointment");

            int docId;
            while (true) // Loop to ensure valid doctor ID
            {
                docId = GetValidInt("Enter doctor ID: ");
                if (hospital.DoctorIdExists(docId)) break;
                Console.WriteLine("Doctor with this ID does not exist.");
            }

            int patId;
            while (true) // Loop to ensure valid patient ID
            {
                patId = GetValidInt("Enter patient ID: ");
                if (hospital.PatientIdExists(patId)) break;
                Console.WriteLine("Patient with this ID does not exist.");
            }

            DateTime apptDate;
            while (true) // Loop to ensure valid appointment date & time
            {
                Console.Write("Enter appointment date & time (e.g., 2025-07-23 09:00): ");
                if (DateTime.TryParse(Console.ReadLine(), out apptDate)) break;
                Console.WriteLine("Invalid date/time format.");
            }

            hospital.BookAppointment(docId, patId, apptDate);
            Console.ReadLine();
        }

        static int GetValidInt(string prompt) // Method to get a valid integer input
        {
            int val;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out val))
                    return val;
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }

    public class Person // Base class for Doctor and Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public virtual void DisplayInfo() => Console.WriteLine($"ID: {Id}, Name: {Name}, Age: {Age}");
    }

    public class Doctor : Person // Inherits from Person
    {
        public string Specialization { get; set; }
        public List<DateTime> AvailableAppointments { get; set; } = new();
        public override void DisplayInfo() => Console.WriteLine($"Doctor: {Name}, Age: {Age}, Specialization: {Specialization}");
    }

    public class Patient : Person // Inherits from Person
    {
        public string PhoneNumber { get; set; }
        public override void DisplayInfo() => Console.WriteLine($"Patient: {Name}, Age: {Age}, Phone: {PhoneNumber}");
    }

    public class Appointment // Represents an appointment between a doctor and a patient
    {
        public int AppointmentId { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public DateTime AppointmentDate { get; set; }
        public void Display() => Console.WriteLine($"ID: {AppointmentId} | {AppointmentDate} | Dr. {Doctor.Name} ({Doctor.Specialization}) with {Patient.Name}");
    }

    public class Hospital // Represents the hospital system
    {
        private List<Doctor> doctors = new();
        private List<Patient> patients = new();
        private List<Appointment> appointments = new();
        private int appointmentCounter = 1;

        public void AddDoctor(Doctor doc) => doctors.Add(doc);
        public void AddPatient(Patient pat) => patients.Add(pat);

        public bool DoctorIdExists(int id) => doctors.Any(d => d.Id == id);
        public bool PatientIdExists(int id) => patients.Any(p => p.Id == id);

        public void BookAppointment(int doctorId, int patientId, DateTime date) // Method to book an appointment
        {
            var doctor = doctors.FirstOrDefault(d => d.Id == doctorId);
            var patient = patients.FirstOrDefault(p => p.Id == patientId);

            if (appointments.Any(a => a.Doctor.Id == doctorId && a.AppointmentDate == date))
            {
                Console.WriteLine("Doctor already has an appointment at this time.");
                return;
            }

            appointments.Add(new Appointment
            {
                AppointmentId = appointmentCounter++,
                Doctor = doctor,
                Patient = patient,
                AppointmentDate = date
            });
            Console.WriteLine("Appointment booked successfully.");
        }

        public void DisplayAllAppointments() // Method to display all appointments
        {
            if (!appointments.Any())
            {
                Console.WriteLine("No appointments found.");
                return;
            }
            foreach (var appt in appointments)
                appt.Display();
        }

        public void SearchAppointmentsByPatientName(string name) // Method to search appointments by patient name
        {
            var found = appointments.Where(a => a.Patient.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!found.Any()) Console.WriteLine("No appointments found for that patient.");
            else foreach (var appt in found) appt.Display();
        }

        public void SearchAppointmentsByDate(DateTime date) // Method to search appointments by date
        {
            var found = appointments.Where(a => a.AppointmentDate.Date == date.Date).ToList();
            if (!found.Any()) Console.WriteLine("No appointments found on that date.");
            else foreach (var appt in found) appt.Display();
        }

        public void ShowAvailableDoctors(string specialization) // Method to show doctors by specialization
        {
            var filtered = doctors.Where(d => d.Specialization.Equals(specialization, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!filtered.Any()) Console.WriteLine("No doctors found with that specialization.");
            else foreach (var doc in filtered) doc.DisplayInfo();
        }

        public void SaveToFiles()
        {
            // save doctors to file
            File.WriteAllLines("doctors.txt", doctors.Select(d =>
                $"{d.Id},{d.Name},{d.Age},{d.Specialization}"));
            // save patients to file
            File.WriteAllLines("patients.txt", patients.Select(p =>
            $"{p.Id},{p.Name},{p.Age},{p.PhoneNumber}"));

            // save appointments to file
            File.WriteAllLines("appointments.txt", appointments.Select(a =>
            $"{a.AppointmentId},{a.Doctor.Id},{a.Patient.Id},{a.AppointmentDate}"));


        }

        public void LoadFromFiles()
        { // Method to load data from files
            if (File.Exists("doctors.txt"))
            {
                foreach (var line in File.ReadAllLines("doctors.txt"))
                {
                    var parts = line.Split(',');
                    AddDoctor(new Doctor
                    {
                        Id = int.Parse(parts[0]),
                        Name = parts[1],
                        Age = int.Parse(parts[2]),
                        Specialization = parts[3]
                    });
                }
            }


        }
    }
}

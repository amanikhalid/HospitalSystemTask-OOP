using System; 
using System.Collections.Generic; 
using System.Linq;
using System.Numerics;
using System.Xml.Linq;

namespace HospitalSystemTask_OOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hospital hospital = new Hospital(); // Initialize the hospital system

            bool exit = false;

            while (!exit) // Main menu loop
            {
                Console.Clear(); // Clear the console for a fresh menu display
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

                switch (choice)
                {
                    case "1":
                        AddDoctor(hospital);
                        break;
                    case "2":
                        AddPatient(hospital);
                        break;
                    case "3":
                        BookAppointment(hospital);
                        break;
                    case "4":
                        hospital.DisplayAllAppointments();
                        break;
                    case "5":
                        Console.Write("Enter patient name: ");
                        string name = Console.ReadLine();
                        hospital.SearchAppointmentsByPatientName(name);
                        break;
                    case "6":
                        Console.Write("Enter appointment date (yyyy-mm-dd): ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                            hospital.SearchAppointmentsByDate(date);
                        else
                            Console.WriteLine("Invalid date format.");
                        break;
                    case "7":
                        Console.Write("Enter specialization: ");
                        string specialization = Console.ReadLine();
                        hospital.ShowAvailableDoctors(specialization);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }

            }
        }

        static void AddDoctor(Hospital hospital) // Method to add a doctor
        {
            Console.WriteLine("Add Doctor");

            int id = GetValidInt("Enter doctor ID: ");
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                Console.ReadLine();
                return;
            }
            if (hospital.DoctorIdExists(id))
            {
                Console.WriteLine("Doctor with this ID already exists.");
                Console.ReadLine();
                return;
            }

            if (id <= 0) // Validate ID
            {
                Console.WriteLine("ID must be a positive number.");
                Console.ReadLine();
                return;
            }
            if (name.Length < 3) // Validate name length
            {
                Console.WriteLine("Name must be at least 3 characters long.");
                Console.ReadLine();
                return;
            }

            int age = GetValidInt("Enter age: ");
            if (age < 0 || age > 120) // Validate age
            {
                Console.WriteLine("Age must be between 0 and 120.");
                Console.ReadLine();
                return;
            }

            Console.Write("Enter specialization: ");
            string spec = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(spec))
            { // Validate specialization
                {
                    Console.WriteLine("Specialization cannot be empty.");
                    Console.ReadLine();
                    return;
                }
            }
            

            Doctor doc = new Doctor { Id = id, Name = name, Age = age, Specialization = spec };
            hospital.AddDoctor(doc);
            Console.WriteLine("Doctor added successfully.");
            Console.ReadLine();
        }


        static void AddPatient(Hospital hospital) // Method to add a patient
        {
            
            Console.WriteLine("Add Patient");

            int id = GetValidInt("Enter patient ID: ");

            if (hospital.DoctorIdExists(id)) // Check if patient ID already exists
            {
                Console.WriteLine("Patient with this ID already exists.");
                Console.ReadLine();
                return;
            }
            if (id <= 0) // Validate ID
            {
                Console.WriteLine("ID must be a positive number.");
                Console.ReadLine();
                return;
            }
            if (id.ToString().Length < 3) // Validate ID length
            {
                Console.WriteLine("ID must be at least 3 digits long.");
                Console.ReadLine();
                return;
            }

            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) // Validate name
            {
                Console.WriteLine("Name cannot be empty.");
                Console.ReadLine();
                return;
            }

            int age = GetValidInt("Enter age: ");
            Console.Write("Enter phone number: ");
            if (age < 0 || age > 120) // Validate age
            {
                Console.WriteLine("Age must be between 0 and 120.");
                Console.ReadLine();
                return;
            }

            string phone = Console.ReadLine();

            Patient pat = new Patient { Id = id, Name = name, Age = age, PhoneNumber = phone };
            hospital.AddPatient(pat);
            Console.WriteLine("Patient added successfully.");
            Console.ReadLine();
        }

        static void BookAppointment(Hospital hospital) // Method to book an appointment
        {
            Console.WriteLine("Book Appointment");

            int docId = GetValidInt("Enter doctor ID: ");
            if (!hospital.DoctorIdExists(docId)) // Check if doctor ID exists
            {
                Console.WriteLine("Doctor with this ID does not exist.");
                Console.ReadLine();
                return;
            }
            if (docId <= 0) // Validate doctor ID
            {
                Console.WriteLine("Doctor ID must be a positive number.");
                Console.ReadLine();
                return;
            }
            
            int patId = GetValidInt("Enter patient ID: ");
            if (!hospital.DoctorIdExists(patId)) // Check if patient ID exists
            {
                Console.WriteLine("Patient with this ID does not exist.");
                Console.ReadLine();
                return;
            }
            if (patId <= 0) // Validate patient ID
            {
                Console.WriteLine("Patient ID must be a positive number.");
                Console.ReadLine();
                return;
            }
            Console.Write("Enter appointment date & time (e.g., 2025-07-23 09:00): ");

            string input = Console.ReadLine();


            if (DateTime.TryParse(input, out DateTime apptDate))
            {
                hospital.BookAppointment(docId, patId, apptDate);
            }
            else
            {
                Console.WriteLine("Invalid date/time format.");
                Console.ReadLine();
            }
        }

        static int GetValidInt(string prompt) // Method to get a valid integer input 
        {
            int val;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out val)) break;
                Console.WriteLine("Invalid input. Please enter a number.");
            }
            return val; // Return the valid integer
        }
    }

    public class Person // Base class for Doctor and Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"ID: {Id}, Name: {Name}, Age: {Age}");
        }

    }

    public class Doctor : Person // Doctor class inheriting from Person
    {
        public string Specialization { get; set; }
        public List<DateTime> AvailableAppointments { get; set; } = new();

        public override void DisplayInfo()
        {
            Console.WriteLine($"Doctor: {Name}, Age: {Age}, Specialization: {Specialization}");
        }
    }

    public class Patient : Person // Patient class inheriting from Person
    {
        public string PhoneNumber { get; set; }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Patient: {Name}, Age: {Age}, Phone: {PhoneNumber}");
        }
    }

    public class Appointment // Appointment class to hold appointment details
    {
        public int AppointmentId { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public DateTime AppointmentDate { get; set; }

        public void Display()
        {
            Console.WriteLine($"ID: {AppointmentId} | {AppointmentDate} | Dr. {Doctor.Name} ({Doctor.Specialization}) with {Patient.Name}");
        }
    }

    public class Hospital 
    {
        // Hospital class to manage doctors, patients, and appointments
        private List<Doctor> doctors = new();
        private List<Patient> patients = new();
        private List<Appointment> appointments = new();
        private int appointmentCounter = 1;


        public void AddDoctor(Doctor doc) => doctors.Add(doc); // Method to add a doctor
        public void AddPatient(Patient pat) => patients.Add(pat); // Method to add a patient

        public void BookAppointment(int doctorId, int patientId, DateTime date)
        {
            var doctor = doctors.FirstOrDefault(d => d.Id == doctorId);
            var patient = patients.FirstOrDefault(p => p.Id == patientId);

            if (doctor == null || patient == null) // Check if doctor and patient exist
            {
                Console.WriteLine("Invalid doctor or patient ID.");
                return;
            }

            bool alreadyBooked = appointments.Any(a => a.Doctor.Id == doctorId && a.AppointmentDate == date);
            if (alreadyBooked) // Check if the doctor already has an appointment at the specified time
            {
                Console.WriteLine("Doctor already has an appointment at this time.");
                return;
            }

            Appointment appt = new Appointment
            {
                AppointmentId = appointmentCounter++,
                Doctor = doctor,
                Patient = patient,
                AppointmentDate = date
            };
            appointments.Add(appt); // Add the appointment to the list
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
            if (!found.Any())
            {
                Console.WriteLine("No appointments found for that patient.");
                return;
            }

            foreach (var appt in found) // Display each appointment found
                appt.Display();
        }


        public void SearchAppointmentsByDate(DateTime date) // Method to search appointments by date
        {
            var found = appointments.Where(a => a.AppointmentDate.Date == date.Date).ToList();
            if (!found.Any())
            {
                Console.WriteLine("No appointments found on that date.");
                return;
            }

            foreach (var appt in found)
                appt.Display();
        }

        public void ShowAvailableDoctors(string specialization) // Method to show doctors by specialization
        {
            var filtered = doctors.Where(d => d.Specialization.Equals(specialization, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!filtered.Any())
            {
                Console.WriteLine("No doctors found with that specialization.");
                return;
            }

            foreach (var doc in filtered)
                doc.DisplayInfo();
        }

        public bool DoctorIdExists(int id)
        {
            return doctors.Any(d => d.Id == id);
        }

    }
}

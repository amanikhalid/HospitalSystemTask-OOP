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
            Hospital hospital = new Hospital();

            Doctor doc1 = new Doctor { Id = 1, Name = "Dr. Fahad", Age = 45, Specialization = "Cardiology" }; 
            Doctor doc2 = new Doctor { Id = 2, Name = "Dr. Sama", Age = 38, Specialization = "Dermatology" };

            Patient pat1 = new Patient { Id = 101, Name = "Amani", Age = 27, PhoneNumber = "92123456" };
            Patient pat2 = new Patient { Id = 102, Name = "Abdullah", Age = 34, PhoneNumber = "93334567" };

            // Adding doctors and patients to the hospital
            hospital.AddDoctor(doc1); 
            hospital.AddDoctor(doc2); 

            // Adding patients to the hospital
            hospital.AddPatient(pat1); 
            hospital.AddPatient(pat2);

            //Booking appointments
            hospital.BookAppointment(1, 101, new DateTime(2025, 7, 23, 9, 0, 0)); 
            hospital.BookAppointment(2, 102, new DateTime(2025, 7, 23, 10, 0, 0));
            hospital.BookAppointment(1, 102, new DateTime(2025, 7, 23, 9, 0, 0));

            // Displaying all appointments
            Console.WriteLine("\nAll Appointments");
            hospital.DisplayAllAppointments();

            // Searching appointments (by patient name)
            Console.WriteLine("\nSearch by Patient Name: Amani");
            hospital.SearchAppointmentsByPatientName("Amani");

            // Searching appointments (by date)
            Console.WriteLine("\nSearch by Date: 2025-07-23");
            hospital.SearchAppointmentsByDate(new DateTime(2025, 7, 23));

            // Displaying available doctors
            Console.WriteLine("\nDoctors with Specialization: Cardiology");
            hospital.ShowAvailableDoctors("Cardiology");
        }

        public class Person
        {
            
            public int id { get; set; }
            public string name { get; set; }
            public int age { get; set; }

            // Constructor
            public int Id { get => id; set => id = value; }
            public string Name { get => name; set => name = value; }
            public int Age { get => age; set => age = value; }

            public virtual void DisplayInfo() 
            {
                Console.WriteLine($"ID: {Id}, Name: {Name}, Age: {Age}");
            }


        }

        public class Doctor : Person
        {
            
            private string specialization;
            private List<DateTime> availableAppointments = new();

            // Constructor
            public string Specialization { get => specialization; set => specialization = value; } 
            public List<DateTime> AvailableAppointments { get => availableAppointments; set => availableAppointments = value; }

            
            public override void DisplayInfo()
            {
                Console.WriteLine($"Doctor: {Name}, Age: {Age}, Specialization: {Specialization}");
            }

        }

        
        public class Hospital : Person 
        {
            private string phoneNumber;

            // Constructor
            public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; } 
            
            
            public override void DisplayInfo() 
            {
                Console.WriteLine($"Patient: {Name}, Age: {Age}, Phone: {PhoneNumber}"); 
            }

        }
        
        public class Appointment 
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

    } 
}

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
            int age = GetValidInt("Enter age: ");
            Console.Write("Enter specialization: ");
            string spec = Console.ReadLine();

            Doctor doc = new Doctor { Id = id, Name = name, Age = age, Specialization = spec };
            hospital.AddDoctor(doc);
            Console.WriteLine("Doctor added successfully.");
        }


        static void AddPatient(Hospital hospital) // Method to add a patient
        {
            Console.WriteLine("Add Patient");

            int id = GetValidInt("Enter patient ID: ");
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            int age = GetValidInt("Enter age: ");
            Console.Write("Enter phone number: ");
            string phone = Console.ReadLine();

            Patient pat = new Patient { Id = id, Name = name, Age = age, PhoneNumber = phone };
            hospital.AddPatient(pat);
            Console.WriteLine("Patient added successfully.");
        }

        static void BookAppointment(Hospital hospital) // Method to book an appointment
        {
            Console.WriteLine("Book Appointment");

            int docId = GetValidInt("Enter doctor ID: ");
            int patId = GetValidInt("Enter patient ID: ");
            Console.Write("Enter appointment date & time (e.g., 2025-07-23 09:00): ");
            string input = Console.ReadLine();

            if (DateTime.TryParse(input, out DateTime apptDate))
            {
                hospital.BookAppointment(docId, patId, apptDate);
            }
            else
            {
                Console.WriteLine("Invalid date/time format.");
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
}

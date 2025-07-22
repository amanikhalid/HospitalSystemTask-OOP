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

            bool exit = false;

            while (!exit)
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
    }
}

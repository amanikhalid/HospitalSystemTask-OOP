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



            }
        }
}

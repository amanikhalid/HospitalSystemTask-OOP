using System; 
using System.Collections.Generic; 
using System.Linq;
using System.Numerics;

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

            hospital.AddDoctor(doc1); //first doctor
            hospital.AddDoctor(doc2); // second doctor

            hospital.AddPatient(pat1); // first patient
            hospital.AddPatient(pat2); // second patient
        }

    }
}

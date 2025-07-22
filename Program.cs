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

        }
    }
}

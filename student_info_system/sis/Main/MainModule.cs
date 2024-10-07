using sis.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sis.Main
{
    internal class MainModule
    {
        private readonly HospitalService _service;

        public MainModule(HospitalService service)
        {
            _service = service;
        }

        public void ShowMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n--- Student Information System Menu ---");
                Console.WriteLine("1. Enroll Student in Course");
                Console.WriteLine("2. Assign Teacher to Course");
                Console.WriteLine("3. Record Payment");
                Console.WriteLine("4. Generate Enrollment Report");
                Console.WriteLine("5. Generate Payment Report");
                Console.WriteLine("6. Calculate Course Statistics");
                Console.WriteLine("7. Get Enrollments for Student");
                Console.WriteLine("8. Get Courses for Teacher");
                Console.WriteLine("9. Add Student");

                Console.WriteLine("10. Exit");
                Console.WriteLine("Choose an option:");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _service.EnrollStudent();
                        break;
                    case "2":
                        _service.AssignTeacher();
                        break;
                    case "3":
                        _service.RecordPayment();
                        break;
                    case "4":
                        _service.GenerateEnrollmentReport();
                        break;
                    case "5":
                        _service.GeneratePaymentReport();
                        break;
                    case "6":
                        _service.CalculateCourseStatistics();
                        break;
                    case "7":
                        _service.GetEnrollmentsForStudent();
                        break;
                    case "8":
                        _service.GetCoursesForTeacher();
                        break;
                    case "9":
                        _service.AddNewStudent();
                        break;
                    case "10":
                        exit = true;
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }
    }
}

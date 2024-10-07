using sis.model;
using sis.model.Exceptions;
using sis.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace sis.Service
{
    internal class HospitalService
    {
 
            private readonly isis _sisRepository;

            public HospitalService(isis sisRepository)
            {
                _sisRepository = sisRepository;
            }

            public void EnrollStudent()
            {
             try
             {


                Console.WriteLine("Enter Student ID:");
                int studentId = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter Course ID:");
                int courseId = int.Parse(Console.ReadLine());

                _sisRepository.EnrollStudentInCourse(studentId, courseId);
             }
             catch (StudentNotFoundException ex)
             {
                Console.WriteLine(ex.Message);  
             }
             catch (CourseNotFoundException ex)
             {
                Console.WriteLine(ex.Message);  
             }
             catch (Exception ex)
             {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);  
             }
            }

            public void AssignTeacher()
            {
               try
               {

                  Console.WriteLine("Enter Teacher ID:");
                  int teacherId = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter Course ID:");
                int courseId = int.Parse(Console.ReadLine());

                _sisRepository.AssignTeacherToCourse(teacherId, courseId);

               }

                catch (TeacherNotFoundException ex)
                {
                Console.WriteLine(ex.Message);  
                
                }
                catch (CourseNotFoundException ex)
                {
                Console.WriteLine(ex.Message);  
                }
                catch (Exception ex)
                {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);  
                }
            }

            public void RecordPayment()
            {
            try
            {


                Console.WriteLine("Enter Student ID:");
                int studentId = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter Payment Amount:");
                int amount = int.Parse(Console.ReadLine());

                _sisRepository.RecordPayment(studentId, amount);
            }

            catch (StudentNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }

            public void GenerateEnrollmentReport()
            {
            try
            {

                Console.WriteLine("Enter Course ID:");
                int courseId = int.Parse(Console.ReadLine());

                _sisRepository.GenerateEnrollmentReport(courseId);
            }
            
            catch (CourseNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }

            public void GeneratePaymentReport()
            {
            try
            {


                Console.WriteLine("Enter Student ID:");
                int studentId = int.Parse(Console.ReadLine());

                _sisRepository.GeneratePaymentReport(studentId);
            }
            catch (StudentNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }

            public void CalculateCourseStatistics()
            {
            try
            {


                Console.WriteLine("Enter Course ID:");
                int courseId = int.Parse(Console.ReadLine());

                _sisRepository.CalculateCourseStatistics(courseId);
            }
            catch (CourseNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }





        public void GetEnrollmentsForStudent()
        {
            try
            {


                Console.WriteLine("Enter Student ID:");
                int studentId = int.Parse(Console.ReadLine());

                var enrollments = _sisRepository.GetEnrollmentsForStudent(studentId);
                Console.WriteLine($"Enrollments for Student ID: {studentId}");
                foreach (var enrollment in enrollments)
                {
                    Console.WriteLine($"- Course ID: {enrollment.Course.CourseId}");
                }
            }
            catch (StudentNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }

        public void GetCoursesForTeacher()
        {
            try
            {


                Console.WriteLine("Enter Teacher ID:");
                int teacherId = int.Parse(Console.ReadLine());

                var courses = _sisRepository.GetCoursesForTeacher(teacherId);
                Console.WriteLine($"Courses for Teacher ID: {teacherId}");
                foreach (var course in courses)
                {
                    Console.WriteLine($"- Course ID: {course.CourseId}, Name: {course.CourseName}");
                }
            }
            catch (TeacherNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }
        public void AddNewStudent()
        {
            // Collect user input for student details
            Console.WriteLine("Enter First Name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter Last Name:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter Date of Birth (yyyy-mm-dd):");
            DateTime dateOfBirth;
            while (!DateTime.TryParse(Console.ReadLine(), out dateOfBirth))
            {
                Console.WriteLine("Invalid date. Please enter Date of Birth (yyyy-mm-dd):");
            }

            Console.WriteLine("Enter Email:");
            string email = Console.ReadLine();

            Console.WriteLine("Enter Phone Number:");
            string phoneNumber = Console.ReadLine();

            // Create a new Student object
            Student newStudent = new Student(firstName, lastName, dateOfBirth, email, phoneNumber);

            // Call the SISRepository to add the student
            _sisRepository.AddStudent(newStudent);

            Console.WriteLine("Student successfully added.");
        }

    }
}




using sis.model;
using sis.model.Exceptions;
using sis.Repository.Interfaces;
using sis.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sis.Repository
{
    internal class sisRepo:isis
    {
        private readonly SIS _sis;

        public sisRepo(SIS sis)
        {
            _sis = sis;
        }

        public void EnrollStudentInCourse(int studentId, int courseId)
        {
            if (!StudentExists(studentId))
            {
                throw new StudentNotFoundException();  
            }

            
            if (!CourseExists(courseId))
            {
                throw new CourseNotFoundException(); 
            }
            _sis.EnrollmentRepository.EnrollStudentInCourse(studentId, courseId);
            Console.WriteLine($"Student with ID {studentId} enrolled in course with ID {courseId}.");
        }

        public void AssignTeacherToCourse(int teacherId, int courseId)
        {
            
            if (!TeacherExists(teacherId))
            {
                throw new TeacherNotFoundException();  
            }
            if(!CourseExists(courseId))
            {
                throw new CourseNotFoundException();
            }
            _sis.CourseRepository.AssignTeacherToCourse(courseId, teacherId);
            Console.WriteLine($"Teacher with ID {teacherId} assigned to course with ID {courseId}.");
        }

        public void RecordPayment(int studentId, int amount)
        {
            if (!StudentExists(studentId))
            {
                throw new StudentNotFoundException();  
            }

            _sis.PaymentRepository.AddPayment(studentId, amount, DateTime.Now);
            Console.WriteLine($"Payment of {amount} recorded for student with ID {studentId}.");
        }

        public void GenerateEnrollmentReport(int courseId)
        {
           
            if (!CourseExists(courseId))
            {
                throw new CourseNotFoundException();  
            }
            var enrollments = _sis.EnrollmentRepository.GetEnrollmentsForCourse(courseId); 
            Console.WriteLine($"Enrollment Report for Course ID: {courseId}");
            foreach (var enrollment in enrollments)
            {
                Console.WriteLine($"- {enrollment.Student.FirstName} {enrollment.Student.LastName}"); 
            }
        }


        public void GeneratePaymentReport(int studentId)
        {
            if (!StudentExists(studentId))
            {
                throw new StudentNotFoundException(); 
            }

           
            var payments = _sis.PaymentRepository.GetPaymentHistoryForStudent(studentId);
            Console.WriteLine($"Payment Report for Student ID: {studentId}");
            foreach (var payment in payments)
            {
                Console.WriteLine($"- Amount: {payment.Amount}, Date: {payment.PaymentDate}");
            }
        }

        public void CalculateCourseStatistics(int courseId)
        {
           
            if (!CourseExists(courseId))
            {
                throw new CourseNotFoundException();  
            }
            var enrollments = _sis.EnrollmentRepository.GetEnrollmentsForCourse(courseId);

            Console.WriteLine($"Course Statistics for Course ID: {courseId}");
            Console.WriteLine($"Number of Students Enrolled: {enrollments.Count}");

            decimal totalPayments = 0;

            foreach (var enrollment in enrollments)
            {
               
                var payments = _sis.PaymentRepository.GetPaymentHistoryForStudent(enrollment.Student.StudentId);

                foreach (var payment in payments)
                {
                    totalPayments += payment.Amount;
                }
            }

            
            Console.WriteLine($"Total Payments Received: {totalPayments:C}");
        }

       


        public List<Enrollment> GetEnrollmentsForStudent(int studentId)
        {
            if (!StudentExists(studentId))
            {
                throw new StudentNotFoundException();
            }
            return _sis.EnrollmentRepository.GetEnrollmentsForStudent(studentId);
        }

        public List<Course> GetCoursesForTeacher(int teacherId)
        {
            if (!TeacherExists(teacherId))
            {
                throw new TeacherNotFoundException();
            }
            return _sis.TeacherRepository.GetCoursesForTeacher(teacherId);
        }


        public bool StudentExists(int studentId)
        {
            bool exists = false;
            string query = "SELECT COUNT(*) FROM Student WHERE student_id = @StudentId";

            SqlConnection connection = new SqlConnection(DbConnUtil.GetConnString());
            
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentId", studentId);

                connection.Open();
                int count = (int)command.ExecuteScalar();  
                exists = count > 0;
            connection.Close();


            return exists;
        }
        public bool CourseExists(int courseId)
        {
            bool exists = false;
            string query = "SELECT COUNT(*) FROM Course WHERE course_id = @CourseId";

            SqlConnection connection = new SqlConnection(DbConnUtil.GetConnString());
            
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CourseId", courseId);

                connection.Open();
                int count = (int)command.ExecuteScalar();  
                exists = count > 0;
            connection.Close();


            return exists;
        }
        public bool EnrollmentExists(int studentId, int courseId)
        {
            bool exists = false;
            string query = "SELECT COUNT(*) FROM Enrollment WHERE student_id = @StudentId AND course_id = @CourseId";

            SqlConnection connection = new SqlConnection(DbConnUtil.GetConnString());
            
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentId", studentId);
                command.Parameters.AddWithValue("@CourseId", courseId);

                connection.Open();
                int count = (int)command.ExecuteScalar();  
                exists = count > 0;
            connection.Close();


            return exists;
        }
        public bool TeacherExists(int teacherId)
        {
            bool exists = false;
            string query = "SELECT COUNT(*) FROM Teacher WHERE teacher_id = @TeacherId";

            SqlConnection connection = new SqlConnection(DbConnUtil.GetConnString());
            
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TeacherId", teacherId);

                connection.Open();
                int count = (int)command.ExecuteScalar(); 
                exists = count > 0;
                connection.Close();
            

            return exists;
        }
        public void AddStudent(Student student)
        {
            string query = "INSERT INTO Student (first_name,last_name,date_of_birth,email,phone_number) " +
                           "VALUES (@FirstName, @LastName, @DateOfBirth, @Email, @PhoneNumber)";

            using (SqlConnection connection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                command.Parameters.AddWithValue("@Email", student.Email);
                command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);

                connection.Open();
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    Console.WriteLine("Student added successfully.");
                }
                else
                {
                    Console.WriteLine("Error adding student.");
                }
            }
        }





    }
}

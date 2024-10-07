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
    public class StudentRepo: IStudent
    {
        private readonly string _connectionString;

        public StudentRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void EnrollInCourse(int studentId, int courseId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Enrollment (student_id,course_id, enrollment_date) VALUES (@StudentId, @CourseId, @EnrollmentDate)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentId", studentId);
                command.Parameters.AddWithValue("@CourseId", courseId);
                command.Parameters.AddWithValue("@EnrollmentDate", DateTime.Now);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateStudentInfo(int studentId, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Student SET first_name = @FirstName, last_name = @LastName, date_of_birth = @DateOfBirth, " +
                               "email = @Email, phone_number = @PhoneNumber WHERE student_id= @StudentId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                command.Parameters.AddWithValue("@StudentId", studentId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void MakePayment(int studentId, int amount, DateTime paymentDate)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Payment (student_id, amount, payment_date) VALUES (@StudentId, @Amount, @PaymentDate)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentId", studentId);
                command.Parameters.AddWithValue("@Amount", amount);
                command.Parameters.AddWithValue("@PaymentDate", paymentDate);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DisplayStudentInfo(int studentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Student WHERE student_id = @StudentId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentId", studentId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["student_id"]}, Name: {reader["first_name"]} {reader["last_name"]}, " +
                        $"DOB: {reader["date_of_birth"]}, Email: {reader["email"]}, Phone: {reader["phone_number"]}");
                }
            }
        }

        public List<Course> GetEnrolledCourses(int studentId)
        {
            List<Course> courses = new List<Course>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT c.course_id , c.course_name FROM Course c " +
                               "JOIN Enrollment e ON c.course_id = e.course_id WHERE e.student_id = @StudentId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentId", studentId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    courses.Add(new Course(
                        (int)reader["course_id"],
                        (string)reader["course_name"],
                        (string)reader["course_code"],
                        (string)reader["InstructorName"]
                    ));
                }
            }
            return courses;
        }

        public List<Payment> GetPaymentHistory(int studentId)
        {
            List<Payment> payments = new List<Payment>();

            // Retrieve the student object based on studentId
            Student student = GetStudentById(studentId);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return payments;
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Payment WHERE student_id = @StudentId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentId", studentId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    payments.Add(new Payment(
                        (int)reader["payment_id"],
                        student,  
                        (int)reader["amount"],
                        (DateTime)reader["payment_date"]
                    ));
                }
            }
            return payments;
        }
        public Student GetStudentById(int studentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Student WHERE student_id = @StudentId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentId", studentId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Student(
                        (int)reader["student_id"],
                        (string)reader["first_name"],
                        (string)reader["last_name"],
                        (DateTime)reader["date_of_birth"],
                        (string)reader["email"],
                        (string)reader["phone_number"]
                    );
                }
            }
            return null; 
        }
        


    }
}

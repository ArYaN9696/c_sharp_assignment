using sis.model;
using sis.model.Exceptions;
using sis.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sis.Repository
{
    internal class EnrollmentRepo:IEnrollment
    {
        private readonly string _connectionString;

        public EnrollmentRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        // File: EnrollmentRepository.cs
        public List<Enrollment> GetEnrollmentsForCourse(int courseId)
        {
            List<Enrollment> enrollments = new List<Enrollment>();
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT * FROM Enrollment WHERE course_id = @CourseId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CourseId", courseId);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int enrollmentId = (int)reader["enrollment_id"];
                int studentId = (int)reader["student_id"];
                DateTime enrollmentDate = (DateTime)reader["enrollment_date"];

               
                Student student = new StudentRepo(_connectionString).GetStudentById(studentId);
                Course course = new CourseRepo(_connectionString).GetCourseById(courseId);

                enrollments.Add(new Enrollment(enrollmentId, student, course, enrollmentDate));
            }

            reader.Close();
            connection.Close();
            return enrollments;
        }

        public void EnrollStudentInCourse(int studentId, int courseId)
        {
          
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "INSERT INTO Enrollment (student_id, course_id, enrollment_date) VALUES (@StudentId, @CourseId, @EnrollmentDate)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@StudentId", studentId);
            command.Parameters.AddWithValue("@CourseId", courseId);
            command.Parameters.AddWithValue("@EnrollmentDate", DateTime.Now);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdateEnrollment(int enrollmentId, int studentId, int courseId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "UPDATE Enrollment SET student_id = @StudentId, course_id = @CourseId WHERE enrollment_id = @EnrollmentId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@EnrollmentId", enrollmentId);
            command.Parameters.AddWithValue("@StudentId", studentId);
            command.Parameters.AddWithValue("@CourseId", courseId);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Enrollment GetEnrollmentById(int enrollmentId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT * FROM Enrollment WHERE enrollment_id = @EnrollmentId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@EnrollmentId", enrollmentId);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            Enrollment enrollment = null;
            if (reader.Read())
            {
                int studentId = (int)reader["student_id"];
                int courseId = (int)reader["course_id"];
                DateTime enrollmentDate = (DateTime)reader["enrollment_date"];

                // Fetch the full Student and Course objects here
                Student student = new StudentRepo(_connectionString).GetStudentById(studentId);
                Course course = new CourseRepo(_connectionString).GetCourseById(courseId);

                enrollment = new Enrollment(enrollmentId, student, course, enrollmentDate);
            }

            reader.Close();
            connection.Close();
            return enrollment; // Return the enrollment or null if not found
        }

        public List<Enrollment> GetEnrollmentsForStudent(int studentId)
        {
            List<Enrollment> enrollments = new List<Enrollment>();
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT * FROM Enrollment WHERE student_id = @StudentId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@StudentId", studentId);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int enrollmentId = (int)reader["enrollment_id"];
                int courseId = (int)reader["course_id"];
                DateTime enrollmentDate = (DateTime)reader["enrollment_date"];

                // Fetch the full Course object
                Course course = new CourseRepo(_connectionString).GetCourseById(courseId);
                Enrollment enrollment = new Enrollment(enrollmentId, new Student { StudentId = studentId }, course, enrollmentDate);
                enrollments.Add(enrollment);
            }

            reader.Close();
            connection.Close();
            return enrollments;
        }

        public List<Enrollment> GetAllEnrollments()
        {
            List<Enrollment> enrollments = new List<Enrollment>();
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT * FROM Enrollment";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int enrollmentId = (int)reader["enrollment_id"];
                int studentId = (int)reader["student_id"];
                int courseId = (int)reader["course_id"];
                DateTime enrollmentDate = (DateTime)reader["enrollment_date"];

                // Fetch the Student and Course objects
                Student student = new StudentRepo(_connectionString).GetStudentById(studentId);
                Course course = new CourseRepo(_connectionString).GetCourseById(courseId);

                Enrollment enrollment = new Enrollment(enrollmentId, student, course, enrollmentDate);
                enrollments.Add(enrollment);
            }

            reader.Close();
            connection.Close();
            return enrollments;
        }

        public Course GetCourse(int enrollmentId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT CourseId FROM Enrollment WHERE enrollment_id = @EnrollmentId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@EnrollmentId", enrollmentId);

            connection.Open();
            int courseId = (int)command.ExecuteScalar(); 
            connection.Close();

            return new CourseRepo(_connectionString).GetCourseById(courseId);
        }
    }
}

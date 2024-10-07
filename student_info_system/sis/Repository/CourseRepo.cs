using sis.model;
using System;
using sis.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sis.model.Exceptions;
using System.Data.SqlClient;

namespace sis.Repository
{
    internal class CourseRepo:ICourse
    {
        private readonly string _connectionString;

        public CourseRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddCourse(string courseName, string courseCode, string instructorName)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "INSERT INTO Course (course_name, course_code, instructor_name) VALUES (@CourseName, @CourseCode, @InstructorName)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CourseName", courseName);
            command.Parameters.AddWithValue("@CourseCode", courseCode);
            command.Parameters.AddWithValue("@InstructorName", instructorName);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdateCourse(int courseId, string courseName, string courseCode, string instructorName)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "UPDATE Course SET course_name = @CourseName, CourseCode = @CourseCode, instructor_name = @InstructorName WHERE course_id = @CourseId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CourseName", courseName);
            command.Parameters.AddWithValue("@CourseCode", courseCode);
            command.Parameters.AddWithValue("@InstructorName", instructorName);
            command.Parameters.AddWithValue("@CourseId", courseId);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Course GetCourseById(int courseId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT * FROM Course WHERE course_id = @CourseId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CourseId", courseId);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            Course course = null;
            if (reader.Read())
            {
                course = new Course(
                    (int)reader["course_id"],
                    (string)reader["course_name"],
                    (string)reader["course_code"],
                    (string)reader["instructor_name"]
                );
            }

            reader.Close();
            connection.Close();
            return course; // Return the course or null if not found
        }

        public List<Course> GetAllCourses()
        {
            List<Course> courses = new List<Course>();
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT * FROM Course";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                courses.Add(new Course(
                    (int)reader["course_id"],
                    (string)reader["course_name"],
                    (string)reader["course_code"],
                    (string)reader["instructor_name"]
                ));
            }

            reader.Close();
            connection.Close();
            return courses;
        }

        public void AssignTeacherToCourse(int courseId, int teacherId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "UPDATE Course SET teacher_id = @TeacherId WHERE course_id = @CourseId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CourseId", courseId);
            command.Parameters.AddWithValue("@TeacherId", teacherId);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<Student> GetStudentsEnrolledInCourse(int courseId)
        {
            List<Student> students = new List<Student>();
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT s.* FROM Student s " +
                           "JOIN Enrollment e ON s.student_id = e.student_id WHERE e.course_id = @CourseId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CourseId", courseId);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                students.Add(new Student(
                    (int)reader["student_id"],
                    (string)reader["first_name"],
                    (string)reader["last_name"],
                    (DateTime)reader["date_of_birth"],
                    (string)reader["email"],
                    (string)reader["phone_number"]
                ));
            }

            reader.Close();
            connection.Close();
            return students;
        }

        public string GetTeacher(int courseId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT instructor_name FROM Course WHERE course_id = @CourseId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CourseId", courseId);

            connection.Open();
            string instructorName = (string)command.ExecuteScalar(); // Gets the first column of the first row
            connection.Close();
            return instructorName; // Return the instructor name or null if not found
        }
    }
}

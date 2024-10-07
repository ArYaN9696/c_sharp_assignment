using sis.model;
using System;
using System.Collections.Generic;
using System.Linq;
using sis.Repository.Interfaces;
using System.Text;
using System.Threading.Tasks;
using sis.model.Exceptions;
using System.Data.SqlClient;

namespace sis.Repository
{
    internal class TeacherRepo:ITeacher
    {
        private readonly string _connectionString;

        public TeacherRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddTeacher(string firstName, string lastName, string email)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "INSERT INTO Teacher (first_name, last_name, email) VALUES (@FirstName, @LastName, @Email)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@Email", email);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdateTeacher(int teacherId, string firstName, string lastName, string email)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "UPDATE Teacher SET first_name = @FirstName, last_name = @LastName, email = @Email WHERE teacher_id = @TeacherId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@TeacherId", teacherId);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Teacher GetTeacherById(int teacherId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT * FROM Teacher WHERE teacher_id = @TeacherId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TeacherId", teacherId);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            Teacher teacher = null;
            if (reader.Read())
            {
                teacher = new Teacher(
                    (int)reader["teacher_id"],
                    (string)reader["first_name"],
                    (string)reader["last_name"],
                    (string)reader["email"]
                );
            }

            reader.Close();
            connection.Close();
            return teacher; // Return the teacher or null if not found
        }

        public List<Teacher> GetAllTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT * FROM Teacher";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                teachers.Add(new Teacher(
                    (int)reader["teacher_id"],
                    (string)reader["first_name"],
                    (string)reader["last_name"],
                    (string)reader["email"]
                ));
            }

            reader.Close();
            connection.Close();
            return teachers;
        }

        public List<Course> GetCoursesForTeacher(int teacherId)
        {
            List<Course> courses = new List<Course>();
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT * FROM Course WHERE teacher_id = @TeacherId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TeacherId", teacherId);

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
    }
}

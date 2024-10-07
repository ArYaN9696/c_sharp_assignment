using sis.model;
using System;
using System.Collections.Generic;
using sis.Repository.Interfaces;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sis.model.Exceptions;
using System.Data.SqlClient;

namespace sis.Repository
{
    internal class PaymentRepo:IPayment
    {
        private readonly string _connectionString;

        public PaymentRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddPayment(int studentId, int amount, DateTime paymentDate)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "INSERT INTO Payment (student_id, amount, payment_date) VALUES (@StudentId, @Amount, @PaymentDate)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@StudentId", studentId);
            command.Parameters.AddWithValue("@Amount", amount);
            command.Parameters.AddWithValue("@PaymentDate", paymentDate);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }


        public Payment GetPaymentById(int paymentId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT * FROM Payment WHERE payment_id = @PaymentId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PaymentId", paymentId);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            Payment payment = null;
            if (reader.Read())
            {
                int studentId = (int)reader["student_id"];
                int amount = (int)reader["amount"];
                DateTime paymentDate = (DateTime)reader["payment_date"];

                // Fetch the full Student object
                Student student = new StudentRepo(_connectionString).GetStudentById(studentId);
                payment = new Payment(paymentId, student, amount, paymentDate); // Pass the Student object
            }

            reader.Close();
            connection.Close();
            return payment;
        }

        public List<Payment> GetPaymentHistoryForStudent(int studentId)
        {
            List<Payment> payments = new List<Payment>();
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT * FROM Payment WHERE student_id = @StudentId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@StudentId", studentId);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int paymentId = (int)reader["payment_id"];
                int amount = (int)reader["amount"];
                DateTime paymentDate = (DateTime)reader["payment_date"];

                Student student = new StudentRepo(_connectionString).GetStudentById(studentId);
                payments.Add(new Payment(paymentId, student, amount, paymentDate));
            }

            reader.Close();
            connection.Close();
            return payments;
        }

        public List<Payment> GetAllPayments()
        {
            List<Payment> payments = new List<Payment>();
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT * FROM Payment";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int paymentId = (int)reader["payment_id"];
                int studentId = (int)reader["student_id"];
                int amount = (int)reader["amount"];
                DateTime paymentDate = (DateTime)reader["payment_date"];

                Student student = new StudentRepo(_connectionString).GetStudentById(studentId);
                payments.Add(new Payment(paymentId, student, amount, paymentDate));
            }

            reader.Close();
            connection.Close();
            return payments;
        }

        // Retrieve the student associated with the payment
        public int GetStudent(int paymentId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT student_id FROM Payment WHERE payment_id = @PaymentId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PaymentId", paymentId);

            connection.Open();
            int studentId = (int)command.ExecuteScalar(); // Gets the StudentId from the Payments table
            connection.Close();
            return studentId; // Return the student ID
        }

        // Retrieve the payment amount
        public decimal GetPaymentAmount(int paymentId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT amount FROM Payment WHERE payment_id = @PaymentId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PaymentId", paymentId);

            connection.Open();
            decimal amount = (decimal)command.ExecuteScalar(); // Gets the Amount from the Payments table
            connection.Close();
            return amount; // Return the payment amount
        }

        // Retrieve the payment date
        public DateTime GetPaymentDate(int paymentId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "SELECT PaymentDate FROM Payment WHERE payment_id = @PaymentId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PaymentId", paymentId);

            connection.Open();
            DateTime paymentDate = (DateTime)command.ExecuteScalar(); // Gets the PaymentDate from the Payments table
            connection.Close();
            return paymentDate; // Return the payment date
        }
    }
}

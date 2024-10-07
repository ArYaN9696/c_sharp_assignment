using sis.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sis.Repository.Interfaces
{
    public interface IStudent
    {
        void EnrollInCourse(int studentId, int courseId);
        void UpdateStudentInfo(int studentId, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber);
        void MakePayment(int studentId, int amount, DateTime paymentDate);
        void DisplayStudentInfo(int studentId);
        List<Course> GetEnrolledCourses(int studentId);
        List<Payment> GetPaymentHistory(int studentId);

    }
}

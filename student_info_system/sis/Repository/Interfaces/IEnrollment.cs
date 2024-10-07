using sis.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sis.Repository.Interfaces
{
    public interface IEnrollment
    {
        void EnrollStudentInCourse(int studentId, int courseId);
        void UpdateEnrollment(int enrollmentId, int studentId, int courseId);
        Enrollment GetEnrollmentById(int enrollmentId);
        List<Enrollment> GetEnrollmentsForCourse(int courseId);
        List<Enrollment> GetEnrollmentsForStudent(int studentId);
        List<Enrollment> GetAllEnrollments();
    }
}

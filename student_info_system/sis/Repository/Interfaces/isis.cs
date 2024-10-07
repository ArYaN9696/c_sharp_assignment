using sis.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sis.Repository.Interfaces
{
    internal interface isis
    {
        void EnrollStudentInCourse(int studentId, int courseId);
        void AssignTeacherToCourse(int teacherId, int courseId);
        void RecordPayment(int studentId, int amount);
        void GenerateEnrollmentReport(int courseId);
        void GeneratePaymentReport(int studentId);
        void CalculateCourseStatistics(int courseId);
      
        List<Enrollment> GetEnrollmentsForStudent(int studentId);
        List<Course> GetCoursesForTeacher(int teacherId);
        void AddStudent(Student student);

    }
}

using sis.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sis.Repository.Interfaces
{
    public interface ICourse
    {
        void AddCourse(string courseName, string courseCode, string instructorName);
        void UpdateCourse(int courseId, string courseName, string courseCode, string instructorName);
        Course GetCourseById(int courseId);
        List<Course> GetAllCourses();
        void AssignTeacherToCourse(int courseId, int teacherId);
        List<Student> GetStudentsEnrolledInCourse(int courseId);

        string GetTeacher(int courseId);
    }
}

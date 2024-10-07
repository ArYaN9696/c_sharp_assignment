using sis.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sis.Repository.Interfaces
{
    public interface ITeacher
    {
        void AddTeacher(string firstName, string lastName, string email);
        void UpdateTeacher(int teacherId, string firstName, string lastName, string email);
        Teacher GetTeacherById(int teacherId);
        List<Teacher> GetAllTeachers();
        List<Course> GetCoursesForTeacher(int teacherId);
    }
}

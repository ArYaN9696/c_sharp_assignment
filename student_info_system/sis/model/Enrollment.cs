using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sis.model
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public Student Student { get; set; }  
        public Course Course { get; set; }     
        public DateTime EnrollmentDate { get; set; }

        // Constructor to initialize attributes
        public Enrollment(int enrollmentId, Student student, Course course, DateTime enrollmentDate)
        {
            EnrollmentId = enrollmentId;
            Student = student;
            Course = course;
            EnrollmentDate = enrollmentDate;
        }


    }
}

using sis.Repository;
using sis.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace sis.model
{
    internal class SIS
    {
        public IStudent StudentRepository { get; }
        public ICourse CourseRepository { get; }
        public IEnrollment EnrollmentRepository { get; }
        public ITeacher TeacherRepository { get; }
        public IPayment PaymentRepository { get; }

        // Constructor to initialize repositories
        public SIS(string connectionString)
        {
            StudentRepository = new StudentRepo(connectionString);
            CourseRepository = new CourseRepo(connectionString);
            EnrollmentRepository = new EnrollmentRepo(connectionString);
            TeacherRepository = new TeacherRepo(connectionString);
            PaymentRepository = new PaymentRepo(connectionString);
        }



       
    }
}

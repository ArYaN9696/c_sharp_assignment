using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sis.model.Exceptions
{
    internal class TeacherNotFoundException:Exception
    {
        public TeacherNotFoundException()
        : base("The specified teacher was not found.") { }
    }
}

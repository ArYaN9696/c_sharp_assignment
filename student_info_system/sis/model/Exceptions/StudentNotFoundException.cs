using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sis.model.Exceptions
{
    internal class StudentNotFoundException:Exception
    {
        public StudentNotFoundException()
        : base("The specified student was not found.") { }
    }
}

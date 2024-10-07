using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sis.model.Exceptions
{
    internal class CourseNotFoundException:Exception
    {
        public CourseNotFoundException()
        : base("The specified course was not found.") { }
    }
}

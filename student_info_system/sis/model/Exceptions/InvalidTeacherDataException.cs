using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sis.model.Exceptions
{
    internal class InvalidTeacherDataException:Exception
    {
        public InvalidTeacherDataException(string message)
        : base(message) { }
    }
}

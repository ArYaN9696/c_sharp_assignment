using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sis.model.Exceptions
{
    internal class InvalidEnrollmentDataException:Exception
    {
        public InvalidEnrollmentDataException(string message)
      : base(message) { }
    }
}

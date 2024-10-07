using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sis.model.Exceptions
{
    internal class PaymentValidationException:Exception
    {
        public PaymentValidationException(string message)
        : base(message) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sis.model.Exceptions
{
    internal class InsufficientFundsException:Exception
    {
        public InsufficientFundsException()
       : base("The student does not have enough funds to make this payment.") { }
    }
}

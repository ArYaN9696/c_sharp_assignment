using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sis.model
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public Student Student { get; set; }  
        public int Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        // Constructor to initialize attributes
        public Payment(int paymentId, Student student, int amount, DateTime paymentDate)
        {
            PaymentId = paymentId;
            Student = student;
            Amount = amount;
            PaymentDate = paymentDate;
        }


    }
}

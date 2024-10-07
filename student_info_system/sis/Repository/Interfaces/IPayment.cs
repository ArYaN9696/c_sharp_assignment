using sis.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sis.Repository.Interfaces
{
    public interface IPayment
    {
        void AddPayment(int studentId, int amount, DateTime paymentDate);
        Payment GetPaymentById(int paymentId);
        List<Payment> GetPaymentHistoryForStudent(int studentId);
        List<Payment> GetAllPayments();
        int GetStudent(int paymentId); 
        decimal GetPaymentAmount(int paymentId); 
        DateTime GetPaymentDate(int paymentId);
    }
}

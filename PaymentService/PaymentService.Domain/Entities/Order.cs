using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Domain.Entities
{
    public class Order
    {
        public Order(double amount,Guid orderId)
        {
            Amount = amount;
            Id = orderId;
        }

        public Guid Id { get;private set; }
        public double Amount { get; private set; }
        public ICollection<Payment> Payments { get; private set; }
        public void SetAmount(double amount) => Amount = amount;
    }
}

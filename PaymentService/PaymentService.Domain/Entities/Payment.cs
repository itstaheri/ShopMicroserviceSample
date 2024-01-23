using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Domain.Entities
{
    public class Payment
    {
        public Payment(double amount, Guid orderId)
        {
            OrderId = orderId;
            Amount = amount;
            DatePay = DateTime.Now;
            IsPay = false;
        }
        public void ChangeToPay() => IsPay = true;
        public void PayDone(bool isPay,long refId,DateTime datePay,string authority)
        {
            IsPay = isPay;
            RefId = refId;
            DatePay = datePay;
            Authority = authority;
        }

        public Guid Id { get;private set; }
        public double Amount { get; private set; }
        public bool IsPay { get; private set; }
        public DateTime DatePay { get; private set; }
        public string Authority { get; private set; }
        public long RefId { get; private set; }
        public Guid OrderId { get; private set; }
        public Order Order { get; private set; }
    }
}

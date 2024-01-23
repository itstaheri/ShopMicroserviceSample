using System;

namespace PaymentService.Application.Services
{
    public class PaymentDto
    {
        public Guid PaymentId { get; set; }
        public Guid OrderId { get; set; }
        public double Amount { get; set; }
        public bool IsPay { get; set; }
    }
}

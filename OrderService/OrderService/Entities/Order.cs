using OrderService.Entities.Enums;

namespace OrderService.Entities
{
    public class Order : BaseEntity
    {
        public Order()
        {

        }
        public Order(Guid userId, string code, double payAmount)
        {
            UserId = userId;
            Code = code;  
            PaymentStatus = PaymentStatus.UnPaid;
            PayAmount = payAmount;

        }

        public void ChangePayStatus(PaymentStatus status)
        {
            PaymentStatus = status;
            if (PaymentStatus == PaymentStatus.Paid) IsPaid = true;
        }

        public Guid UserId { get;private set; }
        public bool IsPaid { get;private set; }
        public string Code { get;private set; }
        public double PayAmount { get;private set; }
        
        public PaymentStatus PaymentStatus { get;private set; }
        public List<OrderDetail> OrderDetails { get; private set; }

        /// <summary>
        /// Change order payStatus to Paid
        /// </summary>
        public void Paid() => IsPaid = true;

        /// <summary>
        /// Add orderdetail
        /// </summary>
        /// <param name="orderDetail"></param>
        public void AddOrderDetails(List<OrderDetail> orderDetail)
        {
            OrderDetails = orderDetail;

        }
    }
}

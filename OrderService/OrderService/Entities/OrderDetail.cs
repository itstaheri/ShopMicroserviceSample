namespace OrderService.Entities
{
    public class OrderDetail : BaseEntity
    {
        public OrderDetail()
        {

        }
        public OrderDetail(Guid orderId, Guid productId, int count, double unitPriceOfProduct)
        {
            OrderId = orderId;
            ProductId = productId;
            Count = count;
            UnitPriceOfProduct = unitPriceOfProduct;
        }

        public Guid OrderId { get;private set; }
        public Guid ProductId { get; private set; }
        public int Count { get; private set; }
        public double UnitPriceOfProduct { get; private set; }
        public Order Order { get; private set; }
    }
}

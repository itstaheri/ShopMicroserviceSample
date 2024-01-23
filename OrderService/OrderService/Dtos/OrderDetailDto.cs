namespace OrderService.Dtos
{
    public class OrderDetailDto
    {
        public Guid OrderId { get;  set; }
        public Guid ProductId { get;  set; }
        public int Count { get;  set; }
        public double UnitPriceOfProduct { get;  set; }
    }
}

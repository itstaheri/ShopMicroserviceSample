namespace CartService.Dtos
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get;  set; }
        public double UnitPrice { get;  set; }
        public string Imageurl { get;  set; }
    }
}

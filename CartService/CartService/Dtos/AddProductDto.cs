namespace CartService.Dtos
{
    public class AddProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; private set; }
        public double UnitPrice { get; private set; }
        public string Imageurl { get; private set; }
    }
}

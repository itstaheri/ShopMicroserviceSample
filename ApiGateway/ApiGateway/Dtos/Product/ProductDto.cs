namespace ApiGateway.Dtos
{
    public class ProductDto
    {
        public string poductId { get; set; }
        public string productName { get; set; }
        public string productCode { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public bool isInStock { get; set; }
        public int quantityInStock { get; set; }
        public string categoryId { get; set; }
    }
}

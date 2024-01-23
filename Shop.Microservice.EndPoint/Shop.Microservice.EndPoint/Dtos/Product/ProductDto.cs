namespace Shop.Microservice.EndPoint.Dtos.Product
{
    public class ProductDto
    {
        public string productName { get; set; }
        public string productCode { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public bool isInStock { get; set; }
        public int quantityInStock { get; set; }
        public string categoryId { get; set; }
        public object category { get; set; }
        public string id { get; set; }
        public DateTime createDate { get; set; }
        public bool isDeleted { get; set; }
    }
}

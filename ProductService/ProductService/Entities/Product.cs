namespace ProductService.Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {

        }
        public Product(string productName, Guid productCode, string description, double price, bool isInStock, int quantityInStock, Guid categoryId)
        {
            ProductName = productName;
            ProductCode = productCode;
            Description = description;
            Price = price;
            IsInStock = isInStock;
            QuantityInStock = quantityInStock;
            CategoryId = categoryId;
        }

        public string ProductName { get;private set; }
        public Guid ProductCode { get;private set; }
        public string Description { get;private set; }
        public double Price { get;private set; }
        public bool IsInStock { get;private set; }
        public int QuantityInStock { get;private set; }
        public Guid CategoryId { get;private set; }
        public Category Category { get; private set; } 


    }
}

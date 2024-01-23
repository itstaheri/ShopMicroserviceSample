namespace CartService.Entities
{
    public class Product : BaseEntity
    {
        public Product(string productName, double unitPrice, string imageurl)
        {
            ProductName = productName;
            UnitPrice = unitPrice;
            Imageurl = imageurl;
            CartItems = new List<CartItem>();
        }

        public string ProductName { get;private set; }
        public double UnitPrice { get;private set; }
        public string Imageurl { get;private set; }
        public ICollection<CartItem> CartItems { get; private set; } 
    }
}

namespace ProductService.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {

        }
        public Category(string categoryName, string categoryDescription)
        {
            CategoryName = categoryName;
            CategoryDescription = categoryDescription;
        }

        public string CategoryName { get;private set; }
        public string CategoryDescription { get; private set; }
        public List<Product> Products { get; private set; } = new();
    }
}

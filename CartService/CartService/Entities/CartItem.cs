namespace CartService.Entities
{
    public class CartItem 
    {
        public CartItem()
        {

        }
        public CartItem(Guid cartId, Guid productId, int count)
        {
            Id = Guid.NewGuid();
            CartId = cartId;
            ProductId = productId;
            Count = count;
        } 

        public Guid Id { get; private set; }
        public int Count { get;private set; }
        public Guid CartId { get;private set; }
        public Cart Cart { get; private set; }
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }
        public void ChangeCount(int count) => Count = count;
    }
}

using AutoMapper;
using CartService.Context;
using CartService.Dtos;
using CartService.Entities;
using CartService.MessageBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CartService.Services
{
    public interface ICartService
    {
        Task<Guid> AddCartAsync(Guid UserId);
        Task<CartDto> GetCartAsync(Guid UserId);
        Task AddCartItem(AddCartItemDto cartItem);
        Task RemoveCartItem(Guid CartItemId);
        ResultDto CheckoutCart(CheckoutDto checkout);

        
        
    }
    public class CartService : ICartService
    {
        private readonly CartContext _context;
        private readonly IMapper _mapper;
        private readonly IMessageBus messageBus;
        private string QueueName_CartCheckout { get; set; }

        public CartService(CartContext context, IMapper mapper, IMessageBus messageBus
            ,IOptions<RabbitMQConfiguration> options)
        {
            _context = context;
            _mapper = mapper;
            this.messageBus = messageBus;
            QueueName_CartCheckout = options.Value.QueueName_CartCheckout;
        }

        public async Task<Guid> AddCartAsync(Guid UserId)
        {
            var cart = new Cart(UserId);
            await _context.AddAsync(cart);
            await _context.SaveChangesAsync();
            return cart.Id;
        }

        public async Task AddCartItem(AddCartItemDto cartItem)
        {
           var cartItemMap = _mapper.Map<CartItem>(cartItem);
            await _context.AddAsync(cartItemMap);
            await _context.SaveChangesAsync();

        }

        public async Task<CartDto> GetCartAsync(Guid UserId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(x=>x.UserId == UserId);
            var cartItems = _context.CartItems.Include(x=>x.Product).Where(x => x.CartId == cart.Id).Select(x => new CartItemDto
            {
                CartId = x.CartId,
                CartItemId = x.Id,
                Count = x.Count,
                ProductId = x.ProductId,
                ProductName = x.Product.ProductName,
                UnitPrice = x.Product.UnitPrice


            }).ToList();
            
            return new CartDto { UserId = cart.UserId ,CartItems = cartItems};

        }

        public async Task RemoveCartItem(Guid CartItemId)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(x => x.Id == CartItemId);
             _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

        private ProductDto GetProduct(Guid productId)
        {
            var product = new ProductDto();
            var productDetail = _context.Products.FirstOrDefault(x => x.Id == productId);
            if (product is not null)
            {
                product.ProductName = productDetail.ProductName;
                product.ProductId = productDetail.Id;
                product.Imageurl = productDetail.Imageurl;
                product.UnitPrice = productDetail.UnitPrice;
            }
            return product;
        }
        private ProductDto AddProduct(AddProductDto entity)
        {
            var checkProduct = GetProduct(entity.ProductId);
            if (checkProduct is not null) return checkProduct;

            var product = new Product(entity.ProductName, entity.UnitPrice, entity.Imageurl);


            _context.Products.Add(product);
            _context.SaveChanges();

            return new ProductDto
            {
                Imageurl = product.Imageurl,
                ProductId = entity.ProductId,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice

            };

        }

        
        public ResultDto CheckoutCart(CheckoutDto checkout)
        {
            //get cart
            var cart = _context.Carts.Include(x=>x.CartItems).FirstOrDefault(x=>x.Id == checkout.CartId);
            if(cart is null) return new ResultDto(false,"Cart not found!");
            //send message to order
            var message = new CheckoutMessageDto {
                CartId = checkout.CartId,
                Description = checkout.Description,
                AddressId = checkout.AddressId,
                UserId = checkout.UserId
            };
            foreach (var item in cart.CartItems)
            {
                var cartItems = new CartItemDto
                {
                    CartId = item.CartId,
                    CartItemId = item.Id,
                    Count = item.Count,
                    ProductId = item.ProductId,
                };  
                message.CartItems.Add(cartItems);
            }

            messageBus.SendMessage(message, QueueName_CartCheckout);
            //remove cart
            _context.Carts.Remove(cart);
            _context.SaveChanges();

            return new ResultDto(true, "successfull");
        }
    }
}

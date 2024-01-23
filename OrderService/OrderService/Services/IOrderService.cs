using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderService.Common;
using OrderService.Context;
using OrderService.Dtos;
using OrderService.Entities;
using OrderService.MessagingBus;
using OrderService.MessagingBus.SendMessage;
using OrderService.MessagingBus.SendMessage.Messages;

namespace OrderService.Services
{
    public interface IOrderService
    {
        /// <summary>
        /// Add new order to database
        /// </summary>
        /// <param name="entery"></param>
        /// <returns></returns>
        Task AddOrdeAsync(AddOrderDto entery);
        /// <summary>
        /// Get all orders of a user from database
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<List<OrderDto>> GetUserOrdersBy(Guid UserId);

        /// <summary>
        /// Get all orders from database
        /// </summary>
        /// <returns></returns>
        Task<List<OrderDto>> GetAllAsync();

        /// <summary>
        /// Request to payment 
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        ResultDto RequestPayment(Guid OrderId);
       
    }
    public class OrderService : IOrderService
    {
        private readonly OrderContext _context;
        private readonly IMessageBus messageBus;
        private readonly string QueueName;
        public OrderService(OrderContext context, IMessageBus messageBus,IOptions<RabbitMQConfiguration> options)
        {
            _context = context;
            this.messageBus = messageBus;
            QueueName = options.Value.QueueName_OrderSendToPay;
        }

        public async Task AddOrdeAsync(AddOrderDto entery)
        {
            var payAmount = entery.Details.Sum(x => x.UnitPriceOfProduct);

            var order = new Order(entery.UserId, CodeGenerator.Generate("Hm450"), payAmount);

            if (entery.Details != null && entery.Details.Count > 0)
            {
                //Map orderDetailDto to orderDetail class
                var orderDetails = entery.Details.Select(x => new OrderDetail(x.OrderId, x.ProductId, x.Count, x.UnitPriceOfProduct)).ToList();
                //add orderdetails
                order.AddOrderDetails(orderDetails);
            }
           await _context.Orders.AddAsync(order);

            await _context.SaveChangesAsync();

        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            return await _context.Orders
                 .Select(x => new OrderDto { Code = x.Code, IsPaid = x.IsPaid, OrderId = x.Id, UserId = x.UserId })
                 .AsNoTracking().ToListAsync();
        }

        public async Task<List<OrderDto>> GetUserOrdersBy(Guid UserId)
        {
            var userOrders = await _context.Orders.Include(x => x.OrderDetails)
                .Where(x => x.UserId == UserId).AsNoTracking().ToListAsync();

            var userOrderDetail = userOrders.SelectMany(x => x.OrderDetails).Select(q => new OrderDetailDto
            {
                Count = q.Count,
                OrderId = q.OrderId,
                ProductId = q.ProductId,
                UnitPriceOfProduct = q.UnitPriceOfProduct
            }).ToList();
            List<OrderDto> orderDto = new List<OrderDto>();

            foreach (var order in userOrders)
            {
                orderDto.Add(new OrderDto
                {
                    Code = order.Code,
                    OrderDetails = userOrderDetail,
                    IsPaid = order.IsPaid,
                    OrderId = order.Id,
                    UserId = order.UserId,
                });

            }
            return orderDto;



        }

        public ResultDto RequestPayment(Guid OrderId)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == OrderId);
            if (order is null) return new ResultDto( false,  "order is not found!" );

            //send message
            messageBus.SendMessage(new PaymentRequestMessage { OrderId = order.Id,PayAmount = order.PayAmount},QueueName);

            order.ChangePayStatus(Entities.Enums.PaymentStatus.RequestForPayment);
            _context.SaveChanges();
            return new ResultDto(true, "successfull");


        }
    }
}

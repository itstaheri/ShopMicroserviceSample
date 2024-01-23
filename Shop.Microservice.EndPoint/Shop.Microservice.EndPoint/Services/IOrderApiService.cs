using Shop.Microservice.EndPoint.Common;
using Shop.Microservice.EndPoint.Dtos;
using Shop.Microservice.EndPoint.Dtos.Order;

namespace Shop.Microservice.EndPoint.Services
{
    public interface IOrderApiService
    {
        ResultDto<IEnumerable<OrderDto>> GetAllOrders();
    }
    public class OrderApiService : IOrderApiService
    {
        RestRequestHelper restRequestHelper = new RestRequestHelper();
        private readonly string accessToken;
        public OrderApiService()
        {
            accessToken = AccessToken.GetAccessToken("https://localhost:7096").Result;
        }
        public ResultDto<IEnumerable<OrderDto>> GetAllOrders()
        {
            var response = restRequestHelper.GETRequest<OrderDto>("https://localhost:7056/api/Order", RestSharp.Method.Get, accessToken);
            if(!response.Item2) return new ResultDto<IEnumerable<OrderDto>>("",false,null);
            return new ResultDto<IEnumerable<OrderDto>>("",true,response.Item1 as IEnumerable<OrderDto>); 
        }
    }
}

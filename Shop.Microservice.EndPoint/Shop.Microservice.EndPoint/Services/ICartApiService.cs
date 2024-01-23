using Shop.Microservice.EndPoint.Common;
using Shop.Microservice.EndPoint.Dtos;
using Shop.Microservice.EndPoint.Dtos.Cart;

namespace Shop.Microservice.EndPoint.Services
{
    public interface ICartApiService
    {
        ResultDto AddCart(Guid userId);
        ResultDto AddCartItem(AddCartItemDto cartItem);
        ResultDto<CartDto> GetCart(Guid userId);
        ResultDto Checkout(CheckoutDto checkout);
    }

    public class CartApiService : ICartApiService
    {
        RestRequestHelper restRequestHelper = new RestRequestHelper();
        public ResultDto AddCart(Guid userId)
        {
            var response = restRequestHelper
                .POSTRequest($"https://localhost:7046/api/Cart/AddCart?userId={userId}", userId, RestSharp.Method.Post, RestSharp.ParameterType.QueryString,"");
            return new ResultDto(response);
        }

        public ResultDto AddCartItem(AddCartItemDto cartItem)
        {
            var response = restRequestHelper
                .POSTRequest("https://localhost:7046/api/Cart/AddCartItem", cartItem, RestSharp.Method.Post, RestSharp.ParameterType.RequestBody,"");
            return new ResultDto(response);
        }

        public ResultDto Checkout(CheckoutDto checkout)
        {
            var response = restRequestHelper
                .POSTRequest("",checkout, RestSharp.Method.Post, RestSharp.ParameterType.RequestBody,"");
        
             return new ResultDto(response); 
        }

        public ResultDto<CartDto> GetCart(Guid userId)
        {
            var response = restRequestHelper
                .GETRequest<CartDto>($"https://localhost:7046/api/Cart/{userId}", RestSharp.Method.Get,"");

            return new ResultDto<CartDto>("", response.Item2, response.Item1 as CartDto);
        }
    }
}

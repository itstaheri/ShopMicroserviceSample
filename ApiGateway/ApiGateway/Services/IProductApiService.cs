using ApiGateway.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Shop.Microservice.EndPoint.Common;

namespace ApiGateway.Services
{
    public interface IProductApiService
    {
      Task<List<ProductDto>>  GetAllProductsAsync();
    }
    public class ProductApiService : IProductApiService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        RestRequestHelper _restRequest = new RestRequestHelper();
        private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public ProductApiService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            _restRequest.setAccessToken().Wait();
         
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var accsessToken = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            var result = await _restRequest.GETRequestAsync<List<ProductDto>>("https://localhost:7225/api/Product/Product", RestSharp.Method.Get);

            Logger.Info($"controller : {nameof(GetAllProductsAsync)},message : request was sended!");
            if (result.Item2 == null)

            {
                Logger.Error($"result is null!");

                return new List<ProductDto>();
            }
            //reutrn product result (product data)
            return result.Item1 as List<ProductDto>;

        }
    }
}

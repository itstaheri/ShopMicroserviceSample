using RestSharp;
using Shop.Microservice.EndPoint.Common;
using Shop.Microservice.EndPoint.Dtos;
using Shop.Microservice.EndPoint.Dtos.Product;
using System.Text.Json;

namespace Shop.Microservice.EndPoint.Services
{
    public interface IProductApiService
    {
        ResultDto<IEnumerable<ProductDto>> GetAllProducts();
    }
    public class ProductApiService : IProductApiService
    {
        RestRequestHelper restRequestHelper = new RestRequestHelper();


       
            
        public ResultDto<IEnumerable<ProductDto>> GetAllProducts()
        {

           var response = restRequestHelper
                .GETRequest<List<ProductDto>>("https://localhost:7078/api/Product/Product", Method.Get,"");
            return new ResultDto<IEnumerable<ProductDto>>("",response.Item2,response.Item1 as IEnumerable<ProductDto>);

        }
    }
}

using IdentityModel.Client;
using RestSharp;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Shop.Microservice.EndPoint.Common
{
    public  class RestRequestHelper
    {
        RestClient client = new RestClient();

        /// <summary>
        /// Send request to api target by GET method and return value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <returns>return object and request result</returns>
        public (object,bool) GETRequest<T>(string url,RestSharp.Method method)
        {

            var request = new RestRequest(url, method);
            RestResponse response = client.Execute(request);
            var deserializeObject = JsonSerializer.Deserialize<T>(response.Content);
            return  (deserializeObject,response.StatusCode == System.Net.HttpStatusCode.OK);
        }
        /// <summary>
        /// Send value by POST method to target api
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Model"></param>
        /// <param name="method"></param>
        /// <param name="parameterType"></param>
        /// <returns>return request result</returns>
        public bool POSTRequest(string url,object Model, RestSharp.Method method,ParameterType parameterType)
        {
            RestClient client = new RestClient();
            var request = new RestRequest(url, method); 
            request.AddHeader("Content-Type", "application/json");
            var serializeObject = JsonSerializer.Serialize(Model);
            request.AddParameter("application/json", serializeObject, parameterType);
            var response = client.Execute(request);
           
            return response.StatusCode == System.Net.HttpStatusCode.OK;


        }
        /// <summary>
        /// Send async request to api target by GET method and return value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <returns>return object and request result</returns>
        public async Task<(object, bool)> GETRequestAsync<T>(string url, RestSharp.Method method,string ?accessToken = null)
        {

            var request = new RestRequest(url, method);
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            RestResponse response =await client.ExecuteAsync(request);
            
            var deserializeObject = JsonSerializer.Deserialize<T>(response.Content);
            return  (deserializeObject, response.StatusCode == System.Net.HttpStatusCode.OK);

        }
        /// <summary>
        /// Send value by POST method to target api by async request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Model"></param>
        /// <param name="method"></param>
        /// <param name="parameterType"></param>
        /// <returns>return request result</returns>
        public async Task<bool> POSTRequestAsync(string url, object Model, RestSharp.Method method, ParameterType parameterType,string ?accessToken = null)
        {
            RestClient client = new RestClient();
            var request = new RestRequest(url, method);
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            request.AddHeader("Content-Type", "application/json");
            var serializeObject = JsonSerializer.Serialize(Model);
            request.AddParameter("application/json", serializeObject, parameterType);
            var response = await client.ExecuteAsync(request);

            return await Task.FromResult(response.StatusCode == System.Net.HttpStatusCode.OK);


        }

        public async Task setAccessToken()
        {
            HttpClient client = new HttpClient();
            var discoveryDocument = await client.GetDiscoveryDocumentAsync("https://localhost:7096");
            var token = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "endpointweb",
                ClientSecret = "123",
                Scope = "orderservice.fullaccess"
            });

            if (token.IsError)
            {
                throw new Exception(token.Error);
            }
        

        }
    }
}

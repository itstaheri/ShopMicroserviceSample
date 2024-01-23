using IdentityModel.Client;
using System.Net.Http;

namespace Shop.Microservice.EndPoint.Common
{
    public static class AccessToken 
    {
        public static async Task<string> GetAccessToken(string RequestUrl)
        {
            HttpClient client = new HttpClient();
            var discoveryDocument =  await client.GetDiscoveryDocumentAsync(RequestUrl);
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
            return token.AccessToken;
        }
    }
}

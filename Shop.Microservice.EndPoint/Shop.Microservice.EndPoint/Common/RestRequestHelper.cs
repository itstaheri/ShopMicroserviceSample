using RestSharp;
using System.Text.Json;

namespace Shop.Microservice.EndPoint.Common
{
    public class RestRequestHelper
    {
        RestClient client = new RestClient();

        public (object, bool) GETRequest<T>(string url, RestSharp.Method method, string? accessToken)
        {

            var request = new RestRequest(url, method);
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                request.AddHeader("Authorization", $"Bearer {accessToken}");
            }

            RestResponse response = client.Execute(request);
            if (response.Content == null || string.IsNullOrEmpty(response.Content)||string.IsNullOrWhiteSpace(response.Content))
            {
                return (null, false);
            }
            var deserializeObject = JsonSerializer.Deserialize<T>(response.Content);
            return (deserializeObject, response.StatusCode == System.Net.HttpStatusCode.OK);
        }
        public bool POSTRequest(string url, object Model, RestSharp.Method method, ParameterType parameterType, string? accessToken)
        {
            RestClient client = new RestClient();
            var request = new RestRequest(url, method);
            request.AddHeader("Content-Type", "application/json");
            if (string.IsNullOrEmpty(accessToken)) request.AddHeader("Authorization", $"Bearer {accessToken}");
            var serializeObject = JsonSerializer.Serialize(Model);
            request.AddParameter("application/json", serializeObject, parameterType);
            var response = client.Execute(request);

            return response.StatusCode == System.Net.HttpStatusCode.OK;


        }
    }
}

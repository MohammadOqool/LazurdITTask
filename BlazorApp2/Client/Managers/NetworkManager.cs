using BlazorApp2.Client.Interfaces;
using BlazorApp2.Shared.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Json;
using System.Text;

namespace BlazorApp2.Client.Managers
{
    public class NetworkManager : INetworkManager
    {
        private readonly HttpClient httpClient;
        public NetworkManager(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetAsyncData(string serviceUrl, Dictionary<string, object> parameters = null)
        {
            string parametersStr;

            if (parameters != null)
            {
                parametersStr = "?";
                foreach (var entry in parameters)
                {
                    var param = $"{entry.Key}={entry.Value}";
                    parametersStr += $"& {param}";
                }
            }
            return await httpClient.GetAsync(serviceUrl);
        }

        public async Task<HttpResponseMessage> PostAsyncData(string serviceUrl, object requestBody, bool isFormUrlEncodedContent = false)
        {

            Task<HttpResponseMessage> request = null;

            if (!isFormUrlEncodedContent)
            {
                var jsonBody = JsonConvert.SerializeObject(requestBody,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
                var jsonRequestBody = new StringContent(jsonBody, Encoding.UTF8, "application/json");
               return await httpClient.PostAsync(serviceUrl, jsonRequestBody);
            }
            else
            {
                var formUrlEncodedContent = new FormUrlEncodedContent(((Dictionary<string, object>)requestBody).ToDictionary(k => k.Key, k => k.Value == null ? "" : k.Value.ToString()));
                return await httpClient.PostAsync(serviceUrl, formUrlEncodedContent);
            }
        }
        /* public ApiResponse<T> GetAsyncData<T>(string serviceUrl, Dictionary<string, object> parameters = null)
         {
             string parametersStr;

             if (parameters != null)
             {
                 parametersStr = "?";
                 foreach (var entry in parameters)
                 {
                     var param = $"{entry.Key}={entry.Value}";
                     parametersStr += $"& {param}";
                 }
             }

             var request = await httpClient.GetAsync(serviceUrl);

             return HandleResponse<T>(request);
         }
         public ApiResponse<T> PostAsyncData<T>(string serviceUrl, object requestBody, bool isFormUrlEncodedContent = false)
         {

             Task<HttpResponseMessage> request = null;

             if (!isFormUrlEncodedContent)
             {
                 var jsonBody = JsonConvert.SerializeObject(requestBody,
                     new JsonSerializerSettings
                     {
                         ContractResolver = new CamelCasePropertyNamesContractResolver()
                     });
                 var jsonRequestBody = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                 request = httpClient.PostAsync(serviceUrl, jsonRequestBody);
             }
             else
             {
                 var formUrlEncodedContent = new FormUrlEncodedContent(((Dictionary<string, object>)requestBody).ToDictionary(k => k.Key, k => k.Value == null ? "" : k.Value.ToString()));
                 request = httpClient.PostAsync(serviceUrl, formUrlEncodedContent);
             }

             request.Wait();
             return HandleResponse<T>(request.Result);
         }

         public ApiResponse<T> PutAsyncData<T>(string serviceUrl, Dictionary<string, object> requestBody, bool isFormUrlEncodedContent = false)
         {
             Task<HttpResponseMessage> request = null;

             if (!isFormUrlEncodedContent)
             {
                 var jsonBody = JsonConvert.SerializeObject(requestBody);
                 var jsonRequestBody = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                 request = httpClient.PostAsync(serviceUrl, jsonRequestBody);
             }
             else
             {
                 var formUrlEncodedContent = new FormUrlEncodedContent(requestBody.ToDictionary(k => k.Key, k => k.Value == null ? "" : k.Value.ToString()));
                 request = httpClient.PostAsync(serviceUrl, formUrlEncodedContent);

             }

             request.Wait();
             return HandleResponse<T>(request.Result);
         }*/

        public ApiResponse<T> HandleResponse<T>(HttpResponseMessage httpResponseMessage)
        {
            var responseStr = httpResponseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<ApiResponse<T>>(responseStr);
        }

    }
}

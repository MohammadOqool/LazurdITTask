using BlazorApp2.Shared.Models;

namespace BlazorApp2.Client.Interfaces
{
    public interface INetworkManager
    {
        /* ApiResponse<T> GetAsyncData<T>(string serviceUrl, Dictionary<string, object> parameters = null);
         ApiResponse<T> PostAsyncData<T>(string serviceUrl, object requestBody, bool isFormUrlEncodedContent = false);
         ApiResponse<T> PutAsyncData<T>(string serviceUrl, Dictionary<string, object> requestBody, bool isFormUrlEncodedContent = false);*/

        Task<HttpResponseMessage> GetAsyncData(string serviceUrl, Dictionary<string, object> parameters = null);
        Task<HttpResponseMessage> PostAsyncData(string serviceUrl, object requestBody, bool isFormUrlEncodedContent = false);

        ApiResponse<T> HandleResponse<T>(HttpResponseMessage httpResponseMessage);
    }
}

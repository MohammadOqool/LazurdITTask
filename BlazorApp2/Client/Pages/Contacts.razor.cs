using BlazorApp2.Client.Interfaces;
using BlazorApp2.Client.Managers;
using BlazorApp2.Shared;
using BlazorApp2.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorApp2.Client.Pages
{
    public partial class Contacts
    {
/*        [Inject]
        private HttpClient httpClient { set; get; }
*/
        [Inject]
        private INetworkManager networkManager { set; get; }

        public ApiResponse<List<Contact>> apiResponseData;
        public ApiResponse<Contact> apiResponseData2;
        public ApiResponse<bool> deleteApiResponseData2;

        protected override async Task OnInitializedAsync()
        {

            var apiHttpResponseMessage = await networkManager.GetAsyncData("api/contacts/getAllContacts");
            apiResponseData = networkManager.HandleResponse<List<Contact>>(apiHttpResponseMessage);


            var contact = new Contact()
            {
                FirstName = "Test",
                LastName = "test",
                Email = "oqo@gbmail.com",
                PhoneNumber = "1111111111"
            };

            var apiHttpResponseMessage2 = await networkManager.PostAsyncData("api/contacts/addContact", contact);
            apiResponseData2 = networkManager.HandleResponse<Contact>(apiHttpResponseMessage2);

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                ["id"] = 1
            };
            var apiHttpResponseMessage3 = await networkManager.GetAsyncData("api/contacts/deleteContact", parameters);
            deleteApiResponseData2 = networkManager.HandleResponse<bool>(apiHttpResponseMessage3);

            var updateapiHttpResponseMessage3 = await networkManager.PostAsyncData($"api/contacts/updateContact/{1}", contact);
            apiResponseData2 = networkManager.HandleResponse<Contact>(updateapiHttpResponseMessage3);

            var updateapiHttpResponseMessage4 = await networkManager.GetAsyncData($"getContactById", parameters);
            apiResponseData2 = networkManager.HandleResponse<Contact>(updateapiHttpResponseMessage4);


            //apiResponseData = await httpClient.GetAsync<ApiResponse<List<Contact>>>("api/contacts/getAllContacts");
            //var d  = await httpClient.GetFromJsonAsync<string>("api/contacts/getAllContacts");

            //apiResponseData = await networkManager.GetAsyncData<List<Contact>>("api/contacts/getAllContacts");
        }
    }
}

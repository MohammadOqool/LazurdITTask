using BlazorApp2.Client.Interfaces;
using BlazorApp2.Client.Managers;
using BlazorApp2.Shared;
using BlazorApp2.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics.Contracts;
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

        [Inject]
        public IJSRuntime Js { get; set; }


        public ApiResponse<List<Contact>> apiResponseData;
        public ApiResponse<bool> deleteApiResponseData;

        protected override async Task OnInitializedAsync()
        {
            var apiHttpResponseMessage = await networkManager.GetAsyncData("api/contacts/getAllContacts");
            apiResponseData = networkManager.HandleResponse<List<Contact>>(apiHttpResponseMessage);

            //apiResponseData = await httpClient.GetAsync<ApiResponse<List<Contact>>>("api/contacts/getAllContacts");
            //var d  = await httpClient.GetFromJsonAsync<string>("api/contacts/getAllContacts");

        }

        public async Task Delete(int id)
        {
            var contact = apiResponseData.data.FirstOrDefault(c => c.ID == id);
            var confirmed = await Js.InvokeAsync<bool>("confirm", "Delete row?");

            if (confirmed)
            {
                Dictionary<string, object> parameters = new()
                {
                    ["id"] = id
                };

                var apiHttpResponseMessage = await networkManager.GetAsyncData("api/contacts/deleteContact", parameters);
                deleteApiResponseData = networkManager.HandleResponse<bool>(apiHttpResponseMessage);
                
                apiHttpResponseMessage = await networkManager.GetAsyncData("api/contacts/getAllContacts");
                apiResponseData = networkManager.HandleResponse<List<Contact>>(apiHttpResponseMessage);
            }
        }
    }
}

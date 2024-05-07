using BlazorApp2.Client.Interfaces;
using BlazorApp2.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp2.Client.Pages
{
    public partial class UpdateContact
    {
        [Parameter]
        public string id { get; set; }

        public Contact contact = new();

        private ApiResponse<Contact> apiResponseData;

        [Inject]
        public INetworkManager  networkManager { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }
        protected async override Task OnInitializedAsync()
        {
            Dictionary<string, object> parameters = new()
            {
                ["id"] = id
            };
            var apiHttpResponseMessage = await networkManager.GetAsyncData("api/contacts/getContactById", parameters);
            apiResponseData = networkManager.HandleResponse<Contact>(apiHttpResponseMessage);

            contact = apiResponseData?.data;
        }
        public async Task Update()
        {
            var apiHttpResponseMessage = await networkManager.PostAsyncData($"api/contacts/updateContact/{id}", contact);
            apiResponseData = networkManager.HandleResponse<Contact>(apiHttpResponseMessage);

            if(apiResponseData != null && apiResponseData.data != null)
            {
                _navigationManager.NavigateTo("/contacts");
            }
        }

    }
}

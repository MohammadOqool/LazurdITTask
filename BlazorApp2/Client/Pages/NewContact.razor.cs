using BlazorApp2.Client.Interfaces;
using BlazorApp2.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp2.Client.Pages
{
    public partial class NewContact
    {
        public Contact contact = new();

        private ApiResponse<Contact> apiResponseData;

        [Inject]
        public INetworkManager  networkManager { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        public async Task Create()
        {
            var apiHttpResponseMessage = await networkManager.PostAsyncData("api/contacts/addContact", contact);
            apiResponseData = networkManager.HandleResponse<Contact>(apiHttpResponseMessage);

            if(apiResponseData != null && apiResponseData.data != null)
            {
                _navigationManager.NavigateTo("/contacts");
            }
        }

    }
}

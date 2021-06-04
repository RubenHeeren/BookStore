using Blazored.LocalStorage;
using BookStore.UI.Contracts;
using BookStore.UI.Models;
using BookStore.UI.Providers;
using BookStore.UI.Static;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BookStore.UI.Services
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationRepository(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> Login(LoginModel user)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoints.LoginEndpoint, user);

            if (response.IsSuccessStatusCode == false)
            {
                return false;
            }

            var content = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<TokenResponse>(content);

            await _localStorage.SetItemAsync("authToken", token.Token);

            await ((APIAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Token);

            return true;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((APIAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
        }

        public async Task<bool> Register(RegisterModel user)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoints.RegisterEndpoint, user);

            return response.IsSuccessStatusCode;
        }
    }
}

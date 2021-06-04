using BookStore.UI.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BookStore.UI.Services
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public BaseRepository(HttpClient httpClientFactory, ILocalStorageService localStorage)
        {
            _httpClient = httpClientFactory;
            _localStorage = localStorage;
        }

        public async Task<bool> Create(string url, T obj)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());

            // Send the Javascript Object equivalent of whatever the instance of T is. obj is the instance of T.
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<T>(url, obj);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Delete(string url, int id)
        {
            if (id < 1)
            {
                return false;
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());

            HttpResponseMessage response = await _httpClient.DeleteAsync(url + id);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        public async Task<T> Get(string url, int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());

            var response = await _httpClient.GetFromJsonAsync<T>(url + id);

            return response;
        }

        public async Task<IList<T>> Get(string url)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());

            var response = await _httpClient.GetFromJsonAsync<List<T>>(url);

            return response;
        }

        public async Task<bool> Update(string url, T obj, int id)
        {
            if (obj == null)
            {
                return false;
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());

            var response = await _httpClient.PutAsJsonAsync<T>(url + id, obj);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        private async Task<string> GetBearerToken()
        {
            return await _localStorage.GetItemAsync<string>("authToken");
        }
    }
}

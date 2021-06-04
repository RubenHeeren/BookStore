using Blazored.LocalStorage;
using BookStore.UI.Contracts;
using BookStore.UI.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookStore.UI.Services
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public BookRepository(HttpClient httpClient, ILocalStorageService localStorageService) : base(httpClient, localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }        
    }
}

using Blazored.LocalStorage;
using BookStore.UI.Contracts;
using BookStore.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookStore.UI.Services
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorageService;

        public BookRepository(IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService) : base(httpClientFactory, localStorageService)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageService = localStorageService;
        }
    }
}

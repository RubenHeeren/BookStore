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
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorage;

        public AuthorRepository(IHttpClientFactory httpClientFactory, ILocalStorageService localStorage) : base(httpClientFactory, localStorage)
        {
            _httpClientFactory = httpClientFactory;
            _localStorage = localStorage;
        }
    }
}

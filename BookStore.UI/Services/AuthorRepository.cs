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
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public AuthorRepository(HttpClient httpClient, ILocalStorageService localStorage) : base(httpClient, localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }
    }
}

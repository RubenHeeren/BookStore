using Blazored.LocalStorage;
using Blazored.Toast;
using BookStore.UI.Contracts;
using BookStore.UI.Providers;
using BookStore.UI.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookStore.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            // Inject dependencies
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazoredToast();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<APIAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
                provider.GetRequiredService<APIAuthenticationStateProvider>());
            builder.Services.AddScoped<JwtSecurityTokenHandler>();
            builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
            builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
            builder.Services.AddTransient<IBookRepository, BookRepository>();
            // builder.Services.AddTransient<IFileUpload, FileUpload>();

            await builder.Build().RunAsync();
        }
    }
}

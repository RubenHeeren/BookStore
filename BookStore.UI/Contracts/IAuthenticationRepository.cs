using BookStore.UI.Models;
using System.Threading.Tasks;

namespace BookStore.UI.Contracts
{
    public interface IAuthenticationRepository
    {
        public Task<bool> Register(RegisterModel user);
        public Task<bool> Login(LoginModel user);
        public Task Logout();
    }
}

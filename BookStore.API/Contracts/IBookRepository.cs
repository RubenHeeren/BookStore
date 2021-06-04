using BookStore.API.Data;
using System.Threading.Tasks;

namespace BookStore.API.Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        public Task<string> GetImageFileName(int id);
    }
}

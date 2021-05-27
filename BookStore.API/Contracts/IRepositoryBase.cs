using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.API.Contracts
{
    /// <summary>
    /// This is a demonstration of generics and asynchrous programming.
    /// Facilitates CRUD operations with persistance to the database.
    /// </summary>
    public interface IRepositoryBase<T> where T : class
    {
        Task<IList<T>> FindAll();
        Task<T> FindById(int id);
        Task<bool> Exists(int id);
        Task<bool> Create(T entity);        
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        // In EF changes must be saved to the database, else they won't persist (only stay in memory).
        Task<bool> Save();
    }
}

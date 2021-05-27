using BookStore.API.Contracts;
using BookStore.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.API.Services
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Author entity)
        {
            await _dbContext.Authors.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Author entity)
        {
            _dbContext.Authors.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            return await _dbContext.Authors.AnyAsync(author => author.Id == id);
        }

        public async Task<IList<Author>> FindAll()
        {
            var authors = await _dbContext.Authors.ToListAsync();
            return authors;
        }

        /// <summary>
        /// Where id is primary key.
        /// </summary>
        public async Task<Author> FindById(int id)
        {
            var author = await _dbContext.Authors.FindAsync(id);
            return author;
        }

        public async Task<bool> Save()
        {
            var changes = await _dbContext.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Author entity)
        {
            _dbContext.Authors.Update(entity);
            return await Save();
        }
    }
}

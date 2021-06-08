using BookStore.API.Contracts;
using BookStore.API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.API.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Book entity)
        {
            await _dbContext.Books.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Book entity)
        {
            _dbContext.Books.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            return await _dbContext.Books.AnyAsync(book => book.Id == id);
        }

        public async Task<IList<Book>> FindAll()
        {
            var books = await _dbContext.Books
                .Include(book => book.Author)
                .ToListAsync();
            return books;
        }

        /// <summary>
        /// Where id is primary key.
        /// </summary>
        public async Task<Book> FindById(int id)
        {
            var book = await _dbContext.Books
                .Include(book => book.Author)
                .FirstOrDefaultAsync(book => book.Id == id);

            return book;
        }

        public async Task<bool> Save()
        {
            var changes = await _dbContext.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<bool> Update(Book entity)
        {
            _dbContext.Books.Update(entity);
            return await Save();
        }
    }
}

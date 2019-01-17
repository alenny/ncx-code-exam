using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ncx.Exam.Api.Models;
using Ncx.Exam.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Ncx.Exam.Api.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Book.ToListAsync();
        }

        public async Task CreateAsync(Book book)
        {
            await _context.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task<Book> GetByIdAsync(long id)
        {
            return await _context.Book.FindAsync(id);
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            _context.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
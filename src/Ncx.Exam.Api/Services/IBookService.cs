using System.Collections.Generic;
using System.Threading.Tasks;
using Ncx.Exam.Api.Models;

namespace Ncx.Exam.Api.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllAsync();

        Task CreateAsync(Book book);

        Task<Book> GetByIdAsync(long id);

        Task UpdateAsync(Book book);

        Task DeleteAsync(Book book);
    }
}
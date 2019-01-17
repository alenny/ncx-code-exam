using Ncx.Exam.Api.Models;
using Ncx.Exam.Api.Data;
using System.Threading.Tasks;
using System.Linq;

namespace Ncx.Exam.Api.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(long id)
        {
            return await _context.User.FindAsync(id);
        }

        public Task<User> GetUserAsync(string userName, string password)
        {
            return Task.FromResult(_context.User.FirstOrDefault(u => u.Name == userName && u.Password == password));
        }
    }
}
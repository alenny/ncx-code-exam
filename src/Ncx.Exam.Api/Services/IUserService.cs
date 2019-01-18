using System.Threading.Tasks;
using Ncx.Exam.Api.Models;

namespace Ncx.Exam.Api.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(long id);

        Task<User> GetUserAsync(string userName, string password);

        Task<User> GetUserAsync(string userName);

        Task<User> CreateUser(string userName, string password);
    }
}
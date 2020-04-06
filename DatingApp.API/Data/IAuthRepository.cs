using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IAuthRepository
    {
        Task<Users> Register ( Users user, string password);

        Task<Users> Login ( string userName, string password);

        Task<bool> UserExits ( string userName);
    }
}
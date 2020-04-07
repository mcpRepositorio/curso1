using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext pContext)
        {
            _context = pContext;
        }
        public Task<Users> Login(string userName, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Users> Register(Users pUser, string pPassword)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            CreatePasswordHash(pPassword, out passwordHash, out passwordSalt);
            //throw new System.NotImplementedException();

            pUser.PasswordHash = passwordHash;
            pUser.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(pUser);
            await _context.SaveChangesAsync();

            return pUser;
        }

        public Task<bool> UserExits(string userName)
        {
            throw new System.NotImplementedException();
        }


        private void CreatePasswordHash(string pPassword, out byte[] pPasswordHash, out byte[] pPasswordSalt) 
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                pPasswordSalt = hmac.Key;
                pPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pPassword));
            }
        }


    }
}
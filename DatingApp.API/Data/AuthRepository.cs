using System;
using System.Threading.Tasks;

using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext pContext)
        {
            _context = pContext;
        }
        public async Task<Users> Login(string userName, string password)
        {
            //throw new System.NotImplementedException();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);

            if (user == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            //throw new NotImplementedException();
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
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

        public async Task<bool> UserExits(string userName)
        {
            //throw new System.NotImplementedException();
            if (await _context.Users.AnyAsync(x => x.UserName == userName))
            {
                return true;
            }
            return false;
        }


        private void CreatePasswordHash(string pPassword, out byte[] pPasswordHash, out byte[] pPasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                pPasswordSalt = hmac.Key;
                pPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pPassword));
            }
        }


    }
}
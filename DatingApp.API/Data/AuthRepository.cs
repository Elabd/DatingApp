using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _dbContext;
        public AuthRepository(DataContext dbContext) => _dbContext = dbContext;
        public async Task<User> Login(string userName, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
                return null;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            return user;
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < ComputeHash.Length; i++)
                {
                    if (ComputeHash[i] == passwordHash[i])
                        continue;
                    return false;
                }
                return true;
            }
        }
        public async Task<User> Regester(User user, string password)
        {
            byte[] hashPassword, saltPassword;
            CreatePasswordHash(password, out hashPassword, out saltPassword);
            user.PasswordHash = hashPassword;
            user.PasswordSalt = saltPassword;
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
        private void CreatePasswordHash(string password, out byte[] hashPassword, out byte[] saltPassword)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                saltPassword = hmac.Key;
                hashPassword = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public async Task<bool> UserExists(string userName)
        {
            if (await _dbContext.Users.AnyAsync(x => x.UserName == userName))
                return true;
            return false;
        }
    }
}
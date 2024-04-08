using FitProDB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitProDB.Repositories
{
    public class UserRepository
    {
        private IronBeastContext _context;
        
        public UserRepository(IronBeastContext context)
        {
            _context = context;
        }
        public async Task<User>GetUserById(long id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
        }
        public async Task<User>Register(User user)
        {
            var register = await _context.Users.AnyAsync(u => u.UserName.Equals(user.UserName));
            if (register)
            {
                return null;
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return new();
        }

        public async Task<User>Login(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<User>CheckPassword(string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Password == password);
        }

        public async Task<User>Update(User user)
        {
            var result = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(user.Id));
            if(result == null)
            {
                return null;
            }
            result.Name = user.Name;
            result.LastName = user.LastName;
            result.UserName = user.UserName;
            result.Password = user.Password;
            result.Address = user.Address;
            result.Email = user.Email;

            
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<User>Delete(int id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
            if(result == null)
            {
                return null;
            }

            _context.Users.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }
    }
}

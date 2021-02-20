using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Warehouse.CoreServices.Interfaces;
using Warehouse.DatabaseEntity.DB;
using Warehouse.DomainModels.Models;

namespace Warehouse.CoreServices.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public UserRepository(
            ApplicationDbContext context,
            UserManager<User> userManager
        )
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<User> Login(string username, string password)
        {
            User user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == username);
            if (user == null)
                return null;
            return user;
        }

        public async Task<User> GetUser(int id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
           
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.DomainModels.Models;

namespace Warehouse.Controllers
{
    [Authorize]
    public class AdminController : BaseApiController
    {

        private readonly UserManager<User> _userManager;
        public AdminController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(Roles = "administrator")]
        [HttpGet("UsersRoles")]
        public async Task<ActionResult> GetUsersWithRoles()
        {
            var users = await _userManager.Users
                   .Include(r => r.UserRoles)
                   .ThenInclude(r => r.Role)
                   .OrderBy(u => u.UserName)
                   .Select(u => new
                   {
                       u.Id,
                       Username = u.UserName,
                       Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
                   })
                   .ToListAsync();
            return Ok(users);
        }
    }
}

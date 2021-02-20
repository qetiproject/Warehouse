using System.Threading.Tasks;
using Warehouse.DomainModels.Models;

namespace Warehouse.CoreServices.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
        Task<User> GetUser(int id);
    }
}

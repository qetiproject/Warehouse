using System.Threading.Tasks;
using Warehouse.DomainModels.Models;

namespace ApplicationShared.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}

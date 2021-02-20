using ApplicationShared.DTOs;
using System.Threading.Tasks;
using Warehouse.DomainModels.Models;

namespace ApplicationShared.Interfaces
{
    public interface IUserService
    {
        Task<Result> Register(UserRegisterDto userRegister);
        Task<Result> Login(UserLoginDto userLogin);
        Task<Result> GetUser(int id);
    }
}

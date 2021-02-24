using ApplicationShared.DTOs;
using ApplicationShared.DTOs.Product;
using System.Threading.Tasks;
using Warehouse.DomainModels.Models;

namespace ApplicationShared.Interfaces
{
    public interface IProductService
    {
        Task<Result> CreateProduct(int userId, ProductCreateDto product);
        Task<Result> GetProducts();
        Task<Result> GetProduct(int id);
        Task<Result> UpdateProduct(ProductUpdateDto productUpdate);
        Task<Result> DeleteProduct(int id);
    }
}

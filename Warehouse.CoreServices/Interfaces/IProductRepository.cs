using System.Threading.Tasks;
using Warehouse.DomainModels.Models;
using ApplicationShared.DTOs;
using ApplicationShared.DTOs.Product;

namespace Warehouse.CoreServices.Interfaces
{
    public interface IProductRepository
    {
        Task<Result> CreateProduct(ProductCreateDto product);
        Task<Result> GetProducts();
        Task<Result> GetProduct(int id);
        Task<Result> UpdateProduct(ProductUpdateDto productUpdate);
        Task<Product> DeleteProduct(int id);
        Task<bool> ProductExists(string name);
    }
}

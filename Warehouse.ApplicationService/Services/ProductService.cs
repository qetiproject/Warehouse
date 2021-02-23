using ApplicationShared.DTOs;
using ApplicationShared.DTOs.Product;
using ApplicationShared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.CoreServices.Interfaces;
using Warehouse.DatabaseEntity.DB;
using Warehouse.DomainModels.Models;

namespace ApplicationShared.Services
{
    public class ProductService : IProductService
    {
        public IProductRepository _productRepository;
        public ApplicationDbContext _context;

        public ProductService(
            IProductRepository productRepository,
            ApplicationDbContext context
        )
        {
            _context = context;
            _productRepository = productRepository;
        }

        public async Task<Result> CreateProduct(ProductCreateDto productCreate)
        {
            return await _productRepository.CreateProduct(productCreate);
        }

        public async  Task<Result> GetProduct(int id)
        {
            return await _productRepository.GetProduct(id);
        }

        public async Task<Result> GetProducts()
        {
            return await _productRepository.GetProducts();
        }

        public async Task<Result> UpdateProduct(ProductUpdateDto productUpdate)
        {
            return await _productRepository.UpdateProduct(productUpdate);
        }

        public async Task<Result> DeleteProduct(int id)
        {
            Result result = new Result();
            Product productById = await _productRepository.DeleteProduct(id);

            List<Product> products = await _context.Products.ToListAsync();

            try
            {
                if (productById != null && productById.IsDeleted)
                {
                    _context.Products.Remove(productById);
                    _context.SaveChanges();
                    result.Data = productById;
                    result.Message = "Product deleted successfull";
                    result.ListCount = products.Count - 1;
                }
                else
                {
                    result.Code = 404;
                    result.Success = false;
                    result.ErrorMessage = "This product doesn't exist";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.Code = ex.HResult;
            }

            return result;

        }
    }
}

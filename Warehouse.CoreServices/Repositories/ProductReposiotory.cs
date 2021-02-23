using ApplicationShared.DTOs;
using ApplicationShared.DTOs.Product;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.CoreServices.Interfaces;
using Warehouse.DatabaseEntity.DB;
using Warehouse.DomainModels.Models;

namespace Warehouse.CoreServices.Repositories
{
    public class ProductReposiotory : IProductRepository
    {
        public ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductReposiotory(
            ApplicationDbContext context,
            IMapper mapper
        )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> CreateProduct(ProductCreateDto productCreate)
        {
            Product product =  _mapper.Map<Product>(productCreate);
            Result result = new Result();
            List<Product> products = await _context.Products.ToListAsync();

            try
            {
                if (await ProductExists(productCreate.Name))
                {
                    result.Data = null;
                    result.ErrorMessage = "Product already exists";
                    result.Code = 404;
                    result.Success = false;
                }
                else
                {
                    await _context.Products.AddAsync(product);
                    await _context.SaveChangesAsync();
                    result.Data = productCreate;
                    result.Code = 200;
                    result.Success = true;
                    result.Message = "Product created successfull";
                    result.ListCount = products.Count + 1;
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

        public async Task<Result> GetProducts()
        {
            Result result = new Result();
            List<Product> products = await _context.Products.ToListAsync();
            IEnumerable<ProductDto> productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);

            try
            {
                result.Data = productsDto;
                result.ListCount = products.Count;
                result.Message = "See all products";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.Code = ex.HResult;
            }
            return result;
        }

        public async Task<Result> GetProduct(int id)
        {
            Result result = new Result();
            Product productById = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            ProductDetail product;

            try
            {
                product = _mapper.Map<ProductDetail>(productById);
                result.Data = product;
                result.Message = "See product detail";
                result.ListCount = 1;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.Code = ex.HResult;
            }

            return result;
        }

        public async Task<Result> UpdateProduct(ProductUpdateDto productUpdate)
        {
            Result result = new Result();
            Product productById = await _context.Products.FirstOrDefaultAsync(p => p.Id == productUpdate.Id);

            try
            {

                productById = _mapper.Map(productUpdate, productById);
                result.Data = productById;
                result.Message = "Product updated successfull";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.Code = ex.HResult;
            }

            return result;
        }

        public async Task<Product> DeleteProduct(int id)
        {
            Product productById = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (productById == null)
            {
                return null;
            }
            productById.IsDeleted = true;

            return productById;
        }

        public async Task<bool> ProductExists(string name)
        {
            if (await _context.Products.AnyAsync(x => x.Name == name))
                return true;
            return false;
        }
    }
}

using ApplicationShared.DTOs;
using ApplicationShared.DTOs.Product;
using ApplicationShared.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;


        public ProductService(
            IProductRepository productRepository,
            UserManager<User> userManager,
            IMapper mapper,
            ApplicationDbContext context
        )
        {
            _context = context;
            _productRepository = productRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result> CreateProduct(int userId, ProductCreateDto productCreate)
        {
            Result result = new Result();

            User user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);

            List<Product> products = await _context.Products.ToListAsync();

            try
            {
                if (user == null)
                {
                    result.ErrorMessage = "Unauthorization user";
                }
                if (await _productRepository.ProductExists(productCreate.Name))
                {
                    result.Data = null;
                    result.ErrorMessage = "Product already exists";
                    result.Code = 404;
                    result.Success = false;
                }
                else
                {
                    Product product = await _productRepository.CreateProduct(productCreate);
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

        public async  Task<Result> GetProduct(int id)
        {
            return await _productRepository.GetProduct(id);
        }

        public async Task<Result> GetProducts()
        {
            return await _productRepository.GetProducts();
        }

        public async Task<Result> UpdateProduct(int userId, ProductUpdateDto productUpdate)
        {
            Result result = new Result();

            User user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);

            Product productById = await _productRepository.UpdateProduct(userId, productUpdate);
            try
            {
                if (user == null || productById == null)
                {
                    result.ErrorMessage = "This product or user doesn't exist";
                   
                }
                if(user != null && await _productRepository.ProductExists(productUpdate.Name))
                {
                    productById = _mapper.Map(productUpdate, productById);
                    await _context.SaveChangesAsync();
                    result.Data = productById;
                    result.Message = "Product updated successfull";
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

        public async Task<Result> DeleteProduct(int id)
        {
            Result result = new Result();
            Product productById = await _productRepository.DeleteProduct(id);

            List<Product> products = await _context.Products.ToListAsync();

            try
            {
                if (productById == null)
                {
                    result.ErrorMessage = "This product doesn't exist";
                }
                else if(productById != null && productById.IsDeleted)
                {
                    _context.Products.Remove(productById);
                    _context.SaveChanges();
                    result.Data = productById;
                    result.Message = "Product deleted successfull";
                    result.ListCount = products.Count - 1;
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

﻿using ApplicationShared.DTOs;
using ApplicationShared.DTOs.Product;
using ApplicationShared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Warehouse.CoreServices.Interfaces;
using Warehouse.DatabaseEntity.DB;
using Warehouse.DomainModels.Models;

namespace Warehouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;
        public ApplicationDbContext _context;

        public ProductController(
            IProductService productService,
            IProductRepository productRepository,
            ApplicationDbContext context
        )
        {
            _productService = productService;
            _productRepository = productRepository;
            _context = context;
        }

        [HttpPost("Product")]
        public async Task<Result> CreateProduct([FromBody] ProductCreateDto productCreate)
        {
            return await _productService.CreateProduct(productCreate);
        }

        [HttpGet("Products")]
        public async Task<Result> GetProducts()
        {
            return await _productService.GetProducts();
        }

        [HttpGet("Product")]
        public async Task<Result> GetProduct(int id)
        {
            return await _productService.GetProduct(id);
        }

        [HttpPut("Product")]
        public async Task<Result> UpdateProduct(ProductUpdateDto productUpdate)
        {
            Result updateProduct = await _productService.UpdateProduct(productUpdate);

            if (await _context.SaveChangesAsync() > 0)
                return updateProduct;

            throw new Exception($"Updating product {productUpdate.Id} failed on save");
        }

        [HttpDelete("Product")]
        public async Task<Result> DeleteProduct(int id)
        {
            Result deleteProduct = await _productService.DeleteProduct(id);

            if (await _context.SaveChangesAsync() > 0 )
                return deleteProduct;

            return deleteProduct;
        }
    }
}

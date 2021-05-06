using DotNetWebAPIMVPStarter.DAL;
using DotNetWebAPIMVPStarter.Models.Product;
using DotNetWebAPIMVPStarter.Models.ResponseWrappers;
using DotNetWebAPIMVPStarter.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Services.Implementations
{
    public class ProductService : IProductService
    {
        private DataContext _context;
        public ProductService(DataContext context)
        {
            _context = context;
        }
        public Response<Product> AddProduct(Product Product)
        {
            //Response<Product> ResultProduct = null;
            _context.Products.Add(Product);
            _context.SaveChanges();

            return new Response<Product>(Product);
        }

        public void DeleteProduct(int Id)
        {
            Product Product = _context.Products.Find(Id);
            if (Product == null) return;
            _context.Products.Remove(Product);
            _context.SaveChanges();

        }

        public PaginatedResponse<List<Product>> GetAllProducts(PaginationFilter Filter)
        {
            //T data, int Page, int NumberOfPages, int PageSize
            //int Page = 1;
            int TotalNumberOfProducts = _context.Products.Count();
            int PageSize = 10;
            int NumberOfPages = (int) Math.Floor(Convert.ToDecimal(TotalNumberOfProducts / PageSize));
            PaginationFilter Paginator = new PaginationFilter(Filter.Page, Filter.Limit);
            List<Product> Products =  _context.Products
                .Include(p => p.Reviews)
                .Skip((Paginator.Page - 1) * Paginator.Limit)
                .Take(Paginator.Limit)
                .ToList();

            return new PaginatedResponse<List<Product>>(Products, Paginator.Page, NumberOfPages, Paginator.Limit);
        }

        public Response<Product> GetSingleProduct(int Id)
        {
            Product Product = _context.Products.Where(x => x.Id == Id).FirstOrDefault();
            return new Response<Product>(Product);
        }

        public void UpdateProduct(int Id, Product Product)
        {
            var prod = _context.Products.Where(x => x.Id == Id).FirstOrDefault();
            if (prod == null) return;

            _context.Entry(Product).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}

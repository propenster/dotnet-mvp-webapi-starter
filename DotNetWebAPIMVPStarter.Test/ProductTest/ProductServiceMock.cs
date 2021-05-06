using DotNetWebAPIMVPStarter.Models.Product;
using DotNetWebAPIMVPStarter.Models.ResponseWrappers;
using DotNetWebAPIMVPStarter.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetWebAPIMVPStarter.Test.ProductTest
{
    public class ProductServiceMock : IProductService
    {

        private readonly List<Product> _products;

        public ProductServiceMock()
        {
            _products = new List<Product>()
            {
                new Product()
                {
                    Id = 1, ProductId = "PXy001", ProductName = "My First Product", Slug =  "my-first-product", ProductPrice = 25.68M, ProductRealPrice = 20.82M, DateCreated = DateTime.UtcNow, DateLastUpdated = null, Reviews = null
                },
                new Product()
                {
                    Id = 2, ProductId = "PXy002", ProductName = "My Second Product", Slug =  "my-second-product", ProductPrice = 39.68M, ProductRealPrice = 55.19M, DateCreated = DateTime.UtcNow, DateLastUpdated = null, Reviews = null

                },
                new Product()
                {
                    Id = 3, ProductId = "PXy003", ProductName = "My Third Product", Slug =  "my-third-product", ProductPrice = 65.68M, ProductRealPrice = 70.19M, DateCreated = DateTime.UtcNow, DateLastUpdated = null, Reviews = null

                }
            };
        }

        public Response<Product> AddProduct(Product Product)
        {
            Product.Id = 3;
            _products.Add(Product);
            return new Response<Product>(Product);
        }

        public void DeleteProduct(int Id)
        {
            Product ExistingProduct = _products.First(x => x.Id == Id);
            _products.Remove(ExistingProduct);
        }

        public PaginatedResponse<List<Product>> GetAllProducts(PaginationFilter Filter)
        {
            int TotalNumberOfProducts = _products.Count();
            int PageSize = 10;
            int NumberOfPages = (int)Math.Floor(Convert.ToDecimal(TotalNumberOfProducts / PageSize));
            PaginationFilter Paginator = new PaginationFilter(Filter.Page, Filter.Limit);
            List<Product> Products = _products
                .Skip((Paginator.Page - 1) * Paginator.Limit)
                .Take(Paginator.Limit)
                .ToList();

            return new PaginatedResponse<List<Product>>(Products, Paginator.Page, NumberOfPages, Paginator.Limit);
        }

        public Response<Product> GetSingleProduct(int Id)
        {
            Product Product = _products.Where(x => x.Id == Id).FirstOrDefault();
            return new Response<Product>(Product);
        }

        public void UpdateProduct(int Id, Product Product)
        {
            Console.WriteLine("This is the Update Product method");
        }
    }
}

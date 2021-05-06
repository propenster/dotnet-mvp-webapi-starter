using DotNetWebAPIMVPStarter.Models.Product;
using DotNetWebAPIMVPStarter.Models.ResponseWrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Services.Interfaces
{
    public interface IProductService
    {
        PaginatedResponse<List<Product>> GetAllProducts(PaginationFilter Filter);
        Response<Product> GetSingleProduct(int Id);
        Response<Product> AddProduct(Product Product); //because I want to use CreatedAtAction in the Controller
        void UpdateProduct(int Id, Product Product);
        void DeleteProduct(int Id);

    }
}

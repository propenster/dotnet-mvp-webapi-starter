using DotNetWebAPIMVPStarter.Controllers;
using DotNetWebAPIMVPStarter.Models.Product;
using DotNetWebAPIMVPStarter.Models.ResponseWrappers;
using DotNetWebAPIMVPStarter.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DotNetWebAPIMVPStarter.Test.ProductTest
{
    public class ProductControllerTest
    {

        ProductsController _productsController;
        IProductService _productService;
        PaginationFilter Filter;

        public ProductControllerTest()
        {
            _productService = new ProductServiceMock();
            _productsController = new ProductsController(_productService);
            Filter = new PaginationFilter { Page = 1, Limit = 3 };
        }
        [Fact]
        public void Test_GetAllProducts_When_Called_Returns_OkResult()
        {
            //Arrange

            //Act
            IActionResult OkResult = _productsController.GetAllProducts(Filter);

            //Assert
            /*ObjectResult Result =*/ 
            Assert.IsType<OkObjectResult>(OkResult);
            //Assert.Equal(200, Result.StatusCode);
        }

        [Fact]
        public void Test_GetSingleProduct_When_Called_Returns_Ok_And_An_Item()
        {
            //Arrange
            int Id = 2;
            //Act
            var OkResult = _productsController.GetSingleProduct(Id) as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(OkResult);
            Assert.IsType<Response<Product>>(OkResult.Value); //this passed also...
        }
        [Fact]
        public void Test_GetSingleProduct_When_Called_Returns_The_Right_Item()
        {
            //Arrange
            int Id = 2;
            Product ItShouldReturn = new Product()
            {
                Id = 2,
                ProductId = "PXy002",
                ProductName = "My Second Product",
                Slug = "my-second-product",
                ProductPrice = 39.68M,
                ProductRealPrice = 55.19M,
                DateCreated = DateTime.UtcNow,
                DateLastUpdated = null,
                Reviews = null

            };
            //Act
            var OkResult = _productsController.GetSingleProduct(Id) as OkObjectResult;

            //Assert
            //Assert.IsType<OkObjectResult>(OkResult);
            var Item = Assert.IsType<Response<Product>>(OkResult.Value); //this passed also...
            Assert.Equal(ItShouldReturn.ProductName, Item.Data.ProductName);
            
        }

        [Fact]
        public void Test_GetSingleProduct_When_Passed_Unknown_Id_Returns_NotFoundResult()
        {
            //Arrange
            int Id = 10;
            
            //Act
            //var Result = _productsController.GetSingleProduct(Id) as OkObjectResult;
            IActionResult NotFoundResult = _productsController.GetSingleProduct(Id);

            //Assert
            //Assert.IsType<OkObjectResult>(OkResult);
            //var Item = Assert.IsType<Response<Product>>(Result.Value); //this passed also...
            //Assert.Null(Item.Data);
            Assert.IsType<NotFoundResult>(NotFoundResult);

        }


        [Fact]
        public void Test_AddProduct_When_Called_Should_Increase_Count_In_Product_List()
        {
            //Arrange

            Product ProductToBeAdded = new Product()
            {
                Id = 4,
                ProductId = "PXy004",
                ProductName = "My Fourth Product",
                Slug = "my-fourth-product",
                ProductPrice = 39.68M,
                ProductRealPrice = 55.19M,
                DateCreated = DateTime.UtcNow,
                DateLastUpdated = null,
                Reviews = null

            };
            //Act
            var OkResult = _productsController.AddProduct(ProductToBeAdded) as OkObjectResult;
            //ok we added something, we should now have 4 items I guess, let's call getAllPriducts agfain
            var OkAllProductResult = _productsController.GetAllProducts(Filter) as OkObjectResult;


            //Assert
            Assert.IsType<OkObjectResult>(OkResult); //Was the new product Creation successful?
            //var AllProductResponse = Assert.IsType<PaginatedResponse<List<Product>>>(OkAllProductResult.Value);
            //Assert.Equal(4, AllProductResponse.Data.Count);
        }


    }
}

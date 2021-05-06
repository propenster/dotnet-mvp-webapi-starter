using DotNetWebAPIMVPStarter.Models.Product;
using DotNetWebAPIMVPStarter.Models.ResponseWrappers;
using DotNetWebAPIMVPStarter.Services.Interfaces;
using DotNetWebAPIMVPStarter.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DotNetWebAPIMVPStarter.Test.ProductTest
{
    public class ProductServiceTest
    {

        private IProductService _productService;
        PaginationFilter Filter;
        public ProductServiceTest()
        {
            _productService = new ProductServiceMock();
            Filter = new PaginationFilter { Page = 1, Limit = 3 };
        }

        [Fact]
        public void Test_AddProduct_Returns_Right_Response()
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
            var Result = _productService.AddProduct(ProductToBeAdded);

            //Asssert
            Assert.IsType<Response<Product>>(Result);
        }

        [Fact]
        public void Test_AddProduct_Actually_Pushed_Item_To_The_List()
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
            var Result = _productService.AddProduct(ProductToBeAdded);

            //Asssert
            var Item = Assert.IsType<Response<Product>>(Result);
            Assert.Equal("my-fourth-product", Item.Data.Slug);
        }
        [Fact]
        public void Test_GetAllProducts_Returns_Right_Response()
        {
            //Arrange


            //Act
            var Result = _productService.GetAllProducts(Filter);

            //Assert
            Assert.IsType<PaginatedResponse<List<Product>>>(Result);
        }
        [Fact]
        public void Test_GetAllProducts_Returns_Correct_Number_Of_Items_In_Collection()
        {
            //Arrange


            //Act
            var Result = _productService.GetAllProducts(Filter);

            //Assert
            var Items = Assert.IsType<PaginatedResponse<List<Product>>>(Result);
            Assert.Equal(3, Items.Data.Count);
        }
        [Fact]
        public void Test_GetAllProducts_Pagination_In_Response_Is_Correct()
        {
            //Arrange
            //we need to check that it returens 3 items per page...as we defined in this class' constructor

            //Act
            var Result = _productService.GetAllProducts(Filter);

            //Assert
            var Items = Assert.IsType<PaginatedResponse<List<Product>>>(Result);
            Assert.Equal(3, Items.PageSize); //does it return 3 items on a page?
            Assert.Equal(1, Items.Page); //is the default page  Page 1?
        }

        [Fact]
        public void Test_GetSingleProduct_Returns_Right_Response()
        {
            //Arrange
            int Id = 2;

            //Act
            var Result = _productService.GetSingleProduct(Id);
            //Assert
            Assert.IsType<Response<Product>>(Result);

        }

        [Fact]
        public void Test_GetSingleProduct_Returns_Right_Item()
        {
            //Arrange
            int Id = 3;

            //Act
            var Result = _productService.GetSingleProduct(Id);
            //Assert
            var Item = Assert.IsType<Response<Product>>(Result);
            Assert.Equal(65.68M, Item.Data.ProductPrice);

        }

        [Fact]
        public void Test_Slugify_Works()
        {
            //Arrange
            int Id = 3;
           
            //Act
            var Result = _productService.GetSingleProduct(Id);
            //Assert
            var Item = Assert.IsType<Response<Product>>(Result);
            Assert.Equal("my-third-product", Item.Data.ProductName.Slugify());


        }



    }
}

using DotNetWebAPIMVPStarter.Models.Product;
using DotNetWebAPIMVPStarter.Models.ResponseWrappers;
using DotNetWebAPIMVPStarter.Models.Role;
using DotNetWebAPIMVPStarter.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("get_all")]
        public IActionResult GetAllProducts([FromQuery] PaginationFilter Filter)
        {
            return Ok(_productService.GetAllProducts(Filter));
        }
        [HttpGet]
        [Route("get_single/{Id}")]
        public IActionResult GetSingleProduct(int Id)
        {
            Response<Product> Result = _productService.GetSingleProduct(Id);
            if (Result.Data == null) return NotFound();

            return Ok(Result);
        }
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        [Route("add_product")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AddProduct([FromBody] Product Product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Response<Product> AddedProduct = _productService.AddProduct(Product);
            //return CreatedAtAction("GetProductById", new { Id = AddedProduct.Data.Id }, AddedProduct);
            //return Created(Added)
            return Ok(AddedProduct);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut]
        [Route("update_product/{Id}")]
        public IActionResult UpdateProduct(int Id, [FromBody] Product Product)
        {
            if (Id != Product.Id) return NotFound();
            if (!ModelState.IsValid) return BadRequest();

            _productService.UpdateProduct(Id, Product);
            return Ok();
        }
    }
}

using AutoMapper;
using Ecom.DTOS;
using Ecom.InterFaces.Ecom.InterFaces;
using Ecom.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productRepo.GetProducts();
            List<ProductDto> productsDto = _mapper.Map<List<ProductDto>>(products);
            return Ok(productsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _productRepo.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            var newProduct = _productRepo.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            var existingProduct = _productRepo.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            var updatedProduct = _productRepo.UpdateProduct(id, product);
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var existingProduct = _productRepo.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            _productRepo.Delete(existingProduct);
            return NoContent();
        }
    }
}
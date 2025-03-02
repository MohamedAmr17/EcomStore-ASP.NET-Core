using Ecom.InterFaces;
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

        public ProductsController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productRepo.GetProducts();
            return Ok(products);
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
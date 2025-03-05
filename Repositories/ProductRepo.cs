using Ecom.InterFaces.Ecom.InterFaces;
using Ecom.Models;

namespace Ecom.Repositories
{
    public class ProductRepo : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public Product AddProduct(Product entity)
        {
            _context.Products.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public int Delete(Product entity)
        {
            _context.Products.Remove(entity);
            return _context.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Find(id);
        }

        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public Product UpdateProduct(int productId, Product entity)
        {
            var existingProduct = _context.Products.Find(productId);

            if (existingProduct != null)
            {
                existingProduct.Title = entity.Title;
                existingProduct.Description = entity.Description;
                existingProduct.Image = entity.Image;
                existingProduct.Price = entity.Price;

                _context.SaveChanges();
            }

            return existingProduct;
        }
    }
}
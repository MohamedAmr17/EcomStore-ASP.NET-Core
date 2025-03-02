using Ecom.Models;

namespace Ecom.InterFaces
{
    namespace Ecom.InterFaces
    {
        public interface IProductRepository
        {
            Product GetProductById(int id);
            List<Product> GetProducts();
            Product UpdateProduct(int productId, Product entity);
            Product AddProduct(Product entity);
            int Delete(Product entity);
        }
    }
}

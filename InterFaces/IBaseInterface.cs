using Ecom.DTOS;
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
        public interface IUserRepository
        {
            User GetUserById(int id);
            User GetUserByUsername(string username);
            List<User> GetUsers();
            User UpdateUser(int userId, User entity);
            User AddUser(User entity);
            int Delete(User entity);
            bool IsUsernameUnique(string username);
            bool IsEmailUnique(string email);
        }
    }
}

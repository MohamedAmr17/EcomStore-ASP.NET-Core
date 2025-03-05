using Ecom.InterFaces.Ecom.InterFaces;
using Ecom.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Repositories
{
    public class UserRepo : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public User AddUser(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public int Delete(User entity)
        {
            _context.Users.Remove(entity);
            return _context.SaveChanges();
        }

        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool IsEmailUnique(string email)
        {
            return !_context.Users.Any(u => u.Email == email);
        }

        public bool IsUsernameUnique(string username)
        {
            return !_context.Users.Any(u => u.Username == username);
        }

        public User UpdateUser(int userId, User entity)
        {
            var existingUser = _context.Users.Find(userId);

            if (existingUser != null)
            {
                existingUser.Username = entity.Username;
                existingUser.Email = entity.Email;

                // Only update password if provided
                if (!string.IsNullOrEmpty(entity.PasswordHash))
                {
                    existingUser.PasswordHash = entity.PasswordHash;
                }

                _context.SaveChanges();
            }

            return existingUser;
        }
    }
}
using Microsoft.EntityFrameworkCore;
using SimpleOrderManagementSystem.Models;

namespace SimpleOrderManagementSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public int AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return user.UId; // Return the newly created user's ID
            }

            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public User GetUserForLogin(string email, string password)
        { 
           return _context.Users.Where(u => u.Email == email & u.Password == password).FirstOrDefault();
        
        }

        public User GetUserById(int id)
        {
            return _context.Users.Where(u => u.UId == id).FirstOrDefault();
        }
    }
}

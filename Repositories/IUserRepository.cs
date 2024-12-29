using SimpleOrderManagementSystem.Models;

namespace SimpleOrderManagementSystem.Repositories
{
    public interface IUserRepository
    {
        int AddUser(User user);
        User GetUserForLogin(string email,string password);

        User GetUserById(int id);
    }
}
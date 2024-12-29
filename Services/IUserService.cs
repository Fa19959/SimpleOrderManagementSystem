using SimpleOrderManagementSystem.DTOs;
using SimpleOrderManagementSystem.Models;

namespace SimpleOrderManagementSystem.Services
{
    public interface IUserService
    {
        int AddUser(UserInputDTO userInputDTO);
        string login(string email, string password);

        UserOutputDTO GetUserById(int id);
    }
}
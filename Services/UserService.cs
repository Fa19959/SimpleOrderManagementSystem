using SimpleOrderManagementSystem.DTOs;
using SimpleOrderManagementSystem.Models;
using SimpleOrderManagementSystem.Repositories;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace SimpleOrderManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public int AddUser(UserInputDTO userInputDTO)
        {
            string HashedPassword = PasswordHashing(userInputDTO.password);

            var user = new User
            {
                Name = userInputDTO.name,
                role = userInputDTO.role,
                Email = userInputDTO.email,
                Password = HashedPassword,
                CreatedAt = DateTime.Now,
            };

            return _userRepository.AddUser(user);

        }

        public string login(string email, string password)
        {
            string HashedPassword = PasswordHashing(password);
            var user = _userRepository.GetUserForLogin(email, HashedPassword);
            if(user == null) 
            {
                return null;
            }

            else 
            {
               return GenerateJwtToken(user.UId.ToString(), user.Name);
            }
        }

        public UserOutputDTO GetUserById(int id)
        {
            try 
            {
                var user = _userRepository.GetUserById(id);
                var outPut = new UserOutputDTO
                {
                    name = user.Name,
                    email = user.Email,
                    role = user.role,
                    createdOn = user.CreatedAt

                };

                return outPut;
            
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        private string PasswordHashing(string password)
        {

            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the input password to a byte array
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                // Convert the hash to a string (hexadecimal representation)
                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2")); // Converts each byte to a hex string
                }
                return hashString.ToString();
            }
        }

        public string GenerateJwtToken(string userId, string username)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, userId),
        new Claim(JwtRegisteredClaimNames.UniqueName, username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

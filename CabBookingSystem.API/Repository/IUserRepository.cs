using CabBookingSystem.API.Dtos;
using CabBookingSystem.API.ScaffoldModels;

namespace CabBookingSystem.API.Repository
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(int userId);
        Task<List<LoginDto>> GetAllUsersAsync();


    }
}

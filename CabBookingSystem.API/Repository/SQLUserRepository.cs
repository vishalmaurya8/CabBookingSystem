using CabBookingSystem.API.Dtos;
using CabBookingSystem.API.ScaffoldContext;
using CabBookingSystem.API.ScaffoldModels;
using Microsoft.EntityFrameworkCore;

namespace CabBookingSystem.API.Repository
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly CarBookingSystemCbsContext dbcontext;

        public SQLUserRepository(CarBookingSystemCbsContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            await dbcontext.Users.AddAsync(user);
            await dbcontext.SaveChangesAsync();
            return user;
        }

        public async Task<List<LoginDto>> GetAllUsersAsync()
        {
            var users = await dbcontext.Users
                                        .Select(u => new LoginDto
                                        {
                                            
                                            Name = u.Name,
                                            Email = u.Email
                                            // Add other properties as needed from your User entity to UserDto
                                        })
                                        .ToListAsync();
            return users;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await dbcontext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await dbcontext.Users.FindAsync(userId);
        }
    }
}

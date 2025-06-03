using CabBookingSystem.API.ScaffoldModels;

namespace CabBookingSystem.API.Repository
{
    public interface IJWTTokenRepository
    {
        string CreateToken(User user);
    }
}

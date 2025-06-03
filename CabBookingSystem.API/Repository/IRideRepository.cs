using CabBookingSystem.API.Dtos;
using CabBookingSystem.API.ScaffoldModels;

namespace CabBookingSystem.API.Repository
{
    public interface IRideRepository
    {

        Task<Ride> BookRideAsync(BookRideDto dto);
        Task UpdateRideStatusAsync(int id, UpdateRideStatusDto dto);
        Task<IEnumerable<RideResponseDto>> GetRidesByUserAsync(int userId);
        Task<RideResponseDto> GetRideByIdAsync(int id);

    }
}

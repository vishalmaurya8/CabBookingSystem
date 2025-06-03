using CabBookingSystem.API.Dtos;
using CabBookingSystem.API.ScaffoldContext;
using CabBookingSystem.API.ScaffoldModels;
using Microsoft.EntityFrameworkCore;

namespace CabBookingSystem.API.Repository
{
    public class SQLRideRepository : IRideRepository
    {
        private readonly CarBookingSystemCbsContext dbContext;

        public SQLRideRepository(CarBookingSystemCbsContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Ride> BookRideAsync(BookRideDto dto)
        {

            var ride = new Ride
            {
                UserId = dto.UserId,
                DriverId = dto.DriverId,
                PickupLocation = dto.PickupLocation,
                DropoffLocation = dto.DropoffLocation,
                Fare = dto.Fare,
                Status = "Pending"
            };

            dbContext.Rides.Add(ride);
            await dbContext.SaveChangesAsync();

            return ride;

        }

        public async Task<RideResponseDto> GetRideByIdAsync(int id)
        {

            var ride = await dbContext.Rides
             .Include(r => r.Driver)
             .FirstOrDefaultAsync(r => r.RideId == id);

            if (ride == null)
            {
                throw new KeyNotFoundException("Ride not found");
            }

            return new RideResponseDto
            {
                RideId = ride.RideId,
                PickupLocation = ride.PickupLocation,
                DropoffLocation = ride.DropoffLocation,
                Fare = ride.Fare,
                Status = ride.Status,
                DriverName = ride.Driver.Name
            };

        }

        public async Task<IEnumerable<RideResponseDto>> GetRidesByUserAsync(int userId)
        {

            var rides = await dbContext.Rides
             .Where(r => r.UserId == userId)
             .Include(r => r.Driver)
             .ToListAsync();

            return rides.Select(ride => new RideResponseDto
            {
                RideId = ride.RideId,
                PickupLocation = ride.PickupLocation,
                DropoffLocation = ride.DropoffLocation,
                Fare = ride.Fare,
                Status = ride.Status,
                DriverName = ride.Driver.Name
            });

        }

        public async Task UpdateRideStatusAsync(int id, UpdateRideStatusDto dto)
        {

            var ride = await dbContext.Rides.FindAsync(id);
            if (ride == null)
            {
                throw new KeyNotFoundException("Ride not found");
            }

            ride.Status = dto.Status;
            await dbContext.SaveChangesAsync();

        }
    }
}

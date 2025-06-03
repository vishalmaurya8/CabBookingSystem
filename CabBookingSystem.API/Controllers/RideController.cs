using CabBookingSystem.API.Dtos;
using CabBookingSystem.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CabBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RideController : ControllerBase
    {
        private readonly IRideRepository rideRepository;

        public RideController(IRideRepository rideRepository)
        {
            this.rideRepository = rideRepository;
        }


        // GET: api/rides/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RideResponseDto>> GetRideById(int id)
        {
            var ride = await rideRepository.GetRideByIdAsync(id);
            return Ok(ride);
        }


        // POST: api/rides/book
        [HttpPost("book")]
        public async Task<ActionResult<RideResponseDto>> BookRide(BookRideDto dto)
        {
            var ride = await rideRepository.BookRideAsync(dto);

            var response = new RideResponseDto
            {
                RideId = ride.RideId,
                PickupLocation = ride.PickupLocation,
                DropoffLocation = ride.DropoffLocation,
                Fare = ride.Fare,
                Status = ride.Status,
                DriverName = ride.Driver?.Name ?? "Unknown"
            };

            return CreatedAtAction(nameof(GetRideById), new { id = ride.RideId }, response);
        }


        // PUT: api/rides/status/{id}
        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateRideStatus(int id, UpdateRideStatusDto dto)
        {
            await rideRepository.UpdateRideStatusAsync(id, dto);
            return NoContent();
        }


    }
}

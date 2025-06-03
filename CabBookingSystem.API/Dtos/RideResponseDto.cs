namespace CabBookingSystem.API.Dtos
{
    public class RideResponseDto
    {
        public int RideId { get; set; }
        public string PickupLocation { get; set; } = null!;
        public string DropoffLocation { get; set; } = null!;
        public decimal Fare { get; set; }
        public string? Status { get; set; }
        public string DriverName { get; set; } = null!;
    }
}

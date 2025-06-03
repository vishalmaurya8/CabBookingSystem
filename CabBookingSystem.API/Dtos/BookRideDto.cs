namespace CabBookingSystem.API.Dtos
{
    public class BookRideDto
    {

        public int UserId { get; set; }
        public int DriverId { get; set; }
        public string PickupLocation { get; set; } = null!;
        public string DropoffLocation { get; set; } = null!;
        public decimal Fare { get; set; }

    }
}
